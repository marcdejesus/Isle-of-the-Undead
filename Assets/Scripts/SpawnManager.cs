using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    private Dictionary<string, EnemySpawner> spawnerMap;
    private int currentWaveTotalEnemies = 0;
    private int currentWaveDefeatedEnemies = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        spawnerMap = new Dictionary<string, EnemySpawner>();
        EnemySpawner[] spawners = FindObjectsOfType<EnemySpawner>();
        foreach (var spawner in spawners)
        {
            if (!string.IsNullOrEmpty(spawner.spawnerID))
            {
                spawnerMap[spawner.spawnerID] = spawner;
            }
        }
    }

    public void StartWave(WaveConfig wave)
    {
        currentWaveTotalEnemies = 0;
        currentWaveDefeatedEnemies = 0;

        foreach (var settings in wave.spawnerSettings)
        {
            if (spawnerMap.TryGetValue(settings.spawnerID, out EnemySpawner spawner))
            {
                spawner.ConfigureSpawner(settings.enemyPrefab, settings.maxEnemies, settings.spawnInterval, settings.spawnPerInterval);
                currentWaveTotalEnemies += settings.maxEnemies;
            }
            else
            {
                Debug.LogError($"Spawner with ID '{settings.spawnerID}' not found in the scene.");
            }
        }

        Debug.Log($"Wave starting with {currentWaveTotalEnemies} enemies.");
    }

    public void EnemyDestroyed()
    {
        currentWaveDefeatedEnemies++;
        Debug.Log($"Enemy defeated. {currentWaveDefeatedEnemies}/{currentWaveTotalEnemies} defeated.");
    }

    public bool IsWaveCompleted()
    {
        return currentWaveDefeatedEnemies >= currentWaveTotalEnemies;
    }
}
