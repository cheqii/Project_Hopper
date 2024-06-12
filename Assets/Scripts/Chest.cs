using System;
using Interaction;
using Interface;
using ObjectPool;
using UnityEngine;

public class Chest : MonoBehaviour, IInteraction
{
    [SerializeField] private InteractableObject interactableObject;

    private void Start()
    {
        InitialObject();
    }

    public void InitialObject()
    {
        interactableObject.Interactable = this;
    }
    public void InteractWithObject(int damage = default)
    {
        PoolManager.ReleaseObject(gameObject);
    }
}