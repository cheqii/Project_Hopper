using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using ObjectPool;
using TilesScript;

public class Lava : MonoBehaviour
{
    [SerializeField] private float lavaIncreaseSpeed;
    [SerializeField] private float decreaseLavaValue;

    [SerializeField] private float duration;

    [SerializeField] private bool activateLava;
    
    void Update()
    {
        IncreaseLavaByTime();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Melt"))
        {
            var tile = other.GetComponent<TilesBlock>();
            tile.ReleaseTile();
        }
        if (other.gameObject.CompareTag("Player"))
            Destroy(other.gameObject);
    }

    private void IncreaseLavaByTime()
    {
        if(!activateLava) return;
        var endPos = transform.position += Vector3.up * (lavaIncreaseSpeed * Time.deltaTime);
        transform.DOLocalMove(endPos, 0);
    }

    public void DecreaseLava()
    {
        if(transform.position.y <= -9f) return;
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
