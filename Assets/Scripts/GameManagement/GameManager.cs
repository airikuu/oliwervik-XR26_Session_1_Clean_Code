// GameManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameOver = false;
    public float gameTime = 0f;

    [SerializeField] private int winScore = 30;
    [SerializeField] private float restartDelay = 2f;

    [SerializeField] private PlayerScore playerScore;
    [SerializeField] private GameTimer timer;
    [SerializeField] private HUDController hud;

    private string _sceneName;

    void Start()
    {
        if (playerScore == null) playerScore = FindFirstObjectByType<PlayerScore>();
        if (timer == null) timer = FindFirstObjectByType<GameTimer>();
        if (hud == null) hud = FindFirstObjectByType<HUDController>();

        _sceneName = SceneManager.GetActiveScene().name;

        hud?.ShowStatus("Game Started!");
        hud?.ShowGameOverPanel(false);

        if (timer != null)
        {
            gameTime = 0f;
            hud?.SetTimer(gameTime);
            timer.StartTimer();
        }
    }

    void Update()
    {
        if (gameOver) return;

        if (timer != null)
        {
            gameTime = timer.Elapsed;
            hud?.SetTimer(gameTime);
        }

        if (playerScore != null && playerScore.GetScore() >= winScore)
        {
            WinGame();
        }
    }

    public void GameOver()
    {
        if (gameOver) return;

        gameOver = true;
        timer?.Stop();

        hud?.ShowStatus("GAME OVER!");
        hud?.ShowGameOverPanel(true);

        Invoke(nameof(RestartGame), restartDelay);
    }

    public void WinGame()
    {
        if (gameOver) return;

        gameOver = true;
        int finalScore = playerScore != null ? playerScore.GetScore() : 0;
        timer?.Stop();

        hud?.ShowStatus("YOU WIN! Score: " + finalScore);
        hud?.ShowGameOverPanel(true);

        Invoke(nameof(RestartGame), restartDelay);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(_sceneName);
    }
}

