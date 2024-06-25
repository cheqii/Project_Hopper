using System;
using Character;
using DG.Tweening;
using Interface;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveGroundTile : MonoBehaviour
{
    [SerializeField] private float moveDuration = 0.5f;
    
    public virtual void MoveTileState(RoomState state)
    {
        var endPos = transform.position + Vector3.left;
        transform.DOLocalMove(endPos,  moveDuration);
    }
}
