using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
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

        _player._Control.PlayerAction.Jump.performed += GeneratePlatform;
    }

    private void Update()
    {
        // GeneratePlatform();
    }

    private void GeneratePlatform(InputAction.CallbackContext callback)
    {
        if(!_player.IsGrounded) return;
        if (_player.StepCount % 4 == 0 && _player.StepCount != 0)
        {
            for (int i = 0; i < retainStep; i++)
            {
                GenerateStep((retainStep - targetStep) + i);
            }
        }
    }

    private void GenerateStep(int step = default, float height = default)
    {
        var tile = ObjectPool.Instance.GetPooledObject();
        if (tile == null) return;
        Vector3 position = new Vector3(step, height, 0);
        tile.transform.position = position;
        tile.SetActive(true);
    }
}
