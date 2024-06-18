using System.Collections.Generic;
using Character;
using Character.Monster;
using ObjectPool;
using ScriptableObjects;
using TilesScript;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class LevelGenerator : ObjectPool.Singleton<LevelGenerator>  
{
    [SerializeField] private Player _player;

    [Header("Generate Tile Attributes")]
    [Range(0, 10)]
    [SerializeField] private int retainStep = 7;

    [SerializeField] private GameObject normalTilePrefab;
    [SerializeField] private List<Tiles> allTiles;

    [SerializeField] private float tileMaxHeight = 0.2f;
    [SerializeField] private float currentHeight = 0f;

    [SerializeField] private float limitHeight = 2.8f;

    [Header("Secret Room Generate Tile")]
    [SerializeField] private GameObject fallingTile;
    [SerializeField] private GameObject exitDoor;
    [SerializeField] private Transform secretRoomParent;

    [Header("Generate Monster")] 
    [SerializeField] private List<MonsterData> allMonsters;

    private void Start()
    {
        for (int i = 0; i <= retainStep; i++)
        {
            GenerateTile(i, true);
        }
    }

    #region -Tile Generate Method-

    public void GeneratePlatformByStep()
    {
        if(!_player.PlayerCheckGround()) return;
        if(_player.PlayerInSecretRoom) return;
        var step = retainStep;
        GenerateTile(8);
    }

    private void GenerateTile(int step , bool initialGenerate = false)
    {
        var tilePrefab = normalTilePrefab;
        if (!initialGenerate)
        {
            var tileHeightPossibility = (!(Random.value > 0.5f));
            
            if (!tileHeightPossibility)
                currentHeight += 0;
            else                                                                         
                currentHeight = CheckMaxAndMinHeight(currentHeight);

            tilePrefab = GetRandomTile();
        }

        var position = new Vector3(step, currentHeight, 0f);
        var newTile = PoolManager.SpawnObject(tilePrefab, RoundVector(position), Quaternion.identity);
        newTile.transform.SetParent(PoolManager.Instance.root);
        
        var tile = newTile.GetComponent<TilesBlock>();

        tile._Player = _player;
        tile.SetToInitialTile(position);

        if(!initialGenerate)
            GenerateMonsterOnTile(position, newTile);
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

    private float CheckMaxAndMinHeight(float tileHeight)
    {
        var heightDifference = (Random.value > 0.5f) ? tileMaxHeight : -tileMaxHeight;
        switch (tileHeight)
        {
            case >= 2.8f:
                tileHeight += -tileMaxHeight;
                break;
            case <= -2.8f:
                tileHeight += tileMaxHeight;
                break;
            default:
                tileHeight += heightDifference;
                break;
        }

        return tileHeight;
    }

    public void GenerateTileSecretRoom(float doorYPos = default, Vector3 doorCurrentTransform = default, GameObject startDoorTileGameObject = default)
    {
        if(!_player.PlayerInSecretRoom) return;
        var randomTile = Random.Range(3, retainStep);

        var enterRoomTile = PoolManager.SpawnObject(startDoorTileGameObject, RoundVector(doorCurrentTransform), Quaternion.identity);
        enterRoomTile.transform.SetParent(secretRoomParent);

        var enterDoorTile = enterRoomTile.GetComponent<TilesBlock>();
        enterDoorTile._Player = _player;
        
        for (int i = 2; i <= randomTile; i++)
        {
            var tileHeightPossibility = (!(Random.value > 0.5f));
            
            if (!tileHeightPossibility)
                doorYPos += 0;
            else                                                                         
                doorYPos = CheckMaxAndMinHeight(doorYPos);

            var newTile = PoolManager.SpawnObject(fallingTile, RoundVector(new Vector3(i, doorYPos, 0f)), Quaternion.identity);
            newTile.transform.SetParent(secretRoomParent);
            
            var tile = newTile.GetComponent<TilesBlock>();
            tile._Player = _player;
        }

        var exitRoomTile = PoolManager.SpawnObject(exitDoor, RoundVector(new Vector3(randomTile + 1, doorYPos, 0f)), Quaternion.identity);
        exitRoomTile.transform.SetParent(secretRoomParent);
        
        var doorTile = exitRoomTile.GetComponent<TilesBlock>();
        doorTile._Player = _player;
    }

    #endregion

    #region -Monster Generate On Tile Method-

    private void GenerateMonsterOnTile(Vector3 position = default, GameObject tiles = null)
    {
        if (tiles != null)
        {
            var tileCheck = tiles.GetComponent<TilesBlock>();
            var checkForGenerate = (!(Random.value > 0.85f));

            if(tileCheck.Type != TilesType.Normal || tileCheck.ObjectOnTile != null) return;
            if(checkForGenerate) return;
            
            var monsterPos = RoundVector(new Vector3(tiles.transform.position.x, position.y + 1));
            var newMonster = PoolManager.SpawnObject(GetRandomMonster(), RoundVector(monsterPos), Quaternion.identity);

            var monster = newMonster.GetComponent<Monster>();

            monster.SetToInitialMonster(monsterPos);
            newMonster.transform.SetParent(tiles.transform);
        }
    }

    private GameObject GetRandomMonster()
    {
        var totalChance = 0;
        foreach (var tiles in allMonsters)
        {
            totalChance += tiles.generateChance;
        }

        var rand = Random.Range(0, totalChance);
        foreach (var tiles in allMonsters)
        {
            if (rand < tiles.generateChance)
            {
                return tiles.prefab;
            }

            rand -= tiles.generateChance;
        }
        
        return allMonsters[0].prefab;
    }

    #endregion

    #region -Generate Objects-

    private void GenerateObjectOnTile()
    {
        
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
