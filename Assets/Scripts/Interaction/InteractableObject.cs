using UnityEngine;

namespace Interaction
{
    public class InteractableObject : MonoBehaviour
    {
        [SerializeField] private Interactable interactable;

        public void Interaction(int value = default)
        {
            interactable.InteractWithObject(value);
        }
    }
}
