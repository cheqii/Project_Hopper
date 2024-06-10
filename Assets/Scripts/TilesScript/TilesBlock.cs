using System;
using System.Collections.Generic;
using Character;
using ObjectPool;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

namespace TilesScript
{
    public enum TilesType
    {
        Normal,
        Falling,
        Broken,
        TNT,
        Spear,
        Rock,
        Axe,
        Cloud,
        Door
    }

    public class TilesBlock : MonoBehaviour
    {
        private float camEdgeX;
        [SerializeField] private Player _player;
        public Player _Player
        {
            get => _player;
            set => _player = value;
        }

        [SerializeField] private TilesType type = TilesType.Normal;
        public TilesType Type => type;
        
        [SerializeField] protected float delay;

        [Header("Object layer")]
        [SerializeField] private GameObject objectOnTile;
        [SerializeField] private LayerMask objectLayer;
        
        [Header("Player layer")]
        [SerializeField] private GameObject playerOnTile;
        [SerializeField] private LayerMask playerLayer;

        // Start is called before the first frame update
        void Start()
        {
            camEdgeX = Camera.main.ScreenToViewportPoint(Vector3.zero).x;
            _player._Control.PlayerAction.Jump.performed += CheckObjectOutOfCameraLeftEdge;
        }

        private void CheckObjectOutOfCameraLeftEdge(InputAction.CallbackContext callback = default)
        {
            if (transform.position.x + 0.5f < camEdgeX)
            {
                PoolManager.ReleaseObject(gameObject);
                _player._Control.RemoveAllBindingOverrides();
            }
        }

        protected virtual void OnCollisionStay2D(Collision2D other)
        {
        
        }
        
        protected virtual void CheckPlayerOnTile()
        {
            var startPos = transform.position + new Vector3(-0.5f, 0.55f);
            var endPos = transform.position + new Vector3(0.5f, 0.55f);

            RaycastHit2D hit = Physics2D.Linecast(startPos, endPos, playerLayer);
            if (hit.collider != null)
                playerOnTile = hit.collider.gameObject;
            else
                playerOnTile = null;
        }

        protected virtual void CheckObjectOnTile()
        {
            var startPos = transform.position + new Vector3(-0.5f, 0.55f);
            var endPos = transform.position + new Vector3(0.5f, 0.55f);

            RaycastHit2D hit = Physics2D.Linecast(startPos, endPos, objectLayer);
            if (hit.collider != null)
                objectOnTile = hit.collider.gameObject;
            else 
                objectOnTile = null;
        }

        private void OnDrawGizmos()
        {
            var startPos = transform.position + new Vector3(-0.5f, 0.55f);
            var endPos = transform.position + new Vector3(0.5f, 0.55f);
            Gizmos.DrawLine(startPos, endPos);
        }

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            CheckPlayerOnTile();
            CheckObjectOnTile();
        }

        protected virtual void OnCollisionExit2D(Collision2D other)
        {
            CheckPlayerOnTile();
            CheckObjectOnTile();
        }
    }
}