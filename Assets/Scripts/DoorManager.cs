using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public static DoorManager Instance;
    public Collider2D[] doors;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void DisableAllDoors()
    {
        foreach (var door in doors)
        {
            door.enabled = false;
        }
    }

    public void EnableAllDoors()
    {
        foreach (var door in doors)
        {
            door.enabled = true;
        }
    }
}
