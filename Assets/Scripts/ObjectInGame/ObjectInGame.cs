using Character;
using UnityEngine;

namespace ObjectInGame
{
    public class ObjectInGame : MonoBehaviour
    {
        [SerializeField] protected Player _player;
        public Player _Player
        {
            get => _player;
            set => _player = value;
        }
    
        [SerializeField] protected int damage;

        public virtual void SetToInitialObject(Vector3 startPos = default)
        {
            transform.position = startPos;
        }
    
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                var player = other.gameObject.GetComponent<Player>();
                TriggerAction(player);
            }
        }

        protected virtual void TriggerAction(Player player)
        {
        
        }
    }
}
