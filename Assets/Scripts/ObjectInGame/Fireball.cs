using Character;
using UnityEngine;

namespace ObjectInGame
{
    public class Fireball : ObjectInGame
    {
        [SerializeField] private Player player;

        public Player Player
        {
            get => player;
            set => player = value;
        }

        [SerializeField] private float warningTime;
        private WaitForSeconds _warning;
        // Start is called before the first frame update
        void Start()
        {
            _warning = new WaitForSeconds(warningTime);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
