using System;
using Interaction;
using Interface;
using ObjectInGame;
using ObjectPool;
using UnityEngine;
using UnityEngine.Serialization;

namespace TilesScript
{
    public class DoorTile : TilesBlock
    {
        [Header("Door")]
        [SerializeField] private Door door;
        
        [SerializeField] private bool alreadyEnter;
        public bool AlreadyEnter
        {
            get => alreadyEnter;
            set => alreadyEnter = value;
        }

        protected override void Start()
        {
            base.Start();
        }

        public override void SetToInitialTile(Vector3 startPos = default)
        {
            base.SetToInitialTile(startPos);
            alreadyEnter = false;
            door.IsOpen = false;
            door.DoorSprite.color = Color.black;
        }

        protected override void StartAction()
        {
            base.StartAction();
        }

        public override void CheckPlayerOnTile()
        {
            base.CheckPlayerOnTile();
            
            if(playerOnTile == null) return;
            if(!door.IsOpen) return;
            if(alreadyEnter) return;
            
            door.EnterTheDoor();
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);
        }
    }
}
