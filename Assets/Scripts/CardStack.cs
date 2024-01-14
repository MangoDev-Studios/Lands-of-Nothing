using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStack : MonoBehaviour
{
    public GameObject cardPrefab;
    public Collider2D stackCollider; // Use Collider2D for 2D colliders

    void Start()
    {
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

    void DrawCard()
    {
        // Instantiate a new card
        GameObject newCard = Instantiate(cardPrefab);

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
