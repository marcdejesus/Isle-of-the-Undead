using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public string spawnerID;

    private GameObject enemyPrefab;
    private int maxEnemies;
    private float spawnInterval;
    private int spawnPerInterval;

    private List<GameObject> activeEnemies = new List<GameObject>();
    private float spawnTimer = 0f;
    private int totalEnemiesSpawned = 0;
    private bool firstSpawnDone = false; // Tracks if the first spawn has occurred

    public void ConfigureSpawner(GameObject prefab, int maxEnemies, float interval, int perInterval)
    {
        this.enemyPrefab = prefab;
        this.maxEnemies = maxEnemies;
        this.spawnInterval = interval;
        this.spawnPerInterval = perInterval;

        activeEnemies.Clear();
        spawnTimer = 0f;
        totalEnemiesSpawned = 0;
        firstSpawnDone = false; // Reset first spawn flag
    }

    private void Update()
    {
        if (enemyPrefab == null || maxEnemies <= 0 || spawnInterval <= 0 || spawnPerInterval <= 0)
        {
            return;
        }

        activeEnemies.RemoveAll(enemy => enemy == null);

        if (totalEnemiesSpawned < maxEnemies)
        {
            if (!firstSpawnDone)
            {
                SpawnEnemies(); // Spawn immediately for the first interval
                firstSpawnDone = true;
                return;
            }

            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                SpawnEnemies();
                spawnTimer = 0f;
            }
        }
    }

    private void SpawnEnemies()
    {
        int enemiesToSpawn = Mathf.Min(spawnPerInterval, maxEnemies - totalEnemiesSpawned);
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector3 spawnPosition = GetRandomPointInZone();
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            Enemy enemy = newEnemy.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.spawner = this;
            }

            activeEnemies.Add(newEnemy);
            totalEnemiesSpawned++;
        }
    }

    private Vector3 GetRandomPointInZone()
    {
        Collider zone = GetComponent<Collider>();
        return new Vector3(
            Random.Range(zone.bounds.min.x, zone.bounds.max.x),
            transform.position.y,
            Random.Range(zone.bounds.min.z, zone.bounds.max.z)
        );
    }

    public void EnemyDestroyed(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
        SpawnManager.Instance.EnemyDestroyed();
    }
}
