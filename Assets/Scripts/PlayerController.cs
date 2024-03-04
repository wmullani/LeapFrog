using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public CharacterController characterController1;
    public CharacterController characterController2;

    public GameObject player1;
    public GameObject player2;
    public BoxCollider scoreTrigger1;
    public BoxCollider scoreTrigger2;
    private int score1 = 0;
    private int score2 = 0;
    private Vector3 moveVelocity1;
    private Vector3 turnVelocity1;
    private bool scoredOverPlayer1 = false;

    private Vector3 moveVelocity2;
    private Vector3 turnVelocity2;
    private bool scoredOverPlayer2 = false;

    public float speed = 3;
    public float rotationSpeed = 90;
    public float gravity = -20f;
    public float jumpSpeed = 15;
    public Text player1ScoreText;
    public Text player2ScoreText;

    public Text winText;

    public float pushForce = 10.0f;

    void Awake()
    {
        characterController1 = player1.GetComponent<CharacterController>();
        characterController2 = player2.GetComponent<CharacterController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player1 && moveVelocity1.y > 0 && !scoredOverPlayer1)
        {
            if (player1.transform.position.y > player2.transform.position.y) // Player 1 is higher than player 2
            {
                score1++;
                scoredOverPlayer1 = true;
                Debug.Log("Player 1 Score: " + score1);
                player1ScoreText.text = "Player 1 Score: " + score1;
            }
        }
        else if (other.gameObject == player2 && moveVelocity2.y > 0 && !scoredOverPlayer2)
        {
            if (player2.transform.position.y > player1.transform.position.y) // Player 2 is higher than player 1
            {
                score2++;
                scoredOverPlayer2 = true;
                Debug.Log("Player 2 Score: " + score2);
                player2ScoreText.text = "Player 2 Score: " + score2;
            }
        }
    }

    public void IncrementScore(int value)
    {
        // Determine which player's score to increment based on the player's GameObject
        if (gameObject == player1)
        {
            score1 += value;
            Debug.Log("Player 1 Score: " + score1);
        }
        else if (gameObject == player2)
        {
            score2 += value;
            Debug.Log("Player 2 Score: " + score2);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
{
    Rigidbody body = hit.collider.attachedRigidbody;
    if (body != null && !body.isKinematic)
    {
        body.velocity = hit.moveDirection * pushForce;
    }
} 


    void Update()
    {
        if (score1 >= 5)
        {
            winText.text = "Player 1 Wins!";
            winText.gameObject.SetActive(true);
        }
        else if (score2 >= 5)
        {
            winText.text = "Player 2 Wins!";
            winText.gameObject.SetActive(true);
        }

        float hInput1 = 0;
        float vInput1 = 0;

        float hInput2 = 0;
        float vInput2 = 0;

        if (Input.GetKey(KeyCode.A)) // Left
        {
            hInput1 = -1;
        }
        else if (Input.GetKey(KeyCode.D)) // Right
        {
            hInput1 = 1;
        }

        if (Input.GetKey(KeyCode.W)) // Forward
        {
            vInput1 = 1;
        }
        else if (Input.GetKey(KeyCode.S)) // Backward
        {
            vInput1 = -1;
        }

        if (Input.GetKey(KeyCode.J)) // Left
        {
            hInput2 = -1;
        }
        else if (Input.GetKey(KeyCode.L)) // Right
        {
            hInput2 = 1;
        }

        if (Input.GetKey(KeyCode.I)) // Forward
        {
            vInput2 = 1;
        }
        else if (Input.GetKey(KeyCode.K)) // Backward
        {
            vInput2 = -1;
        }

        // Player 1
        if (characterController1.isGrounded)
        {
            moveVelocity1 = transform.forward * speed * vInput1;
            turnVelocity1 = transform.up * rotationSpeed * hInput1;
            if (Input.GetKeyDown(KeyCode.F))
            {
                moveVelocity1.y = jumpSpeed;
                scoredOverPlayer1 = false;
            }
        }
        moveVelocity1.y += gravity * Time.deltaTime;
        characterController1.Move(moveVelocity1 * Time.deltaTime);
        transform.Rotate(turnVelocity1 * Time.deltaTime);

        // Player 2
        if (characterController2.isGrounded)
        {
            moveVelocity2 = transform.forward * speed * vInput2;
            turnVelocity2 = transform.up * rotationSpeed * hInput2;
            if (Input.GetKeyDown(KeyCode.H))
            {
                moveVelocity2.y = jumpSpeed;
                scoredOverPlayer2 = false;
            }
        }
        moveVelocity2.y += gravity * Time.deltaTime;
        characterController2.Move(moveVelocity2 * Time.deltaTime);
        transform.Rotate(turnVelocity2 * Time.deltaTime);
    }
}