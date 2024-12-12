using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField]
    private int maxHealth = 100;
    private int currentHealth;

    [Header("Navigation")]
    public NavMeshAgent agent;
    public EnemySpawner spawner;

    private GameObject player;

    // Animation parameters
    private float speed;
    private float direction;
    private float angularSpeed;
    private bool attacking;
    private bool takingDamage;
    private bool dead;

    private float attackCooldown = 1f; // Time between attacks
    private float lastAttackTime; // Time of the last attack

    private void Start()
    {
        // Initialize health
        currentHealth = maxHealth;

        // Find the player
        player = GameObject.FindGameObjectWithTag("Player");

        // Get the NavMeshAgent component
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    private void Update()
    {
        if (player != null)
        {
            // Set the agent's destination to the player's position
            agent.destination = player.transform.position;

            // Update animation parameters
            speed = agent.velocity.magnitude;
            direction = Vector3.Dot(transform.forward, agent.desiredVelocity.normalized);
            angularSpeed = Vector3.SignedAngle(transform.forward, agent.desiredVelocity, Vector3.up);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AttackPlayer(collision.gameObject);
        }
    }

    private void AttackPlayer(GameObject player)
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            // Set attacking flag
            attacking = true;
            Invoke("ResetAttacking", 1.0f); // Reset after 1 second

            // Deal damage to the player
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10f);
            }

            // Update the last attack time
            lastAttackTime = Time.time;
        }
    }

    private void ResetAttacking()
    {
        attacking = false;
    }

    public void TakeDamage(int damage)
    {
        // Reduce current health by damage amount
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage!");

        // Set taking damage flag
        takingDamage = true;
        Invoke("ResetTakingDamage", 0.5f); // Reset after 0.5 seconds

        // Check if the enemy should die
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void ResetTakingDamage()
    {
        takingDamage = false;
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has died!");

        // Set dead flag
        dead = true;

        // Notify the spawner that the enemy has been destroyed
        if (spawner != null)
        {
            spawner.EnemyDestroyed(gameObject);
        }

        // Destroy the enemy GameObject
        Destroy(gameObject);
    }

    // Example methods to get the values
    public float GetSpeed() { return speed; }
    public float GetDirection() { return direction; }
    public float GetAngularSpeed() { return angularSpeed; }
    public bool IsAttacking() { return attacking; }
    public bool IsTakingDamage() { return takingDamage; }
    public bool IsDead() { return dead; }
}
