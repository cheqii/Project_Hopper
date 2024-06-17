using System.Collections;
using System.Collections.Generic;
using Interaction;
using Interface;
using TilesScript;
using UnityEngine;
using UnityEngine.Serialization;

public class Door : ObjectInGame, IInteraction
{
    [SerializeField] private DoorTile doorTile;
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
    
    // Start is called before the first frame update
    void Start()
    {
        doorTile = GetComponentInParent<DoorTile>();
        interactableObject.Interactable = this;
    }

    private void OpenTheDoor()
    {
        isOpen = true;
        doorSprite.color = Color.gray;
    }
    
    public void InteractWithObject(int damage = default)
    {
        OpenTheDoor();
        if(doorTile.PlayerOnTile != null)
            doorTile.CheckPlayerOnTile();
    }
}
