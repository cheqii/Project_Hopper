using System;
using System.Collections;
using Character;
using DG.Tweening;
using UnityEngine;

namespace CollectableItems
{
    public class MovingCoin : CollectableItem
    {
        [SerializeField] private float interval;
        private WaitForSeconds _movingInterval;

        [SerializeField] private float yTopPosition;
        [SerializeField] private float yBottomPosition;

        [SerializeField] private bool atTop;
        [SerializeField] private bool atBottom;

        private void OnEnable()
        {
            StartCoroutine(LoopBehavior());
        }

        private void Start()
        {
            _movingInterval = new WaitForSeconds(interval);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
        }

        protected override void TriggerAction(Player player)
        {
            base.TriggerAction(player);
            print("get coin");
            Destroy(gameObject);
            GameManager._instance.UpdatePlayerScore(value);
        }
        
        private void MoveCoinDown()
        {
            transform.DOLocalMoveY(yBottomPosition, 1);
            atTop = false;
            atBottom = true;
        }

        private void MoveCoinUp()
        {
            transform.DOLocalMoveY(yTopPosition, 1);
            atTop = true;
            atBottom = false;
        }

        private IEnumerator LoopBehavior()
        {
            while (true)
            {
                MoveCoinDown();
                yield return _movingInterval;

                if(!atBottom) continue;
                MoveCoinUp();
                yield return _movingInterval;
            }
        }
    }
}
