using System.Collections;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject highlightChild; // Optional visual highlight (e.g. glow effect)
    public GameObject inCan;          // Assign inacn_1 GameObject here (the one to activate)

    void Awake()
    {
        if (highlightChild != null)
            highlightChild.SetActive(false);

        if (inCan != null)
            inCan.SetActive(false); // Initially hidden
    }

    public IEnumerator PlayAndHighlight()
    {
        if (highlightChild != null)
            highlightChild.SetActive(true);

        if (inCan != null)
            inCan.SetActive(true); // Activate when music starts

        if (audioSource != null)
            audioSource.Play();

        float duration = (audioSource != null && audioSource.clip != null)
                         ? audioSource.clip.length
                         : 1f;

        yield return new WaitForSeconds(duration);

        if (highlightChild != null)
            highlightChild.SetActive(false);

        if (inCan != null)
            inCan.SetActive(false); // Deactivate after music
    }
}
