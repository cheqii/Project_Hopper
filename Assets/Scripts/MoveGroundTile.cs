using Character;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveGroundTile : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float moveDuration = 1f;

    public void MoveTile()
    {
        if(player.PlayerInSecretRoom) return;
        var endPos = transform.position + Vector3.left;
        transform.DOLocalMove(endPos,  moveDuration);
    }

    public void MoveSecretRoomTile()
    {
        if(!player.PlayerInSecretRoom) return;
        var endPos = transform.position + Vector3.left;
        transform.DOLocalMove(endPos,  moveDuration);
    }
}
