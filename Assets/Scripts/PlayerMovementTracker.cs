using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public class PlayerMovementTracker : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    public List<MoveDirection> playerDirections = new();

    void Start() => rb = GetComponent<Rigidbody2D>();

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (moveInput != Vector2.zero)
        {
            rb.linearVelocity = moveInput.normalized * speed;
            TrackDirection(moveInput);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void TrackDirection(Vector2 dir)
    {
        MoveDirection moveDir = dir.x switch
        {
            > 0 => MoveDirection.Right,
            < 0 => MoveDirection.Left,
            _ => dir.y switch
            {
                > 0 => MoveDirection.Up,
                < 0 => MoveDirection.Down,
                _ => MoveDirection.Up
            }
        };

        // Only add if it's different from last to count direction changes
        if (playerDirections.Count == 0 || playerDirections[^1] != moveDir)
        {
            playerDirections.Add(moveDir);
            if (playerDirections.Count > 10) playerDirections.RemoveAt(0);
        }
    }
}
