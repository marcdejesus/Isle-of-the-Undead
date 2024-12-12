using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    public WaveConfig[] waves;
    public Text waveText; // Reference to the Text UI element
    public BoxCollider unlockZoneCollider; // Reference to the MeshCollider

    private int currentWaveIndex = 0;
    private bool isWaveActive = false;

    private void Start()
    {
        UpdateWaveText();
    }

    private void Update()
    {
        if (!isWaveActive && currentWaveIndex < waves.Length)
        {
            StartNextWave();
        }

        if (isWaveActive && spawnManager.IsWaveCompleted())
        {
            isWaveActive = false;
            unlockZoneCollider.isTrigger = true;
            Debug.Log($"Wave {currentWaveIndex} completed!");
        }
    }

    public void StartNextWave()
    {
        if (currentWaveIndex < waves.Length)
        {
            Debug.Log($"Starting Wave {currentWaveIndex + 1}");
            spawnManager.StartWave(waves[currentWaveIndex]);
            isWaveActive = true;
            currentWaveIndex++;
            UpdateWaveText();
        }
        else
        {
            Debug.Log("All waves completed!");
        }
    }

    private void UpdateWaveText()
    {
        if (waveText != null)
        {
            waveText.text = $"{currentWaveIndex}";
        }
    }
}
