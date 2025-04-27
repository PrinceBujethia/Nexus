using UnityEngine;

public class EntryTrigger : MonoBehaviour
{
    [Tooltip("The sequence index for this trigger, set in the Inspector")]
    public int sequenceIndex;  // Index for this entry trigger in the sequence

    [Tooltip("Reference to the Room3Logic script")]
    public Room3Logic room3Logic;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only proceed if the player enters
        if (other.CompareTag("Player"))
        {
            // Add this sequence index to the player sequence
            room3Logic.PlayerPressed(sequenceIndex);

            // Optionally, play the sound and highlight the object if needed
            SoundObject soundObject = room3Logic.soundObjects[sequenceIndex];
            StartCoroutine(soundObject.PlayAndHighlight());

            Debug.Log("Player Sequence Updated: " + sequenceIndex);
        }
    }
}
