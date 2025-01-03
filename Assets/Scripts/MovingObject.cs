using System.Collections;
using DG.Tweening;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private float interval; 
    private WaitForSeconds _movingInterval;
    
    [Header("Position to move rock")]
    [SerializeField] private float yTopPosition;
    [SerializeField] private float yBottomPosition;

    [Space]
    [SerializeField] private bool atTop;
    public bool AtTop
    {
        get => atTop;
        set => atTop = value;
    }
    
    [SerializeField] private bool atBottom;
    public bool AtBottom
    {
        get => atBottom;
        set => atBottom = value;
    }
    
    private void OnEnable()
    {
        StartCoroutine(LoopBehavior());
    }

    private void Start()
    {
        _movingInterval = new WaitForSeconds(interval);
    }

    private void MoveObjectDown()
    {
        transform.DOLocalMoveY(yBottomPosition, interval);
        atTop = false;
        atBottom = true;
    }

    private void MoveObjectUp()
    {
        transform.DOLocalMoveY(yTopPosition, interval);
        atTop = true;
        atBottom = false;
    }

    private IEnumerator LoopBehavior()
    {
        while (true)
        {
            MoveObjectDown();
            yield return _movingInterval;
            
            if(!atBottom) continue;
            MoveObjectUp();
            yield return _movingInterval;
        }
    }
}