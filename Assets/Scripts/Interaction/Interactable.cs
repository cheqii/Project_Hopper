using Interface;
using UnityEngine;

namespace Interaction
{
    public class Interactable : MonoBehaviour, IInteraction
    {
        public virtual void InteractWithObject(int damage = default)
        {
            
        }
    }
}