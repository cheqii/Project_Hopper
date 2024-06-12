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

        [Header("Player Action Event")]
        [SerializeField] private Nf_GameEvent jumpEvent;

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

            jumpEvent.Raise();
            animator.SetTrigger("Jump");
            isGuard = false;
            
            rb.velocity = Vector2.up * jumpForce;
            GameManager._instance.UpdatePlayerScore(1);
            
        }
        
        // void ToNex

        private void Attack(InputAction.CallbackContext callback = default)
        {
            animator.SetTrigger("Attack");
            isGuard = false;
            if(facingObject == null) return;
            facingObject.Interaction(attackDamage);
        }

        private void Guard(InputAction.CallbackContext callback = default)
        {
            isGuard = true;
            print("player is on guard");
        }

        #endregion

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            animator.SetTrigger("Hurt");
            GameManager._instance.UpdatePlayerHealth();
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
