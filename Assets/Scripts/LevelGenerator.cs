using System.Collections.Generic;
using Character;
using ObjectPool;
using ScriptableObjects;
using TilesScript;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Player _player;

    [Header("Generate Tile Attributes")]
    [Range(0, 10)]
    [SerializeField] private int retainStep = 7;

    [SerializeField] private GameObject normalTilePrefab;
    [SerializeField] private List<Tiles> allTiles;

    [SerializeField] private float tileMaxHeight = 0.2f;
    [SerializeField] private float currentHeight = 0f;

    [Header("Generate Monster")] 
    [SerializeField] private List<Tiles> monsterTile;
    private void Start()
    {
        _player._Control.PlayerAction.Jump.performed += GeneratePlatformByStep;
        for (int i = 0; i <= retainStep; i++)
        {
            GenerateTile(i, true);
        }
    }

    #region -Tile Generate Method-

    private void GeneratePlatformByStep(InputAction.CallbackContext callback = default)
    {
        if(!_player.PlayerCheckGround()) return;
        var step = retainStep;
        GenerateTile(++step);
    }

    private void GenerateTile(int step = default , bool initialGenerate = false)
    {
        var tilePrefab = normalTilePrefab;
        if (!initialGenerate)
        {
            if (currentHeight >= 0 || currentHeight < 0)
            {
                var check = (!(Random.value > 0.5f));
                if (!check)
                    currentHeight += 0;
                else                                                                         
                {
                    var heightDifference = (Random.value > 0.5f) ? tileMaxHeight : -tileMaxHeight;
                    currentHeight += heightDifference;
                }
            }

            tilePrefab = GetRandomTile();
        }

        var position = new Vector3(step, currentHeight, 0f);
        var newTile = PoolManager.SpawnObject(tilePrefab, RoundVector(position), Quaternion.identity);
        newTile.GetComponent<TilesBlock>()._Player = _player;
    }

    private GameObject GetRandomTile()
    {
        var totalChance = 0;
        foreach (var tiles in allTiles)
        {
            totalChance += tiles.generateChance;
        }

        var rand = Random.Range(0, totalChance);
        foreach (var tiles in allTiles)
        {
            if (rand < tiles.generateChance)
            {
                return tiles.prefab;
            }

            rand -= tiles.generateChance;
        }

        return normalTilePrefab;
    }

    #endregion

    private Vector3 RoundVector(Vector3 vector)
    {
        return new Vector3(
            Mathf.Round(vector.x * 10.0f) / 10.0f,
            Mathf.Round(vector.y * 10.0f) / 10.0f,
            Mathf.Round(vector.z * 10.0f) / 10.0f);
    }
}
