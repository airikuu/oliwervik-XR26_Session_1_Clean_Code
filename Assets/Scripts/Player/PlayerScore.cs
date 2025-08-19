using UnityEngine;
using TMPro;
using System;

public class PlayerScore : MonoBehaviour
{
    private int score = 0;

    // Direct UI references - bad practice for player script
    [SerializeField]
    private TextMeshProUGUI scoreText;

    public event Action<int> OnScoreChanged;

    private void Start()
    {
        UpdateScoreUI();
        OnScoreChanged?.Invoke(score);
    }

    // Collecting collectibles (Monolithic, handles score and interaction)
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
        OnScoreChanged?.Invoke(score);
        Debug.Log("Collected! Score: " + score);
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public int GetScore()
    {
        return score;
    }
}
