using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

namespace Character
{
    public class Player : Character
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private float jumpForce;
        [SerializeField] private bool isGrounded;
        public bool IsGrounded
        {
            get => isGrounded;
            set => isGrounded = value;
        }

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

        [SerializeField] private LayerMask groundLayer;

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
            _control.PlayerAction.Attack.performed += Attack;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void Jump(InputAction.CallbackContext callback)
        {
            if (!PlayerCheckGround()) return;
            
            var tempPos = transform.position;
            transform.DOJump(tempPos, jumpForce, 0, .5f);
            stepCount++;
            GameManager._instance.UpdatePlayerScore(1);
        }
        
        public bool PlayerCheckGround()
        {
            var startPos = transform.position + new Vector3(-0.5f, -0.55f);
            var endPos = transform.position + new Vector3(0.5f, -0.55f);
            return Physics2D.Linecast(startPos, endPos, groundLayer);
        }

        private void OnDrawGizmos()
        {
            var startPos = transform.position + new Vector3(-0.5f, -0.55f);
            var endPos = transform.position + new Vector3(0.5f, -0.55f);
            Gizmos.DrawLine(startPos, endPos);
        }

        private void Attack(InputAction.CallbackContext callback)
        {
            print("attack");
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            GameManager._instance.UpdatePlayerHealth();
        }
    }
}
