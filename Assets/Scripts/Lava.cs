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
        transform.DOLocalMove(endPos, 0);
    }

    public void DecreaseLava()
    {
        if(transform.position.y <= -10f) return;
        StartCoroutine(SmoothDecrease());
    }
    
    private IEnumerator SmoothDecrease()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos - Vector3.up * decreaseLavaValue;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
    }
}