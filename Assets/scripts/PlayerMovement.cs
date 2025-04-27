using System.Collections;
using System.Security.Cryptography;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    public Animator CharacterAnimator;
    public Animator DialogueAnimator1;
    public Animator DialogueAnimator2;
    public GameObject DialogueBox1;
    public GameObject DialogueBox2;

    void Start() 
    { 
        rb = GetComponent<Rigidbody2D>(); 
        DialogueBox1.SetActive(false); // Hide the dialogue box at the start
        DialogueBox2.SetActive(false); // Hide the second dialogue box at the start
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        rb.linearVelocity = new Vector2(moveX, moveY).normalized * speed;
    }

    public void OnCharacterfadeInComplete() 
    {
        DialogueBox1.SetActive(true); // Show the dialogue box
        StartCoroutine(StartDialogueSequence1());
    }

    private IEnumerator StartDialogueSequence1() 
    {
        yield return new WaitForSeconds(6f); // Wait for 2 seconds
        DialogueAnimator1.SetTrigger("hide");
        yield return new WaitForSeconds(1f); // Wait for 1 second
        StartCoroutine(StartDialogueSequence2());
    }

    private IEnumerator StartDialogueSequence2() 
    {
        DialogueBox2.SetActive(true); // show the dialogue box
        yield return new WaitForSeconds(6f); // Wait for 2 seconds
        DialogueAnimator2.SetTrigger("hide");
    }


}