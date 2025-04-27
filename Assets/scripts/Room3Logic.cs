using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Room3Logic : MonoBehaviour
{
    [Header("Sequence Settings")]
    public List<SoundObject> soundObjects;      // Assign in Inspector (5 pillars)
    public List<int> correctSequence = new();  // Set through Inspector (No random sequence)
    public float delayBetweenNotes = 1f;
    public float puzzleDuration = 120f;

    public List<int> playerSequence = new();
    private bool puzzleStarted = false;
    private bool isInputAllowed = false;
    private float timer = 0f;

    [Header("UI Elements")]
    public GameObject uiHintText;     // Drag HintText object here
    public GameObject Final_Message;
    public float hintDisplayTime = 4f;


    void Update()
    {
        if (!puzzleStarted) return;

        timer += Time.deltaTime;
        if (timer >= puzzleDuration)
        {
            isInputAllowed = false;
            EvaluateSequence();
        }
    }

    // Triggered by the collision from EntryTrigger (This starts the sequence)
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!puzzleStarted && other.CompareTag("Player"))
        {
            puzzleStarted = true;
            timer = 0f;

            if (uiHintText != null)
            {
                uiHintText.SetActive(true);
                StartCoroutine(HideHintAfterDelay());
            }

            StartCoroutine(PlaySequence());
        }
    }

    private IEnumerator HideHintAfterDelay()
    {
        yield return new WaitForSeconds(hintDisplayTime);
        if (uiHintText != null)
            uiHintText.SetActive(false);
    }


    private IEnumerator PlaySequence()
    {
        isInputAllowed = false;
        yield return new WaitForSeconds(1f); // Optional delay before starting sequence

        // Play each sound in the correct sequence and highlight
        foreach (int idx in correctSequence)
        {
            SoundObject so = soundObjects[idx];
            StartCoroutine(so.PlayAndHighlight());  // Play sound and highlight
            yield return new WaitForSeconds(so.audioSource.clip.length + delayBetweenNotes);
        }

        isInputAllowed = true;  // Allow player input after the sequence is played
    }

    // Player pressed the sequence index (add to the sequence)
    public void PlayerPressed(int sequenceIndex)
    {
        if (!puzzleStarted || !isInputAllowed) return;

        playerSequence.Add(sequenceIndex);  // Add to player sequence
        Debug.Log("Player Sequence: " + string.Join(", ", playerSequence)); // Log for debugging

        // Check if the player has entered the full sequence
        if (playerSequence.Count == correctSequence.Count)
        {
            EvaluateSequence();
        }
    }

    // Compare the player sequence with the correct sequence
    private void EvaluateSequence()
    {
        bool success = playerSequence.Count == correctSequence.Count;

        for (int i = 0; success && i < correctSequence.Count; i++)
        {
            if (playerSequence[i] != correctSequence[i])
            {
                success = false;
                break;
            }
        }

        //StartCoroutine(success ? GoNext() : Restart());
        if (success) 
        {
            Debug.Log("Good");
            StartCoroutine(GoNext());
        }
        else
        {
            Debug.Log("Bad");
            Restart();
        }
    }

    private IEnumerator GoNext()
    {
        Debug.Log("Good");
        // Go to next scene
        Final_Message.SetActive(true);
        yield return new WaitForSeconds(3f); // Optional delay before going to the next room
        Application.Quit();
    }

    private void Restart()
    {
        // Restart the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
