using UnityEngine;

namespace TilesScript
{
    public class DoorTile : TilesBlock
    {
        [Header("Door Destination")]
        [SerializeField] private Transform doorDestination;
        [SerializeField] private bool isOpen;

        protected override void CheckPlayerOnTile()
        {
            base.CheckPlayerOnTile();
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);
        }

        private void EnterSecretRoom()
        {
            
        }
    }
}
