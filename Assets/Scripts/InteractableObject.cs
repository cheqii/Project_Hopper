using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private Interactable anyObject;
    
    public void InteractToObject(int damage = default)
    {
        // var obj = anyObject from IInteractable;
        // obj.InteractToObject(damage);
    }
}
