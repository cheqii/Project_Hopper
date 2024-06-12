using System;
using Character.Monster;
using Interface;
using UnityEngine;

namespace Interaction
{
    public class InteractableObject : MonoBehaviour
    {
        private IInteraction interactable;

        public IInteraction Interactable
        {
            get => interactable;
            set => interactable = value;
        }

        public void Interaction(int value = default)
        {
            interactable.InteractWithObject(value);
        }
    }
}
