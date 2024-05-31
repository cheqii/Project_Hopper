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
    // Update is called once per frame
    void Update()
    {
        MoveTile();
    }

    private void MoveTile()
    {
        var endPos = transform.position + Vector3.left;
        if(_player._Control.PlayerAction.Jump.WasPressedThisFrame())
            transform.DOLocalMove(endPos,  moveDuration);
    }

    public void ResetPositon()
    {
        transform.position = Vector3.zero;
    }
}
