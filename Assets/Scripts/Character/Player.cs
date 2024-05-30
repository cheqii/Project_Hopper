using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Character
{
    public class Player : Character
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private float jumpForce;
        [SerializeField] private bool isGrounded;
        private Rigidbody2D rb;

        [Header("Tilemaps")]
        [SerializeField] private Tilemap groudTile;

        [SerializeField] private Color32 normalHeart;
        [SerializeField] private Color32 blankHeath;

        private PlayerActionInput _control;

        private void Awake()
        {
            _control = new PlayerActionInput();
        }

        private void OnEnable()
        {
            _control.Enable();
        }

        private void OnDisable()
        {
            
            _control.Disable();
        }

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            
            health = maxHealth;

            _control.PlayerAction.Action.performed += ctx => Jump(ctx.ReadValue<Vector2>());
        }

        // Update is called once per frame
        void Update()
        {
            // if (Input.GetKeyDown(KeyCode.Space))
            // {
            //     print("jump");
            //     rb.velocity = (Vector2.up * jumpForce);
            // }
            //
            // if (Input.GetKeyDown(KeyCode.O))
            // {
            //     print("input here");
            // }
        }

        private void GetInput() // call this in update function but physics in fixed updated
        {
            
        }

        private void Jump(Vector2 direction)
        {
            if (CanMove(direction))
            {
                transform.position += (Vector3) direction;
                print("Jump");
            }
            // rb.velocity = (Vector2.up * jumpForce);
        }

        private bool CanMove(Vector2 direction)
        {
            Vector3Int gridPos = groudTile.WorldToCell(transform.position + (Vector3) direction);
            
            if(!groudTile.HasTile(gridPos))
               return false;

            return true;
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
        }
    }
}
