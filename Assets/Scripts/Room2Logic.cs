using System.Collections.Generic;
using UnityEngine;

public class Room2Logic : MonoBehaviour
{
    public List<string> correctDirectionSequence = new List<string>(); // Correct ghost movement
    public List<string> playerDirectionSequence = new List<string>(); // Player's recorded movement

    // Call this method to evaluate the player's actions after the timer ends
    public void EvaluatePuzzle()
    {
        if (IsSequenceCorrect())
        {
            Debug.Log("Correct mimic! Player advances to the next room.");
            GameManager.Instance.GoToNextRoom("Room3"); // Change "Room3" to the correct scene name
        }
        else
        {
            Debug.Log("Incorrect mimic! Player respawns.");
            GameManager.Instance.OnPlayerDeath(); // Respawn player in the same room
        }
    }

    // Checks if player's sequence matches the ghost's sequence
    private bool IsSequenceCorrect()
    {
        if (playerDirectionSequence.Count != correctDirectionSequence.Count) return false;

        for (int i = 0; i < playerDirectionSequence.Count; i++)
        {
            if (playerDirectionSequence[i] != correctDirectionSequence[i]) return false;
        }

        return true;
    }

    // Call this method to reset the player sequence for a new attempt
    public void ResetPlayerSequence()
    {
        playerDirectionSequence.Clear();
    }
}
