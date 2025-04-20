using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveDirection;
    private Vector2 lastDirection;

    // Room2Logic reference for tracking direction in Room 2 only
    private Room2Logic room2Logic;

    // Movement speed (if you want adjustable speed)
    public float speed = 5f;

    void Start()
    {
        // Initialize the Room2Logic reference only if in Room 2
        room2Logic = Object.FindFirstObjectByType<Room2Logic>();
    }

    void Update()
    {
        // Capture player movement input
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // Handle movement input
        if (moveDirection != Vector2.zero)
        {
            // Record movement direction in Room 2
            if (room2Logic != null && moveDirection != lastDirection)
            {
                // Add to Room2's direction sequence
                string dirStr = GetDirection(moveDirection);
                room2Logic.playerDirectionSequence.Add(dirStr);
                lastDirection = moveDirection;
            }

            // Move the player (common for both rooms)
            transform.Translate(moveDirection * speed * Time.deltaTime);
        }
    }

    // Converts Vector2 direction to a string for comparison in Room 2
    string GetDirection(Vector2 dir)
    {
        if (dir.x > 0) return "Right";
        if (dir.x < 0) return "Left";
        if (dir.y > 0) return "Up";
        if (dir.y < 0) return "Down";
        return "None";
    }
}
