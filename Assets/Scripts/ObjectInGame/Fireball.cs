using System;
using System.Collections;
using Character;
using DG.Tweening;
using ObjectPool;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ObjectInGame
{
    public class Fireball : ObjectInGame
    {
        [SerializeField] private float warningTime;
        private WaitForSeconds _warning;

        [SerializeField] private bool isWarning;

        [Header("Warning Sign")]
        [SerializeField] private GameObject warningSign;
        [SerializeField] private Animator warningSignAnimator;

        private float camEdgeX;

        private void OnEnable()
        {
            StartCoroutine(PlayerTracking());
        }

        // Start is called before the first frame update
        void Start()
        {
            _warning = new WaitForSeconds(warningTime);
            camEdgeX = Camera.main.ScreenToViewportPoint(Vector3.zero).x;
            _player._Control.PlayerAction.Jump.performed += CheckObjectOutOfCameraLeftEdge;
        }

        private void LateUpdate()
        {
            if (isWarning)
            {
                // fire to player with smooth transition
                var moveXPos = transform.position + (Vector3.left * 2);
                transform.DOLocalMoveX(moveXPos.x, 2);
            }
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
        }

        protected override void TriggerAction(Player player)
        {
            base.TriggerAction(player);
            player.TakeDamage(damage);
        }
        
        private void CheckObjectOutOfCameraLeftEdge(InputAction.CallbackContext callback = default)
        {
            if (transform.position.x + 0.5f < camEdgeX)
            {
                PoolManager.ReleaseObject(gameObject);
                _player._Control.RemoveAllBindingOverrides();
            }
        }

        private void FireballToPlayer()
        {
            if (!isWarning)
            {
                var lastPlayerPos = warningSign.transform.position.y;
                transform.DOLocalMoveY(lastPlayerPos, 0f);
                warningSign.SetActive(false);
            }

            // var moveXPos = transform.position + (Vector3.left * 2);
            // transform.DOLocalMoveX(moveXPos.x, fireballMoveTime);
            isWarning = true;
        }

        private IEnumerator PlayerTracking()
        {
            // warningSign.SetActive(true);
            while (true)
            {
                yield return _warning;
                warningSignAnimator.SetTrigger("Flashing");

                yield return _warning;
                FireballToPlayer();
            }
        }
    }
}
