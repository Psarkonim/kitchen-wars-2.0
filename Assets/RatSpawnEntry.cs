using System;
using UnityEngine;

[Serializable]
public struct RatSpawnEntry
{
    [Tooltip("Префаб мыши")] public GameObject ratPrefab;
    [Tooltip("Задержка до спавном")] public float delayBeforeSpawn;

    public float SetDelay(float delay) => delayBeforeSpawn = delay;
}
