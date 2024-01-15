using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStack : MonoBehaviour
{
    public GameObject cardPrefab;
    public Collider2D stackCollider; // Use Collider2D for 2D colliders
    public PlayerOrderManager playerOrderManager;

    void Start()
    {
        playerOrderManager = FindObjectOfType<PlayerOrderManager>();
        // Ensure that a collider is assigned to the stackCollider variable
        if (stackCollider == null)
        {
            Debug.LogError("Please assign a Collider2D to the stackCollider variable!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Check for left mouse click
        {
            // Check if the mouse click is over the card stack collider
            if (IsMouseOverStack())
            {
                DrawCard();
            }
        }
    }

    bool IsMouseOverStack()
    {
        // Cast a ray from the mouse position
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        // Check if the ray hits the stack collider
        return hit.collider != null && hit.collider == stackCollider;
    }

    public GameObject[] cardPrefabs; // Array to hold your 10 card prefabs

    public void DrawCard()
    {
        if (cardPrefabs == null || cardPrefabs.Length == 0)
        {
            Debug.LogError("Card prefabs not set up!");
            return;
        }

        // Select a random index from the cardPrefabs array
        int randomIndex = Random.Range(0, cardPrefabs.Length);

        // Instantiate the selected card prefab
        GameObject newCard = Instantiate(cardPrefabs[randomIndex]);

        // Check if the drawn card is the 6th element
        if (randomIndex == 0) // Adjust the index based on zero-indexing
        {
            // Increase remainingMoves by 2
            if (playerOrderManager != null)
            {
                playerOrderManager.remainingMoves += 4;
            }
            else
            {
                Debug.LogError("PlayerOrderManager reference not set up!");
            }
        }
           // Check if the drawn card is the 6th element
        if (randomIndex == 1) // Adjust the index based on zero-indexing
        {
            // Increase remainingMoves by 2
            if (playerOrderManager != null)
            {
                playerOrderManager.remainingMoves += 4;
            }
            else
            {
                Debug.LogError("PlayerOrderManager reference not set up!");
            }
        }
           // Check if the drawn card is the 6th element
        if (randomIndex == 2) // Adjust the index based on zero-indexing
        {
            // Increase remainingMoves by 2
            if (playerOrderManager != null)
            {
                playerOrderManager.remainingMoves += 4;
            }
            else
            {
                Debug.LogError("PlayerOrderManager reference not set up!");
            }
        }
           // Check if the drawn card is the 6th element
        if (randomIndex == 3) // Adjust the index based on zero-indexing
        {
            // Increase remainingMoves by 2
            if (playerOrderManager != null)
            {
                playerOrderManager.remainingMoves += 16;
            }
            else
            {
                Debug.LogError("PlayerOrderManager reference not set up!");
            }
        }
           // Check if the drawn card is the 6th element
        if (randomIndex == 4) // Adjust the index based on zero-indexing
        {
            // Increase remainingMoves by 2
            if (playerOrderManager != null)
            {
                playerOrderManager.remainingMoves += 18;
            }
            else
            {
                Debug.LogError("PlayerOrderManager reference not set up!");
            }
        }
           // Check if the drawn card is the 6th element
        if (randomIndex == 5) // Adjust the index based on zero-indexing
        {
            // Increase remainingMoves by 2
            if (playerOrderManager != null)
            {
                playerOrderManager.remainingMoves += 4;
            }
            else
            {
                Debug.LogError("PlayerOrderManager reference not set up!");
            }
        }
           // Check if the drawn card is the 6th element
        if (randomIndex == 6) // Adjust the index based on zero-indexing
        {
            // Increase remainingMoves by 2
            if (playerOrderManager != null)
            {
                playerOrderManager.remainingMoves += 4;
            }
            else
            {
                Debug.LogError("PlayerOrderManager reference not set up!");
            }
        }
           // Check if the drawn card is the 6th element
        if (randomIndex == 7) // Adjust the index based on zero-indexing
        {
            // Increase remainingMoves by 2
            if (playerOrderManager != null)
            {
                playerOrderManager.remainingMoves += 4;
            }
            else
            {
                Debug.LogError("PlayerOrderManager reference not set up!");
            }
        }
           // Check if the drawn card is the 6th element
        if (randomIndex == 8) // Adjust the index based on zero-indexing
        {
            // Increase remainingMoves by 2
            if (playerOrderManager != null)
            {
                playerOrderManager.remainingMoves += 6;
            }
            else
            {
                Debug.LogError("PlayerOrderManager reference not set up!");
            }
        }
           // Check if the drawn card is the 6th element
        if (randomIndex == 9) // Adjust the index based on zero-indexing
        {
            // Increase remainingMoves by 2
            if (playerOrderManager != null)
            {
                playerOrderManager.remainingMoves += 8;
            }
            else
            {
                Debug.LogError("PlayerOrderManager reference not set up!");
            }
        }

        // Set the card position to the center of the main camera's view
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            Vector3 centerOfScreen = new Vector3(Screen.width / 2f, Screen.height / 2f, mainCamera.nearClipPlane + 1f);
            newCard.transform.position = mainCamera.ScreenToWorldPoint(centerOfScreen);
        }
        else
        {
            Debug.LogError("Main camera not found!");
        }

        // Add any additional logic for the drawn card here

        // Destroy the card after 3 seconds
        Destroy(newCard, 3f);
    }
}
