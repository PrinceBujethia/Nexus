using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public string direction; // "North", "South", etc.
    public Transform spawnPoint; // Where player teleports back to

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Room1Logic roomLogic = Object.FindFirstObjectByType<Room1Logic>();
            if (roomLogic != null)
            {
                roomLogic.DoorEntered(direction);
                other.transform.position = spawnPoint.position;
            }
        }
    }
}
