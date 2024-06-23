using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using Interaction;
using UnityEngine.Serialization;

namespace Character
{
    public class Player : Character
    {
        [Header("Start Position")]
        [SerializeField] private Vector3 startPos;
        public Vector3 StartPos => startPos;
        
        [SerializeField] private float jumpForce;
        
        [Header("Input System")]
        private PlayerActionInput _control;
        public PlayerActionInput _Control => _control;

        [SerializeField] private LayerMask groundLayer;

        [SerializeField] private bool isGuard;
        public bool IsGuard => isGuard;

        [Range(0, 1)]
        [SerializeField] private float linePosY = 0.55f;

        [Header("Animator")]
        [SerializeField] private Animator animator;
        
        [Header("Facing Object")]
        [SerializeField] private InteractableObject facingObject;

        [SerializeField] private RoomState currentRoom = RoomState.NormalRoom;

        public RoomState CurrentRoom
        {
            get => currentRoom;
            set => currentRoom = value;
        }

        [Header("Player Action Event")]
        [SerializeField] private Nf_GameEvent jumpEvent;
        [SerializeField] private Nf_GameEvent gameOverEvent;

        private Rigidbody2D rb;
        
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
            _control.PlayerAction.Guard.performed += Guard;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("InteractableObject"))
                facingObject = GetInteractableFacingObject(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("InteractableObject"))
                facingObject = null;
        }

        #region **Player Action**

        private void Jump(InputAction.CallbackContext callback = default)
        {
            if (!PlayerCheckGround()) return;
            
            isGuard = false;
            jumpEvent.Raise();
            animator.SetTrigger("Jump");
            isGuard = false;
            
            rb.velocity = Vector2.up * jumpForce;
            
            if(currentRoom == RoomState.SecretRoom) return;
            GameManager._instance.UpdatePlayerScore(1);
        }
        
        private void Attack(InputAction.CallbackContext callback = default)
        {
            isGuard = false;
            animator.SetTrigger("Attack");
            isGuard = false;
            if(facingObject == null) return;
            facingObject.Interaction(attackDamage);
        }

        private void Guard(InputAction.CallbackContext callback = default)
        {
            animator.SetTrigger("IsGuard");
            isGuard = true;
            print("player is on guard");
        }

        #endregion

        public override void TakeDamage(int damage)
        {
            if(isGuard) return;
            base.TakeDamage(damage);
            animator.SetTrigger("Hurt");
            GameManager._instance.UpdatePlayerHealthUI(false);

            if (health > 0) return;
            Destroy(gameObject);
            gameOverEvent.Raise();
        }
        
        public void FullHeal(int value)
        {
            if(health >= maxHealth) return;
            health += value;
            GameManager._instance.UpdatePlayerHealthUI(true);
        }
        

        public bool PlayerCheckGround()
        {
            var startPos = transform.position + new Vector3(-0.4f, -linePosY);
            var endPos = transform.position + new Vector3(0.4f, -linePosY);
            return Physics2D.Linecast(startPos, endPos, groundLayer);
        }

        private void OnDrawGizmos()
        {
            var startPos = transform.position + new Vector3(-0.4f, -linePosY);
            var endPos = transform.position + new Vector3(0.4f, -linePosY);
            Gizmos.DrawLine(startPos, endPos);
        }

        private InteractableObject GetInteractableFacingObject(Component other = null)
        {
            if (other != null)
            {
                var interactable = other.GetComponent<InteractableObject>();
                return interactable;
            }
            return null;
        }
    }
}

public enum RoomState
{
    NormalRoom,
    SecretRoom
}
