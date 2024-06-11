using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class Lava : MonoBehaviour
{
    [SerializeField] private float lavaIncreaseSpeed;
    [SerializeField] private float decreaseLavaValue;

    [SerializeField] private float duration;
    
    void Update()
    {
        IncreaseLavaByTime();
    }

    private void IncreaseLavaByTime()
    {
        var endPos = transform.position += Vector3.up * (lavaIncreaseSpeed * Time.deltaTime);
        transform.DOLocalMove(endPos, duration);
    }

    public void DecreaseLava()
    {
        if(transform.position.y <= -10f) return;
        var endPos = transform.position += Vector3.down * decreaseLavaValue;
        transform.DOLocalMove(endPos, duration);
    }
}
