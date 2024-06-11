using ObjectPool;
using UnityEngine;

namespace Interaction.Object
{
    public class ChestInteraction : Interactable
    {
        public override void InteractWithObject(int damage = default)
        {
            print("Interact with chest");
            PoolManager.ReleaseObject(gameObject);
        }
    }
}
