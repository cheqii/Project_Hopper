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
    [SerializeField] private GameObject groundPrefab;
    
    [SerializeField] private Transform parent;
    [SerializeField] private bool ableToGenerate;
    
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
        
        if (ableToGenerate)
        {
            for (int i = 0; i < targetStep; i++)
            {
                GenerateStep((currentStep + 1) + i);
            }
        }
    }

    public void GenerateStep(int step)
    {
        Vector3 position = new Vector3(step, 0, 0);
        var ground = Instantiate(groundPrefab, position, quaternion.identity, parent);
        ground.transform.localPosition = new Vector3(Mathf.FloorToInt(ground.transform.localPosition.x), ground.transform.position.y, 0f);
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
