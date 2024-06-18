using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmitter : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform parentToSet;
    private ParticleSystem _particle;

    public List<ParticleSystem.Particle> ExitPfx { get; } = new List<ParticleSystem.Particle>();

    // Start is called before the first frame update
    void Start()
    {
        _particle = GetComponent<ParticleSystem>();
    }

    private void OnParticleTrigger()
    {
        _particle.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, ExitPfx);
        foreach (var particle in ExitPfx)
        {
            var spawnObject = Instantiate(prefab, parentToSet, true);
            spawnObject.transform.position = transform.TransformPoint(particle.position);
        }
    }
}
