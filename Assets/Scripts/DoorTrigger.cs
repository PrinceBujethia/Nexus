using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public string doorDirection; // Set this in Inspector: "North", "South", etc.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindFirstObjectByType<Room1Logic>()?.DoorEntered(doorDirection);
        }
    }
}
