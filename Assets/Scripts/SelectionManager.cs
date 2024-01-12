using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    public LayerMask selectionMask;
    public HexGrid hexGrid;
    public GameObject player; 
    public PlayerOrderManager playerOrderManager;

    private Hex playerHex;

    void Update()
    {
        player = playerOrderManager.player;
    } 

    private void Awake()
    {
         if (mainCamera == null)
            mainCamera = Camera.main;

        // Obtain the current player from the PlayerOrderManager
        int currentPlayerID = FindObjectOfType<PlayerOrderManager>().GetCurrentPlayerTurn();

        // Set the player reference based on the current player ID
        player = FindObjectOfType<PlayerOrderManager>().playerDictionary[currentPlayerID];

        // Assuming the player starts on a hexagon. You may need to set this reference based on your game logic.
        playerHex = player.GetComponent<Hex>();

        // ... (rest of the Awake method)
    }

    public void HandleClick(Vector3 mousePosition)
    {
        GameObject result;
        if (FindTarget(mousePosition, out result))
        {
            Hex clickedHex = result.GetComponent<Hex>();

            // Get the current player ID
            int currentPlayerID = playerOrderManager.GetCurrentPlayerTurn();

            // Ensure that the clicked hex is a neighbor and it's the turn of the player
            if (IsNeighbor(playerHex.HexCoords, clickedHex.HexCoords) && currentPlayerID == player.GetComponent<PlayerController>().PlayerID)
            {
                // Allow the player to move to the clicked hexagon.
                MovePlayerToHex(clickedHex);

                // Move to the next player's turn (if needed)
                // Note: Ensure that you have implemented AdvanceToNextPlayerTurn in PlayerOrderManager.
            }
            else
            {
                Debug.Log("Invalid move. Either not a neighbor or not the player's turn.");
            }
        }
    }

    private bool FindTarget(Vector3 mousePosition, out GameObject result)
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out hit, 100, selectionMask))
        {
            result = hit.collider.gameObject;
            return true;
        }
        result = null;
        return false;
    }

    private bool IsNeighbor(Vector3Int playerCoords, Vector3Int clickedCoords)
    {
        // Check if clicked hex is a neighbor of the player's hex.
        List<Vector3Int> neighbors = hexGrid.GetNeighboursFor(playerCoords);
        return neighbors.Contains(clickedCoords);
    }

    private void MovePlayerToHex(Hex destinationHex)
    {
        // Implement your logic to move the player to the destination hex.
        // This could involve updating the player's position or triggering a movement animation.
        // You might want to disable further clicks until the movement is complete.
        Debug.Log("Player moved to " + destinationHex.HexCoords);
    }
}