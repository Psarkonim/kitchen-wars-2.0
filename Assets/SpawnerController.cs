using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;


public class SpawnerController : MonoBehaviour
{
    [SerializeField] private List<RatSpawner> spawners;
    [SerializeField] private List<RatSpawnEntry> spawnEntries;
    private float totalDelay;

    void Awake()
    {
        totalDelay = 0f;

        foreach (var entry in spawnEntries)
        {
            var spawner = GetWeightedRandomSpawner();

            totalDelay += entry.delayBeforeSpawn;

            entry.SetDelay(totalDelay - spawner.totalDelay);
            spawner.AddEntry(entry);
        }
    }

    private RatSpawner GetWeightedRandomSpawner()
    {
        float totalWeight = 0f;
        List<float> weights = new List<float>();

        foreach (var spawner in spawners)
        {
            float weight = 1f / (1f + spawner.waveQueue.Count);
            weights.Add(weight);
            totalWeight += weight;
        }

        float randomValue = Random.Range(0f, totalWeight);
        float currentWeightSum = 0f;

        for (int i = 0; i < spawners.Count; i++)
        {
            currentWeightSum += weights[i];
            if (randomValue <= currentWeightSum)
            {
                return spawners[i];
            }
        }

        return spawners[spawners.Count - 1];
    }

}
