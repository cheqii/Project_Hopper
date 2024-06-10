using UnityEngine;

namespace Character
{
    public class Character : MonoBehaviour
    {
        [Header("Character Attributes")]
        [SerializeField] protected int health;
        public int Health => health;
        
        [SerializeField] protected int attackDamage;

        public virtual void TakeDamage(int damage)
        {
            if (health > 0)
                health -= damage;
        }
    }
}
