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
        public Transform NormalLevelParent
        {
            get => normalLevelParent;
            set => normalLevelParent = value;
        }
        
        [SerializeField] private Transform secretRoomParent;
        public Transform SecretRoomParent
        {
            get => secretRoomParent;
            set => secretRoomParent = value;
        }
        
        [Header("Door")]
        [SerializeField] private Door door;
        
        [SerializeField] private bool alreadyEnter;
        public bool AlreadyEnter
        {
            get => alreadyEnter;
            set => alreadyEnter = value;
        }

        [SerializeField] private bool isGenerateDone;
        public bool IsGenerateDone
        {
            get => isGenerateDone;
            set => isGenerateDone = value;
        }

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
            
            door.EnterTheDoor();
            // door.open
            //     EnterSecretRoom();
            // if (doorType == DoorType.ExitDoor)
            //     ExitSecretRoom();
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);
        }

    }
}
