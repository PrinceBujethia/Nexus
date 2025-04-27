using System.Collections;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public string direction; // "North", "South", etc.
    public Transform spawnPoint; // Where player teleports back to
    public GameObject FLoating_Text;
    public GameObject Incan_1;
    public GameObject Incan_2;
    public GameObject Incan_3;
    public GameObject Incan_4;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Room1Logic roomLogic = Object.FindFirstObjectByType<Room1Logic>();
            if (roomLogic != null)
            {
                roomLogic.DoorEntered(direction);
                StartCoroutine(Spawner(other));
            }
        }
        StartCoroutine(ShowDirections());
    }

    private IEnumerator ShowDirections() 
    {
        FLoating_Text.SetActive(true);
        yield return new WaitForSeconds(2f);
        FLoating_Text.SetActive(false);
    }

    private IEnumerator Spawner(Collider2D other) 
    {
        Incan_1.SetActive(true);
        Incan_2.SetActive(true);
        Incan_3.SetActive(true);
        Incan_4.SetActive(true);
        yield return new WaitForSeconds(1f);
        other.transform.position = spawnPoint.position;
    }
}
