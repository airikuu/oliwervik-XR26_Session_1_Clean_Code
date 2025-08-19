// RestartInput.cs
using UnityEngine;

public class RestartInput : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    void Awake()
    {
        if (gameManager == null) gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // R to Restart
        {
            gameManager?.RestartGame();
        }
    }
}
