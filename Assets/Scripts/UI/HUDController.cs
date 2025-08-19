// HUDController.cs
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameStatusText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject gameOverPanel;

    // Optional: if you want HUD to also show score/health
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Slider healthBar;

    public void ShowStatus(string message)
    {
        if (gameStatusText != null) gameStatusText.text = message;
    }

    public void SetTimer(float seconds)
    {
        if (timerText != null)
            timerText.text = "Time: " + Mathf.FloorToInt(seconds) + "s";
    }

    public void ShowGameOverPanel(bool show)
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(show);
    }

    public void SetScore(int score)
    {
        if (scoreText != null) scoreText.text = "Score: " + score;
    }

    public void ConfigureHealthBar(float max, float current)
    {
        if (healthBar != null)
        {
            healthBar.maxValue = max;   // Set to match max health value
            healthBar.value = current;  // Ensure current value matches
        }
    }

    public void SetHealth(float current)
    {
        if (healthBar != null) healthBar.value = current;
    }
}
