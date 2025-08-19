// GameTimer.cs
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float Elapsed { get; private set; }
    public bool Running { get; private set; }

    public void StartTimer()
    {
        Elapsed = 0f;
        Running = true;
    }

    public void Stop()
    {
        Running = false;
    }

    void Update()
    {
        if (!Running) return;
        Elapsed += Time.deltaTime;
    }
}

