using System.Collections.Generic;
using Character;
using ObjectPool;
using ScriptableObjects;
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
        if(!_player.IsGrounded) return;
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

            tilePrefab = GetRandomTile(allTiles);
        }

        var position = new Vector3(step, currentHeight, 0f);
        var newTile = PoolManager.SpawnObject(tilePrefab, RoundVector(position), Quaternion.identity);
        
        // if(newTile.transform.childCount != 0)
        //     Destroy(newTile.transform.GetChild(0).gameObject);
        
        if(initialGenerate) return;
        var monsterPos = new Vector3(position.x, position.y + 1);
        var monster = Instantiate(GetRandomTile(monsterTile), RoundVector(monsterPos), Quaternion.identity, newTile.transform);
    }

    private GameObject GetRandomTile(List<Tiles> tilesList = default)
    {
        var totalChance = 0;
        if (tilesList != null)
        {
            foreach (var tiles in tilesList)
            {
                totalChance += tiles.generateChance;
            }

            var rand = Random.Range(0, totalChance);
            foreach (var tiles in tilesList)
            {
                if (rand < tiles.generateChance)
                {
                    return tiles.prefab;
                }

                rand -= tiles.generateChance;
            }
        }

        return null;
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
