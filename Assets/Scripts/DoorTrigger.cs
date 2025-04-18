using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public string direction; // "N", "E", "S", "W"

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entered door: " + direction);
            FindFirstObjectByType<Room1Logic>().DoorEntered(direction);
        }
    }
}
