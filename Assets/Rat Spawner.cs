using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSpawner : MonoBehaviour
{
    [Serializable]
    public struct RatSpawnEntry
    {
        [Tooltip("Префаб мыши")] public GameObject ratPrefab;
        [Tooltip("Задержка до спавном")] public float delayBeforeSpawn;
    }

    [Header("Wave Settings")]
    [SerializeField] private List<RatSpawnEntry> waveQueue = new();
    
    [Header("Controls")]
    [SerializeField] private bool autoStart = true;
    [SerializeField] private bool loopWave = false;

    private bool _isSpawning;

    private void Start()
    {
        if (autoStart) StartSpawning();
    }

    private void StartSpawning()
    {
        if (!_isSpawning && waveQueue.Count > 0) StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
        _isSpawning = false;
    }

    private IEnumerator SpawnRoutine()
    {
        _isSpawning = true;
        do
        {
            foreach (var entry in waveQueue)
            {
                if (entry.delayBeforeSpawn > 0) 
                    yield return new WaitForSeconds(entry.delayBeforeSpawn);

                if (entry.ratPrefab) 
                    Instantiate(entry.ratPrefab, transform.position, Quaternion.identity);
            }
        } while (loopWave);
        _isSpawning = false;
    }
}