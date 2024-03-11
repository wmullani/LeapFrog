using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public InventoryManager inventoryManager;
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
        inventoryManager = FindObjectOfType<InventoryManager>();
        characterController1 = player1.GetComponent<CharacterController>();
        characterController2 = player2.GetComponent<CharacterController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Victory Pedestal"))
        {
            int itemCount = inventoryManager.GetItemCount(gameObject.name, "Collectible"); // Check the inventory count
            Debug.Log($"Player {gameObject.name} has {itemCount} collectibles");

            if (itemCount >= 5)
            {
                winText.text = gameObject.name + " Wins!";
                winText.gameObject.SetActive(true);
            }
        }

        /*if (other.gameObject == player1 && moveVelocity1.y > 0 && !scoredOverPlayer1)
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
        }*/
    }

    public void IncrementScore(int value)
    {
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
    HandlePlayerInput(player1, characterController1, ref moveVelocity1, ref turnVelocity1, KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S, KeyCode.F, ref scoredOverPlayer1);
    HandlePlayerInput(player2, characterController2, ref moveVelocity2, ref turnVelocity2, KeyCode.J, KeyCode.L, KeyCode.I, KeyCode.K, KeyCode.H, ref scoredOverPlayer2);
}

void HandlePlayerInput(GameObject player, CharacterController characterController, ref Vector3 moveVelocity, ref Vector3 turnVelocity, KeyCode leftKey, KeyCode rightKey, KeyCode forwardKey, KeyCode backwardKey, KeyCode jumpKey, ref bool scoredOverPlayer)
{
    float hInput = 0;
    float vInput = 0;

    if (player == player1)
    {
        if (Input.GetKey(leftKey)) // Left
        {
            hInput = -1;
        }
        else if (Input.GetKey(rightKey)) // Right
        {
            hInput = 1;
        }

        if (Input.GetKey(forwardKey)) // Forward
        {
            vInput = 1;
        }
        else if (Input.GetKey(backwardKey)) // Backward
        {
            vInput = -1;
        }

        if (Input.GetKeyDown(jumpKey) && characterController.isGrounded)
        {
            moveVelocity.y = jumpSpeed;
            scoredOverPlayer = false;
        }
    }
    else if (player == player2)
    {
        if (Input.GetKey(leftKey)) // Left
        {
            hInput = -1;
        }
        else if (Input.GetKey(rightKey)) // Right
        {
            hInput = 1;
        }

        if (Input.GetKey(forwardKey)) // Forward
        {
            vInput = 1;
        }
        else if (Input.GetKey(backwardKey)) // Backward
        {
            vInput = -1;
        }

        if (Input.GetKeyDown(jumpKey) && characterController.isGrounded)
        {
            
            moveVelocity.y = jumpSpeed;
            scoredOverPlayer = false;
        }
    }

    if (characterController.isGrounded)
    {
        moveVelocity = player.transform.forward * speed * vInput;
        turnVelocity = player.transform.up * rotationSpeed * hInput;
    }

    moveVelocity.y += gravity * Time.deltaTime;
    characterController.Move(moveVelocity * Time.deltaTime);
    player.transform.Rotate(turnVelocity * Time.deltaTime);
}
}