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
        [SerializeField] private Vector3 startPos;
        [SerializeField] private float warningTime;
        private WaitForSeconds _warning;

        [SerializeField] private bool isWarning;

        [Header("Warning Sign")]
        [SerializeField] private GameObject warningSign;
        [SerializeField] private Animator warningSignAnimator;

        public float camEdgeX;

        private void OnEnable()
        {
            startPos.x = 5.25f;
            SetToInitialObject(startPos);
        }

        // Start is called before the first frame update
        void Start()
        {
            _warning = new WaitForSeconds(warningTime);
            camEdgeX = Camera.main.ScreenToViewportPoint(Vector3.zero).x;
        }

        private void LateUpdate()
        {
            if (isWarning)
            {
                var moveXPos = transform.position + (Vector3.left * 2);
                transform.DOLocalMoveX(moveXPos.x, 2);
                
                CheckObjectOutOfCameraLeftEdge();
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
        
        private void CheckObjectOutOfCameraLeftEdge()
        {
            if (transform.position.x + 0.5f < camEdgeX)
            {
                Destroy(gameObject);
            }
        }

        public override void SetToInitialObject(Vector3 startPos = default)
        {
            base.SetToInitialObject(startPos);
            isWarning = false;
            warningSign.SetActive(true);
            transform.SetParent(PoolManager.Instance.root.parent);
            StartCoroutine(PlayerTracking());
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
