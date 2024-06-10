using UnityEngine;

namespace Character.Monster
{
    public class Monster : Character
    {
        [SerializeField] protected float preAttackDelay;
        [SerializeField] protected float cooldownAttack;
        [SerializeField] protected bool isStunned;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        protected virtual void Attack()
        {
            print("attack player");
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
                Attack();
        }
    }
}
