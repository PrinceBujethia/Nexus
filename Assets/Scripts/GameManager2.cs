using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    public float timerDuration = 120f; // 2 minutes
    private float timer;
    public static GameManager2 Instance;

    private Room2Logic room2Logic;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        timer = timerDuration;

        // Use the new API to find Room2Logic
        room2Logic = Object.FindFirstObjectByType<Room2Logic>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            EvaluateRoom();
        }
    }

    public void ResetTimer()
    {
        timer = timerDuration;
    }

    public void EvaluateRoom()
    {
        if (room2Logic != null)
        {
            // Assuming EvaluatePuzzle is a function in Room2Logic that compares the sequence
            room2Logic.EvaluatePuzzle();
        }
    }

    public void OnPlayerDeath()
    {
        // Fade, show message, reset room
        Debug.Log("You failed. Try again.");
        // Reload current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    public void GoToNextRoom(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
