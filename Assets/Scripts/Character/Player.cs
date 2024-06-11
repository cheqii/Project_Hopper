using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
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
        
        [Header("Facing Object")]
        [SerializeField] private InteractableObject facingObject;
        
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
            health = maxHealth;
            
            _control.PlayerAction.Jump.performed += Jump;
            _control.PlayerAction.Attack.performed += Attack;
            _control.PlayerAction.Guard.performed += Guard;
        }

        #region **Player Action**

        private void Jump(InputAction.CallbackContext callback = default)
        {
            if (!PlayerCheckGround()) return;

            isGuard = false;
            var tempPos = transform.position;
            transform.DOJump(tempPos, jumpForce, 0, .5f);
            GameManager._instance.UpdatePlayerScore(1);
        }

        private void Attack(InputAction.CallbackContext callback = default)
        {
            isGuard = false;
            if(facingObject == null) return;
            facingObject.InteractToObject(attackDamage);
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
            GameManager._instance.UpdatePlayerHealth();
        }

        public bool PlayerCheckGround()
        {
            var startPos = transform.position + new Vector3(-0.4f, -0.55f);
            var endPos = transform.position + new Vector3(0.4f, -0.55f);
            return Physics2D.Linecast(startPos, endPos, groundLayer);
        }

        private void OnDrawGizmos()
        {
            var startPos = transform.position + new Vector3(-0.4f, -0.55f);
            var endPos = transform.position + new Vector3(0.4f, -0.55f);
            Gizmos.DrawLine(startPos, endPos);
        }

        private InteractableObject GetInteractableFacingObject(Collider2D other = null)
        {
            if (other != null)
            {
                var interactable = other.GetComponent<InteractableObject>();
                return interactable;
            }
            return null;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("InteractableObject"))
            {
                facingObject = GetInteractableFacingObject(other);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("InteractableObject"))
                facingObject = null;
        }
    }
}
