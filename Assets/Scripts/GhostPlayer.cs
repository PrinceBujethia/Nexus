using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GhostPlayer : MonoBehaviour
{
    public float moveSpeed = 3f;
    public List<MoveDirection> ghostDirections;
    private int currentIndex = 0;

    void Start()
    {
        if (ghostDirections == null || ghostDirections.Count == 0)
        {
            Debug.LogWarning("No ghost directions set!");
            return;
        }

        Debug.Log("GhostPlayer will start moving in 5 seconds.");
        Invoke(nameof(BeginGhostMovement), 5f);
    }

    void BeginGhostMovement()
    {
        Debug.Log("GhostPlayer started moving.");
        StartCoroutine(MoveSequence());
    }

    IEnumerator MoveSequence()
    {
        while (currentIndex < ghostDirections.Count)
        {
            Vector3 dir = DirectionToVector(ghostDirections[currentIndex]);
            Debug.Log("Moving " + ghostDirections[currentIndex]);

            float elapsed = 0f;
            Vector3 startPos = transform.position;
            Vector3 target = startPos + dir;

            while (elapsed < 1f / moveSpeed)
            {
                elapsed += Time.deltaTime;
                transform.position = Vector3.Lerp(startPos, target, elapsed * moveSpeed);
                yield return null;
            }

            transform.position = target;
            currentIndex++;
        }

        Debug.Log("GhostPlayer finished path.");
    }

    Vector3 DirectionToVector(MoveDirection dir) => dir switch
    {
        MoveDirection.Up => Vector3.up,
        MoveDirection.Down => Vector3.down,
        MoveDirection.Left => Vector3.left,
        MoveDirection.Right => Vector3.right,
        _ => Vector3.zero
    };
}
