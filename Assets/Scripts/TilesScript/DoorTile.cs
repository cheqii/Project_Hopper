using System;
using Interaction;
using Interface;
using ObjectInGame;
using ObjectPool;
using UnityEngine;
using UnityEngine.Serialization;

namespace TilesScript
{
    public enum DoorType
    {
        EnterDoor,
        ExitDoor
    }
    
    public class DoorTile : TilesBlock
    {
        [Header("Door Type")]
        [SerializeField] private DoorType doorType;

        [Header("Door Destination")] 
        [SerializeField] private Transform normalLevelParent;
        [SerializeField] private Transform secretRoomParent;
        
        [Header("Door")]
        [SerializeField] private Door door;
        [SerializeField] private bool alreadyEnter;
        
        [SerializeField] private bool isGenerateDone;
        
        protected override void Start()
        {
            base.Start();
        }

        public override void SetToInitialTile(Vector3 startPos = default)
        {
            base.SetToInitialTile(startPos);
            isGenerateDone = false;
            alreadyEnter = false;
            door.IsOpen = false;
            door.DoorSprite.color = Color.black;
        }

        protected override void StartAction()
        {
            base.StartAction();
            normalLevelParent = PoolManager.Instance.root.parent;
            secretRoomParent = PoolManager.Instance.secretRoom;
        }

        public override void CheckPlayerOnTile()
        {
            base.CheckPlayerOnTile();
            
            if(playerOnTile == null) return;
            if(!door.IsOpen) return;
            if(alreadyEnter) return;
            
            if(doorType == DoorType.EnterDoor)
                EnterSecretRoom();
            if (doorType == DoorType.ExitDoor)
                ExitSecretRoom();
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);
        }

        private void EnterSecretRoom()
        {
            alreadyEnter = true;
            _Player.PlayerInSecretRoom = true;

            secretRoomParent.gameObject.SetActive(true);
            normalLevelParent.gameObject.SetActive(false);

            if(isGenerateDone) return;
            LevelGenerator.Instance.GenerateTileSecretRoom(transform.position.y, transform.position, gameObject);
            isGenerateDone = true;
        }

        private void ExitSecretRoom()
        {
            _Player.PlayerInSecretRoom = false;
            normalLevelParent.gameObject.SetActive(true);
            secretRoomParent.gameObject.SetActive(false);
            
            ReleaseSecretRoom();

            _Player.transform.position = new Vector3(_Player.transform.position.x, transform.position.y + 2);
        }

        private void ReleaseSecretRoom()
        {
            foreach (Transform child in secretRoomParent)
            {
                PoolManager.ReleaseObject(child.gameObject);
            }
        }
    }
}
