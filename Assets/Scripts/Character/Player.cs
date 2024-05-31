using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class Player : Character
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private float jumpForce;
        [SerializeField] private bool isGrounded;
        public bool IsGrounded => isGrounded;
        
        [Space]
        [SerializeField] private int stepCount;
        public int StepCount
        {
            get => stepCount;
            set => stepCount = value;
        }

        private Rigidbody2D rb;

        [Header("Input System")]
        private PlayerActionInput _control;
        public PlayerActionInput _Control => _control;

        public MoveGroundTile moveTile;
        
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
            
            _control.PlayerAction.Jump.performed += Jump;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void Jump(InputAction.CallbackContext callback)
        {
            if (!isGrounded) return;
            rb.velocity = Vector2.up * jumpForce;
            stepCount++;
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                isGrounded = false;
            }
        }
    }
}
