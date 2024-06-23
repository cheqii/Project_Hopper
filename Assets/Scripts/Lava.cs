using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using ObjectPool;
using TilesScript;
using UnityEngine.Serialization;

public class Lava : MonoBehaviour
{
    [SerializeField] private float lavaIncreaseSpeed;
    [SerializeField] private float decreaseLavaValue;

    [SerializeField] private float duration;

    [SerializeField] private bool activateLava;

    [SerializeField] private Nf_GameEvent gameOverEvent;
    
    void Update()
    {
        if(GameManager._instance.player == null) return;
        IncreaseLavaByTime();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Melt"))
        {
            var tiles = other.GetComponent<FallingTile>();
            if(tiles.IsFalling)
                PoolManager.ReleaseObject(tiles.gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            gameOverEvent.Raise();
            other.gameObject.SetActive(false);
        }
    }

    private void IncreaseLavaByTime()
    {
        if(!activateLava) return;
        transform.position += Vector3.up * (lavaIncreaseSpeed * Time.deltaTime);
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
