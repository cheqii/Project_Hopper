using UnityEngine;

namespace Character
{
    public class Player : Character
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private float jumpForce;
        [SerializeField] private bool isGrounded;
        private Rigidbody2D rb;

        [SerializeField] private int stepCount;
        public int StepCount
        {
            get => stepCount;
            set => stepCount = value;
        }

        
        private MoveGroundTile groundTile;
        private PlayerActionInput _control;

        public LevelGenerator level;
        private void Awake()
        {
            _control = new PlayerActionInput();
            groundTile = FindObjectOfType<MoveGroundTile>();
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
            // groundTile
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void Jump(Vector2 direction)
        {
            if (isGrounded)
            {
                rb.velocity = direction * jumpForce;
                stepCount++;
                StartCoroutine(groundTile.MoveTile());
                StartCoroutine(level.CheckCurrentPlayerStep());
            }
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
        }

        private void OnCollisionEnter2D(Collision2D other)
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
