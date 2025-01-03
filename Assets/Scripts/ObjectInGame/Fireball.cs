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
        [SerializeField] private Vector3 trackingPos;
        [SerializeField] private float warningTime;
        private WaitForSeconds _warning;
        
        [SerializeField] private bool isWarning;

        [Header("Warning Sign")]
        [SerializeField] private GameObject warningSign;

        [SerializeField] private Animator warningSignAnimator;

        private float camEdgeX;

        private void OnEnable()
        {
            SetToInitialObject(trackingPos);
        }

        // Start is called before the first frame update
        void Start()
        {
            _warning = new WaitForSeconds(warningTime);
            camEdgeX = Camera.main.ScreenToViewportPoint(Vector3.zero).x;
        }

        private void LateUpdate()
        {
            WarningTracking();
            
            FireballToPlayer();
                
            CheckObjectOutOfCameraLeftEdge();
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
        }

        protected override void TriggerAction(Player player)
        {
            base.TriggerAction(player);
            player.TakeDamage(value);
            PoolManager.ReleaseObject(gameObject);
        }
        
        private void CheckObjectOutOfCameraLeftEdge()
        {
            if (transform.position.x + 0.5f < camEdgeX)
            {
                PoolManager.ReleaseObject(gameObject);
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
        
        private Vector3 Tracking(float playerYPosition)
        {
            trackingPos.y = playerYPosition;
            return trackingPos;
        }

        private void WarningTracking()
        {
            if(_player == null) return;
            if(isWarning) return;
            transform.position = Tracking(_player.transform.position.y);
        }

        private void WarningEnd()
        {
            if (!isWarning)
                warningSign.SetActive(false);
            
            isWarning = true;
            SoundManager.Instance.PlaySFX("Fireball");
        }

        private void FireballToPlayer()
        {
            if (!isWarning) return;
            var moveXPos = transform.position + (Vector3.left * 2);
            transform.DOLocalMoveX(moveXPos.x, 2);
        }

        private IEnumerator PlayerTracking()
        {
            while (true)
            {
                yield return _warning;
                SoundManager.Instance.PlaySFX("WarningFireball");
                warningSignAnimator.SetTrigger("Flashing");

                yield return _warning;
                WarningEnd();
            }
        }
    }
}
