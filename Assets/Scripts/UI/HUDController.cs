using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameStatusText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Slider healthBar;

    [SerializeField] private PlayerScore playerScore;
    [SerializeField] private PlayerHealth playerHealth;

    private void Awake()
    {
        if (healthBar != null)
        {
            healthBar.maxValue = 30f;   // Set to match max health value
        }
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (playerScore == null) playerScore = FindFirstObjectByType<PlayerScore>();
        if (playerHealth == null) playerHealth = FindFirstObjectByType<PlayerHealth>();

        if (playerScore != null) playerScore.OnScoreChanged += HandleScoreChanged;
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged += HandleHealthChanged;
            playerHealth.OnDied += HandlePlayerDied;
        }
    }

    private void OnDisable()
    {
        if (playerScore != null) playerScore.OnScoreChanged -= HandleScoreChanged;
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged -= HandleHealthChanged;
            playerHealth.OnDied -= HandlePlayerDied;
        }
    }

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

    private void HandleScoreChanged(int newScore)
    {
        if (scoreText != null)
            scoreText.text = "Score: " + newScore;
    }

    private void HandleHealthChanged(float value)
    {
        if (healthBar != null)
            healthBar.value = value;
    }

    private void HandlePlayerDied()
    {
        ShowStatus("GAME OVER!");
        ShowGameOverPanel(true);
    }
}
