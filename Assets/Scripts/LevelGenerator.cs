using System;
using System.Collections;
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
    [SerializeField] private Transform parent;
    private Player _player;

    private Vector3 tempTilePos;

    [SerializeField] private bool ableToGenerate;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        for (int i = 0; i <= retainStep; i++)
        {
            GenerateStep(i);
        }
        
        // GenerateStep(retainStep);
    }

    private void Update()
    {
        // GeneratePlatform();
    }

    private void FixedUpdate()
    {
        GeneratePlatform();
    }


    void GenerateNextStep()
    {
        
    }

    private void GeneratePlatform()
    {
        // if (currentStep == targetStep && retainStep > 0)
        // {
        //     nextStep = _player.StepCount;
        //     retainStep--;
        // }
        // else if (currentStep == targetStep && retainStep <= 0)
        // {
        //     var rand = Random.Range(0, 1);
        //     if (rand <= 0)
        //         targetStep = currentStep - Random.Range(1, 3);
        //     else
        //         targetStep = currentStep + Random.Range(1, 3);
        //
        //     if (targetStep > 8)
        //         targetStep = 8;
        //     else if (targetStep < 1)
        //         targetStep = 1;
        //     retainStep = Random.Range(1, 5);
        // }
        //
        // if (currentStep != targetStep)
        // {
        //     if (currentStep < targetStep)
        //         nextStep++;
        //     else
        //         nextStep--;
        // }

        // StartCoroutine(CheckCurrentPlayerStep());
        if (ableToGenerate)
        {
            // GenerateStep(4);
            for (int i = 0; i <= retainStep; i++)
            {
                GenerateStep(_player.StepCount + i);
            }
        }
    }

    public void GenerateStep(int step)
    {
        Vector3 position = new Vector3((int)step, -1, 0);
        Instantiate(groundPrefab, position, quaternion.identity, parent);
    }

    public IEnumerator CheckCurrentPlayerStep()
    {
        if (_player.StepCount % 4 == 0 && _player.StepCount != 0)
        {
            print("check player step");
            ableToGenerate = true;
            yield return new WaitForFixedUpdate();
            ableToGenerate = false;
        }
        else ableToGenerate = false;
    }
}
