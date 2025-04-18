using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Timer Settings")]
    public float timerDuration = 120f;
    private float timer;

    private bool timerActive = true;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // Ensure only one GameManager exists

        DontDestroyOnLoad(gameObject); // Optional: persist across scenes
    }

    private void Start()
    {
        ResetTimer();
    }

    private void Update()
    {
        if (!timerActive) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timerActive = false;
            EvaluateCurrentRoom();
        }
    }

    public void ResetTimer()
    {
        timer = timerDuration;
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }

    public void EvaluateCurrentRoom()
    {
        // Room-specific evaluation logic based on current scene
        string currentScene = SceneManager.GetActiveScene().name;

        switch (currentScene)
        {
            case "Room1":
                FindFirstObjectByType<Room1Logic>()?.EvaluatePuzzle();
                break;
            //case "Room2":
            //    FindFirstObjectByType<Room2Logic>()?.EvaluatePuzzle();
            //    break;
            //case "Room3":
            //    FindFirstObjectByType<Room3Logic>()?.EvaluatePuzzle();
            //    break;
            //case "Room4":
            //    FindFirstObjectByType<Room4Logic>()?.EvaluatePuzzle();
            //    break;
            //case "Room5":
            //    FindFirstObjectByType<Room5Logic>()?.EvaluatePuzzle();
            //    break;
            default:
                Debug.LogWarning("No room logic found for: " + currentScene);
                break;
        }
    }

    public void OnPlayerDeath()
    {
        Debug.Log("You failed. Try again.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToNextRoom(string nextRoomName)
    {
        SceneManager.LoadScene(nextRoomName);
        ResetTimer();
    }
}
