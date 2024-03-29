using UnityEngine;
using TMPro;

public class Alterar : MonoBehaviour
{
    public Sprite newSprite; // Assign the new sprite in the Unity editor
    public GameObject hex;
    private Sprite originalSprite; // To store the original sprite
    public TMP_Text texto;
    public PlayerOrderManager playerOrderManager;
    private void Start()
    {
        playerOrderManager = FindObjectOfType<PlayerOrderManager>();
        // Get the original sprite at the start
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalSprite = spriteRenderer.sprite;
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found!");
        }
    }

    private void Update()
    {   

        // Check for mouse click
        if (Input.GetMouseButtonDown(1)) // Change to Input.GetButtonDown("Fire1") for cross-platform input
        {
            // Check if the mouse is over the object
            if (IsMouseOverObject() && playerOrderManager.remainingMoves >= 2)
            {
                ToggleSprite();

            if (GetComponent<SpriteRenderer>().sprite == originalSprite)
            {
                playerOrderManager.greensidesCount[playerOrderManager.currentPlayerId] -= 1;
            }
            else
            {
                playerOrderManager.greensidesCount[playerOrderManager.currentPlayerId] += 1;
            }

                playerOrderManager.remainingMoves -= 2;
            }
        }
    }

    
    public void ToggleSprite()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // If the current sprite is the original sprite, switch to the new sprite, and vice versa
            spriteRenderer.sprite = (spriteRenderer.sprite == originalSprite) ? newSprite : originalSprite;
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found!");
        }
    }

    private bool IsMouseOverObject()
    {
        // Create a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Perform a raycast for 2D physics
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        // Check if the hit object is the same as the object this script is attached to
        return hit.collider != null && hit.collider.gameObject == hex;
    }
}



