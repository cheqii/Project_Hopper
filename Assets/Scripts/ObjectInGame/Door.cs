using Interaction;
using Interface;
using TilesScript;
using UnityEngine;

namespace ObjectInGame
{
    public class Door : ObjectInGame, IInteraction
    {
        [SerializeField] protected DoorTile doorTile;
        [SerializeField] private bool isOpen;
        public bool IsOpen
        {
            get => isOpen;
            set => isOpen = value;
        }

        [SerializeField] private SpriteRenderer doorSprite;
        public SpriteRenderer DoorSprite
        {
            get => doorSprite;
            set => doorSprite = value;
        }

        [Header("Interactable")]
        [SerializeField] private InteractableObject interactableObject;

        [SerializeField] protected Nf_GameEvent doorEvent;
    
        // Start is called before the first frame update
        void Start()
        {
            doorTile = GetComponentInParent<DoorTile>();
            interactableObject.Interactable = this;
        }

        private void OpenTheDoor()
        {
            SoundManager.Instance.PlaySFX("Trigger");
            isOpen = true;
            doorSprite.color = Color.gray;
        }
    
        public void InteractWithObject(int damage = default)
        {
            OpenTheDoor();
            if(doorTile.PlayerOnTile != null)
                doorTile.CheckPlayerOnTile();
        }

        public virtual void EnterTheDoor()
        {
            
        }
    }
}
