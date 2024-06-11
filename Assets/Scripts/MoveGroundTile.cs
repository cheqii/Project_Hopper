using Character;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveGroundTile : MonoBehaviour
{
    [SerializeField] private float moveDuration = 1f;

    private void Start()
    {
        // _player._Control.PlayerAction.Jump.performed += MoveTile;
    }

    public void MoveTile()
    {
        var endPos = transform.position + Vector3.left;
        transform.DOLocalMove(endPos,  moveDuration);
    }
}
