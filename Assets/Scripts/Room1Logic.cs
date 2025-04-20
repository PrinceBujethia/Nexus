using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class Room1Logic : MonoBehaviour
{
    public List<string> correctSequence = new List<string> { "North", "East", "South", "East" };
    public List<string> playerSequence = new List<string>();
    private int maxMoves = 4;

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
            GameManager.Instance.GoToNextRoom("Room2");
        }
        else
        {
            GameManager.Instance.OnPlayerDeath();
        }
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
 