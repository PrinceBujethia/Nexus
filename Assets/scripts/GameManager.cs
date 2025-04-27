using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Timer Settings")]
    public float timerDuration = 120f; // 2 minutes
    private float timer;
    private bool timerRunning = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); // Optional if you want persistence
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //void Start()
    //{
    //    ResetTimer();
    //}

    public void TimerStart() 
    {
        ResetTimer();
    }

    void Update()
    {
        if (!timerRunning) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timerRunning = false;
            EvaluateCurrentRoom();
        }
    }

    public void ResetTimer()
    {
        Debug.Log("Timer reset");
        timer = timerDuration;
        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void EvaluateCurrentRoom()
    {
        string scene = SceneManager.GetActiveScene().name;

        switch (scene)
        {
            case "Room1":
                var r1 = FindFirstObjectByType<Room1Logic>();
                if (r1 != null) r1.EvaluatePuzzle();
                break;
            case "Room2":
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
                break;
            //case "Room3":
            //    var r3 = FindFirstObjectByType<Room3Logic>();
            //    if (r3 != null) r3.EvaluatePuzzle();
            //    break;
            //case "Room4":
            //    var r4 = FindFirstObjectByType<Room4Logic>();
            //    if (r4 != null) r4.EvaluatePuzzle();
            //    break;
            //case "Room5":
            //    var r5 = FindFirstObjectByType<Room5Logic>();
            //    if (r5 != null) r5.EvaluatePuzzle();
            //    break;
            default:
                Debug.LogWarning("Unknown scene: " + scene);
                break;
        }
    }

    public void GoToNextRoom(string nextSceneName)
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public void OnPlayerDeath()
    {
        Debug.Log("You failed. Try again.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
