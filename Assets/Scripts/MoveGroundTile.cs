using Character;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveGroundTile : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float moveDuration = 1f;
    [SerializeField] private RoomState tileInRoom;

    public void MoveTile()
    {
        if(player.CurrentRoom != tileInRoom) return;
        var endPos = transform.position + Vector3.left;
        transform.DOLocalMove(endPos,  moveDuration);
    }
}
