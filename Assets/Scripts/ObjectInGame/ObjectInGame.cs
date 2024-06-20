using Character;
using ObjectPool;
using UnityEngine;
using UnityEngine.Serialization;

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
    
        [SerializeField] protected int value;
        public int Value => value;

        public virtual void SetToInitialObject(Vector3 startPos = default)
        {
            transform.position = startPos;
        }
    
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                TriggerAction(_player);
            }
        }

        protected virtual void TriggerAction(Player player)
        {
        
        }
    }
}
