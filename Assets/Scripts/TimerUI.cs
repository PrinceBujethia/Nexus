using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public Text timerText;

    void Update()
    {
        float time = Mathf.Ceil(GameManager.Instance.timerDuration);
        timerText.text = "Time: " + time.ToString("0");
    }
}
