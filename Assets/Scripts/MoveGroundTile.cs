using Character;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveGroundTile : MonoBehaviour
{
    [SerializeField] private float moveDuration = 1f;
    [SerializeField] private Player _player;

    private void Start()
    {
        _player._Control.PlayerAction.Jump.performed += MoveTile;
    }

    private void MoveTile(InputAction.CallbackContext callback)
    {
        if(!_player.IsGrounded) return;
        var endPos = transform.position + Vector3.left;
        transform.DOLocalMove(endPos,  moveDuration);
    }
}
