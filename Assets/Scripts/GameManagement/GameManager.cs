using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Game state variables
    public bool gameOver = false;
    public float gameTime = 0f;

    [SerializeField] private int winScore = 30;
    [SerializeField] private float restartDelay = 2f;

    // Tightly coupled dependency to Player (violating Separation of Concerns)
    [SerializeField] private PlayerScore playerScore;

    // UI elements directly managed by GameManager (tight coupling)
    [SerializeField] private HUDController hud;

    [SerializeField] private GameTimer timer;

    public event Action OnGameOver;
    public event Action<int> OnWin;

    private string _sceneName;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (playerScore == null) playerScore = FindFirstObjectByType<PlayerScore>();
        if (hud == null) hud = FindFirstObjectByType<HUDController>();
        if (timer == null) timer = FindFirstObjectByType<GameTimer>();

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
        if (!gameOver)
        {
            if (timer != null)
            {
                gameTime = timer.Elapsed;
                hud?.SetTimer(gameTime);
            }

            // Win condition (tightly coupled)
            if (playerScore != null && playerScore.GetScore() >= winScore) // Direct access to player score
            {
                WinGame();
            }
        }
    }

    public void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            if (timer != null) timer.Stop();

            hud?.ShowStatus("GAME OVER!");
            hud?.ShowGameOverPanel(true);

            OnGameOver?.Invoke();

            Invoke(nameof(RestartGame), restartDelay); // Restart after 2 seconds
        }
    }

    public void WinGame()
    {
        if (!gameOver) // Ensure win can only happen once
        {
            gameOver = true;
            int finalScore = playerScore != null ? playerScore.GetScore() : 0;
            if (timer != null) timer.Stop();

            hud?.ShowStatus("YOU WIN! Score: " + finalScore);
            hud?.ShowGameOverPanel(true);

            OnWin?.Invoke(finalScore);

            Invoke(nameof(RestartGame), restartDelay); // Restart after 2 seconds
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume game
        SceneManager.LoadScene(_sceneName); // Reload current scene
    }
}
