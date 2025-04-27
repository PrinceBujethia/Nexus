using UnityEngine;
using System.Collections;

public class Room1GateTrigger : MonoBehaviour
{
    public GameObject uiHintCanvas;
    public Animator DoorMessageAnimator;

    public GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.TimerStart();
            if (uiHintCanvas != null)
            {
                uiHintCanvas.SetActive(true);
                StartCoroutine(DoorMessage());
            }
        }
    }

    private IEnumerator DoorMessage() 
    {
        yield return new WaitForSeconds(10f);
        DoorMessageAnimator.SetTrigger("hide");
    }
}
