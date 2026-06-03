using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSpawner : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] public List<RatSpawnEntry> waveQueue = new();
    public float totalDelay;
    
    [Header("Controls")]
    [SerializeField] private bool autoStart = true;
    [SerializeField] private bool loopWave = false;

    private bool _isSpawning;
    public bool IsSpawnEnd => !_isSpawning;

    private void Start()
    {
        if (autoStart) StartSpawning();
    }

    public void AddEntry(RatSpawnEntry entry)
    {
        waveQueue.Add(entry);
        totalDelay += entry.delayBeforeSpawn;
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