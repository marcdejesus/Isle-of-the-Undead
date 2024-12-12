using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public HealthBar healthBar; // Reference to the HealthBar script
    public float regenRate = 2f;
    public string gameOverSceneName; // Name of the Game Over scene

    public delegate void PlayerDeathHandler();
    public event PlayerDeathHandler OnPlayerDeath;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth); // Initialize the health bar
        StartCoroutine(RegenerateHealth());
    }

    void Update()
    {
        // For testing purposes, reduce health when the space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10f);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth); // Update the health bar
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        // Trigger the player death event
        if (OnPlayerDeath != null)
        {
            OnPlayerDeath();
        }

        // Load the Game Over scene
        SceneManager.LoadScene(gameOverSceneName);
    }

    private IEnumerator RegenerateHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second

            if (currentHealth < maxHealth)
            {
                currentHealth += regenRate;
                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
                healthBar.SetHealth(currentHealth); // Update the health bar
            }
        }
    }
}
