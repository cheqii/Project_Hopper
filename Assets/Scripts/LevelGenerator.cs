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
    }

    private void Update()
    {
        GeneratePlatform();
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
        
        if (_player.StepCount % 4 == 0 && _player._Control.PlayerAction.Jump.WasPressedThisFrame())
        {
            for (int i = 0; i < retainStep; i++)
            {
                GenerateStep(currentStep + i);
            }
        }
    }

    public void GenerateStep(int step = default, float height = default)
    {
        var tile = ObjectPool.Instance.GetPooledObject();
        if (tile == null) return;
        Vector3 position = new Vector3(step, height, 0);
        tile.transform.position = position;
        tile.SetActive(true);
    }
}
