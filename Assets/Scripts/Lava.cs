using System;
using System.Collections;
using Character;
using UnityEngine;
using DG.Tweening;
using Interface;
using ObjectPool;
using TilesScript;
using Unity.VisualScripting;
using UnityEngine.Serialization;

public class Lava : MonoBehaviour
{
    private Player _player;
    [SerializeField] private Vector3 lavaStartPos;
    [SerializeField] private float lavaIncreaseSpeed;
    [SerializeField] private float decreaseLavaValue;

    [SerializeField] private float duration;

    [SerializeField] private bool activateLava;

    [SerializeField] private Nf_GameEvent gameOverEvent;

    private void Start()
    {
        _player = GameManager._instance.player;
        _player._Control.PlayerAction.Jump.performed += context => DecreaseLava();
    }

    void Update()
    {
        if(GameManager._instance.player == null) return;
        IncreaseLavaByTime();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Melt"))
        {
            // var tiles = other.GetComponent<FallingTile>();
            // tiles.ReleaseTile();
            PoolManager.ReleaseObject(other.gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySFX("Dead");
            other.gameObject.SetActive(false);
            GameManager._instance.OnGameOver();
        }
    }

    private void IncreaseLavaByTime()
    {
        if(!activateLava) return;
        transform.position += Vector3.up * (lavaIncreaseSpeed * Time.deltaTime);
    }

    public void DecreaseLava()
    {
        if(GameManager._instance.player.CurrentRoom == RoomState.SecretRoom) return;
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

    public void ResetLava()
    {
        transform.position = lavaStartPos;
    }
}
