using System.Collections;
using System.Collections.Generic;
using Character;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveGroundTile : MonoBehaviour
{
    [SerializeField] private float moveDuration = 1f;
    [SerializeField] private Player _player;
    
    void Update()
    {
        MoveTile();
    }

    private void MoveTile()
    {
        var endPos = transform.position + Vector3.left;
        if(_player._Control.PlayerAction.Jump.WasPressedThisFrame() && _player.IsGrounded)
            transform.DOLocalMove(endPos,  moveDuration);
    }
}
