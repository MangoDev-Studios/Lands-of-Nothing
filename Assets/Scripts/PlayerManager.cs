using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int currentPlayerIndex = 0;
    private int remainingMoves = 0;
    private bool isMoving = false;

    public HexGrid hexGrid; // Reference to your HexGrid
    public PlayerOrderManager playerOrderManager; // Reference to your PlayerOrderManager
    public float moveSpeed = 5f; // Player movement speed

    private void Start()
    {
        // Check if player order is determined
        if (playerOrderManager.IsPlayerOrderDetermined())
        {
            int[] playerOrder = playerOrderManager.GetPlayerOrder();
            currentPlayerIndex = playerOrder[0];
        }
        else
        {
            Debug.LogWarning("Player order not yet determined.");
        }
    }

    private void Update()
    {
        // Check if the player still has moves and is not moving
        if (remainingMoves > 0 && !isMoving)
        {
            // Simulate player movement
            StartCoroutine(MovePlayer());
        }
    }

    public void RollDice(int rollResult)
    {
        // Update the number of moves with the dice result
        remainingMoves = rollResult;

        // Update the current player index
        currentPlayerIndex = playerOrderManager.GetCurrentPlayerTurn() - 1;
    }

    private IEnumerator MovePlayer()
    {
        isMoving = true;

        while (remainingMoves > 0)
        {
            // Get the next position for the player
            Vector3Int nextPosition = hexGrid.GetNearestTilePosition(transform.position);

            // Move the player to the next position
            yield return StartCoroutine(MoveToPosition(nextPosition));

            // Wait until the player reaches the next position
            yield return new WaitForSeconds(1f / moveSpeed);

            // Update the remaining moves
            remainingMoves--;
        }

        isMoving = false;
    }

    private IEnumerator MoveToPosition(Vector3Int targetPosition)
    {
        // Logic for player movement to the next position
        // Implement as needed for your game

        // Example 2D movement:
        Vector3 targetPosition3D = hexGrid.GetTileCenter(targetPosition);
        while (Vector3.Distance(transform.position, targetPosition3D) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition3D, Time.deltaTime * moveSpeed);
            yield return null;
        }
    }

}
