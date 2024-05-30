using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
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
        [SerializeField] private GroundTile groundTile;

        [SerializeField] private int stepCount;
        public int StepCount
        {
            get => stepCount;
            set => stepCount = value;
        }

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
            groundTile = FindObjectOfType<GroundTile>();
            
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
                
                StartCoroutine(groundTile.MoveGroundTile());
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
        
        // private IEnumerator MoveGroundTile()
        // {
        //     var startPos = groundTile.transform.position;
        //     var endPos = startPos + Vector3.left;
        //
        //     float elapsedTime = 0;
        //     
        //     while (elapsedTime < moveDuration)
        //     {
        //         groundTile.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / moveDuration);
        //         elapsedTime += Time.deltaTime * tileMoveSpeed;
        //         yield return null;
        //     }
        //
        //     groundTile.transform.position = endPos;
        // }
    }
}
