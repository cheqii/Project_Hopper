using System.Collections.Generic;
using Character;
using ObjectPool;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [Range(0, 10)]
    [SerializeField] private int retainStep = 7;
    
    [SerializeField] private GameObject normalTilePrefab;
    [SerializeField] private List<Tiles> allTiles;

    [SerializeField] private float tileMaxHeight = 0.2f;
    [SerializeField] private float currentHeight = 0f;

    private int lastTileIndex;
    private int firstTileIndex;
    
    private Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        for (int i = 0; i <= retainStep; i++)
        {
            GenerateTile(i, true);
        }
        _player._Control.PlayerAction.Jump.performed += GeneratePlatformByStep;
    }

    private void GeneratePlatformByStep(InputAction.CallbackContext callback = default)
    {
        if(!_player.IsGrounded) return;
        var stepCount = _player.StepCount;
        if (stepCount >= 4)
        {
            print("test = 4");
            GenerateTile(++stepCount);
            GenerateTile(++stepCount);
            ReturnOldTile();
        }
    }

    private void GenerateTile(int step = default , bool initialGenerate = false)
    {
        var tilePrefab = normalTilePrefab;
        if (!initialGenerate)
        {
            var heightDifference = (Random.value > 0.5f) ? tileMaxHeight : -tileMaxHeight;
            currentHeight += heightDifference;

            tilePrefab = GetRandomTile();
        }

        var position = new Vector3(step, currentHeight, 0f);
        var newTile = PoolManager.SpawnObject(tilePrefab, RoundVector(position), Quaternion.identity);
    }

    private void ReturnOldTile()
    {
        if (lastTileIndex - firstTileIndex > retainStep)
        {
            var oldTile = PoolManager.Instance.root.GetChild(0);
            PoolManager.ReleaseObject(oldTile.gameObject);
            firstTileIndex++;
        }
    }

    private GameObject GetRandomTile()
    {
        // var totalChance = 0;
        // foreach (var tiles in allTiles)
        // {
        //     totalChance += tiles.generateChance;
        // }
        //
        // var rand = Random.Range(0, totalChance);
        // print($"random = {rand}");
        // print($"total chance = {totalChance}");
        // foreach (var tiles in allTiles)
        // {
        //     print("hello world");
        //     if (totalChance < tiles.generateChance)
        //     {
        //         print("is this random???");
        //         return tiles.prefab;
        //     }
        //     
        //     rand -= tiles.generateChance;
        // }
        //
        
        return normalTilePrefab;
    }

    private Vector3 RoundVector(Vector3 vector)
    {
        return new Vector3(
            Mathf.Round(vector.x * 10.0f) / 10.0f,
            Mathf.Round(vector.y * 10.0f) / 10.0f,
            Mathf.Round(vector.z * 10.0f) / 10.0f);
    }
}
