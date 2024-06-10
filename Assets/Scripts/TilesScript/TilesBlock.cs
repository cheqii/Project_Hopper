using Character;
using ObjectPool;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TilesScript
{
    public enum TilesType
    {
        Normal,
        Falling,
        Broken,
        TNT,
        Spear,
        Rock,
        Axe,
        Cloud,
        Door
    }

    public class TilesBlock : MonoBehaviour
    {
        private float camEdgeX;
        private Player _player;
        private GameObject playerObject;

        [SerializeField] private TilesType type = TilesType.Normal;
        public TilesType Type => type;
        
        [SerializeField] private float delay;

        private void Awake()
        {
            if (playerObject != null) return;
            playerObject = GameObject.FindGameObjectWithTag("Player");
        }

        // Start is called before the first frame update
        void Start()
        {
            camEdgeX = Camera.main.ScreenToViewportPoint(Vector3.zero).x;
            _player = playerObject.GetComponent<Player>();
            _player._Control.PlayerAction.Jump.performed += CheckObjectOutOfCameraLeftEdge;
        }

        protected virtual void CheckObjectOutOfCameraLeftEdge(InputAction.CallbackContext callback = default)
        {
            if (transform.position.x + 0.5f < camEdgeX)
            {
                PoolManager.ReleaseObject(gameObject);
            }
        }

        protected virtual void OnCollisionStay2D(Collision2D other)
        {
        
        }

    }
}