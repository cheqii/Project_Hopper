using System;
using System.Collections.Generic;
using Character;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [Range(0, 10)]
    [SerializeField] private int retainStep = 7;
    [Range(0, 10)]
    [SerializeField] private int currentStep = 4;
    [Range(0, 10)]
    [SerializeField] private int nextStep = 4;
    [Range(0, 10)]
    [SerializeField] private int targetStep = 4;

    [Header("Tilemap")]
    [SerializeField] private Tilemap groundTile;
    [SerializeField] private Tile tile;
    [SerializeField] private GameObject groundPrefab;

    [SerializeField] private List<GameObject> groundList = new List<GameObject>();

    private Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        for (int i = 0; i <= retainStep; i++)
        {
            GenerateStep(i);
            // SpawnTile(i);
        }
    }

    private void Update()
    {
        
    }

    private void GeneratePlatform()
    {
        if (_player.StepCount % 4 == 0 && _player.StepCount != 0)
        {
            
        }
    }

    private void GenerateStep(int step)
    {
        // Vector3Int position = new Vector3Int(_player.StepCount, step, 0);
        // Vector3Int position = new Vector3Int(step, 0, 0);
        Vector3 position = new Vector3(step, -1, 0);
        // groundTile.SetTile(position, tile);
        
        var ground = Instantiate(groundPrefab, position, quaternion.identity, groundTile.transform);
        groundList.Add(ground);
    }
}
