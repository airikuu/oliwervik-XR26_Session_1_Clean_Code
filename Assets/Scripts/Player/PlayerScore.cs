using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    private int score = 0;

    // Direct UI references - bad practice for player script
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private void Start()
    {
        UpdateScoreUI();
    }

    // Collecting collectibles (Monolithic, handles score and interaction)
    // -> now handled by Collectible.cs instead of this script
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
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
