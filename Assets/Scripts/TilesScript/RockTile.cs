using System;
using ObjectInGame;
using UnityEngine;
using UnityEngine.Serialization;

namespace TilesScript
{
    public class RockTile : TilesBlock
    {
        [Header("Moving Rock")]
        [SerializeField] private MovingObject movingObject;

        protected override void Start()
        {
            base.Start();
        }

        public override void SetToInitialTile(Vector3 startPos = default)
        {
            base.SetToInitialTile(startPos);
            movingObject.AtTop = true;
            movingObject.AtBottom = false;
        }
    }
}
