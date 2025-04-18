using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float timerDuration = 120f;
    private float timer;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        timer = timerDuration;
    }

    private void Update()
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
        // Using new API
        FindFirstObjectByType<Room1Logic>()?.EvaluateSequence();
    }

    public void OnPlayerDeath()
    {
        Debug.Log("You failed. Try again.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToNextRoom(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
