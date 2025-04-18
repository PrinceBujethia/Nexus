using System.Collections.Generic;
using UnityEngine;

public class Room1Logic : MonoBehaviour
{
    public Transform playerSpawnPoint;

    private List<string> correctSequence = new List<string> { "N", "E", "S", "E" };
    private List<string> playerSequence = new List<string>();

    public void DoorEntered(string direction)
    {
        playerSequence.Add(direction);
        if (!IsSequenceCorrectSoFar())
        {
            Debug.Log("Wrong path! Restarting...");
            playerSequence.Clear();
            TeleportToStart();
        }
    }

    private bool IsSequenceCorrectSoFar()
    {
        if (playerSequence.Count > correctSequence.Count)
            return false;

        for (int i = 0; i < playerSequence.Count; i++)
        {
            if (playerSequence[i] != correctSequence[i])
                return false;
        }

        return true;
    }

    public void EvaluatePuzzle()
    {
        if (playerSequence.Count != correctSequence.Count)
        {
            GameManager.Instance.OnPlayerDeath();
            return;
        }

        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (playerSequence[i] != correctSequence[i])
            {
                GameManager.Instance.OnPlayerDeath();
                return;
            }
        }

        GameManager.Instance.GoToNextRoom("Room2"); // Set next room name
    }

    private void TeleportToStart()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = playerSpawnPoint.position;
    }
}
