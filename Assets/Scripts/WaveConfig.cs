using UnityEngine;

[CreateAssetMenu(fileName = "WaveConfig", menuName = "Wave/WaveConfig", order = 1)]
public class WaveConfig : ScriptableObject
{
    [System.Serializable]
    public class SpawnerSettings
    {
        public string spawnerID;             // Unique identifier for each spawner
        public GameObject enemyPrefab;      // Prefab to spawn from this spawner
        public int maxEnemies;              // Max enemies for this spawner
        public float spawnInterval;         // Time between spawns
        public int spawnPerInterval;        // Number of enemies spawned per interval
    }

    public SpawnerSettings[] spawnerSettings; // Array of spawner configurations
}
