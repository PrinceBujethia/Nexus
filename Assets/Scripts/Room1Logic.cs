using System.Collections.Generic;
using UnityEngine;

public class Room1Logic : MonoBehaviour
{
    public List<string> correctSequence = new() { "North", "East", "South", "East" };
    private List<string> playerSequence = new();
    private bool sequenceLocked = false;

    public GameObject[] doorsToDisableColliders; // drag door gameObjects here
    public Transform playerSpawnPoint;           // drag spawn point here
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void DoorEntered(string direction)
    {
        if (sequenceLocked) return;

        playerSequence.Add(direction);

        // Teleport back to center
        player.transform.position = playerSpawnPoint.position;

        if (playerSequence.Count >= 4)
        {
            sequenceLocked = true;
            DisableAllDoors();
        }
    }

    private void DisableAllDoors()
    {
        foreach (var door in doorsToDisableColliders)
        {
            Collider2D col = door.GetComponent<Collider2D>();
            if (col != null)
            {
                col.isTrigger = false; // now door becomes solid
            }
        }
    }


    public void EvaluateSequence()
    {
        if (IsCorrectSequence())
        {
            Debug.Log("Correct Sequence! Moving to next room...");
            GameManager.Instance.GoToNextRoom("Room2"); // Set your next scene name here
        }
        else
        {
            Debug.Log("Wrong sequence. Restarting...");
            GameManager.Instance.OnPlayerDeath();
        }
    }

    private bool IsCorrectSequence()
    {
        if (playerSequence.Count != correctSequence.Count)
            return false;

        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (playerSequence[i] != correctSequence[i])
                return false;
        }

        return true;
    }
}
