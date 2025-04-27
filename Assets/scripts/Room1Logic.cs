using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Room1Logic : MonoBehaviour
{
    public List<string> correctSequence = new List<string> { "North", "East", "South", "East" };
    public List<string> playerSequence = new List<string>();
    private int maxMoves = 4;
    public GameObject LevelComplete; // Assign in Inspector
    public void DoorEntered(string direction)
    {
        if (playerSequence.Count >= maxMoves) return;

        playerSequence.Add(direction);

        if (playerSequence.Count >= maxMoves)
        {
            DoorManager.Instance.DisableAllDoors(); // Optional visual/logic lock
        }
    }

    public void EvaluatePuzzle()
    {
        if (IsSequenceCorrect())
        {
            // Optional: Play success sound or visual feedback
            Debug.Log("Puzzle solved! Player sequence: " + string.Join(", ", playerSequence));
            StartCoroutine(NextRoom());
        }
        else
        {
            GameManager.Instance.OnPlayerDeath();
        }
    }

    private IEnumerator NextRoom()
    {
        //Start Animation

        LevelComplete.SetActive(true);
        yield return new WaitForSeconds(6f); // Optional delay before going to the next room

        //Move to the next Scene
        GameManager.Instance.GoToNextRoom("Room2");
    }

    private bool IsSequenceCorrect()
    {
        if (playerSequence.Count != correctSequence.Count) return false;

        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (playerSequence[i] != correctSequence[i])
                return false;
        }

        return true;
    }
}
 