using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health = 30f;

    // Tightly coupled dependency to GameManager
    [SerializeField]
    private GameManager gameManager;

    // Direct UI references - bad practice for player script
    [SerializeField]
    private Slider healthBar;

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindFirstObjectByType<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("Player cannot find GameManager!");
            }
        }

        UpdateHealthUI();

        // Initialize health bar max value
        if (healthBar != null)
        {
            healthBar.maxValue = 30f; // Set to match max health value
            healthBar.value = health; // Ensure current value matches
        }
    }

    // Health management (Monolithic, includes UI logic)
    public void TakeDamage(float amount)
    {
        health -= amount;
        health = Mathf.Max(health, 0); // Health won't go below zero
        UpdateHealthUI();

        // Game Over condition tightly coupled here
        if (health <= 0)
        {
            Debug.Log("Player defeated!");
            if (gameManager != null)
            {
                gameManager.GameOver(); // Direct call to GameManager
            }
        }
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.value = health;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Enemy collision (damages player)
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
            Destroy(collision.gameObject);
            Debug.Log("Player hit by enemy! Health: " + health);
        }
    }
}

