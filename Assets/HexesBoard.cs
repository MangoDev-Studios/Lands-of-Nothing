using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.AnimatedValues;
using UnityEngine;

public class HexesBoard : MonoBehaviour
{
    public GameObject Hex0;
    public GameObject Hex1;
    public GameObject Hex2;
    public GameObject Hex3;
    public GameObject Hex4;
    public GameObject Hex5;
    public GameObject Hex6;
    public GameObject Hex7;
    public GameObject Hex8;
    public GameObject Jogador;
    public PlayerOrderManager playerOrderManager;
    public PlayerController playerController;
    private Dictionary<int, List<int>> validMoves;
    
     public Dice diceScript;
    // Start is called before the first frame update
    void Start()
    {
    InitializeValidMoves();
    MovePlayerToHex(0); // Starting position at hex 0
    }

    // Update is called once per frame
     void Update()
    {
        
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            
            // Check if it's the current player's turn
            int currentPlayerId = playerOrderManager.GetCurrentPlayerTurn();
            PlayerController jogadorController = Jogador.GetComponent<PlayerController>();


            Debug.Log("Current Player ID: " + jogadorController.PlayerID);
            Debug.Log("Current Player ID: " + currentPlayerId);
            
            if (currentPlayerId == jogadorController.PlayerID)
            {
                // Cast a ray from the mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                // Check if the ray hit a collider
                if (hit.collider != null)
                {
                    // Check which hex was clicked
                    GameObject clickedHex = hit.collider.gameObject;
                    int hexIndex = GetHexIndex(clickedHex);

                    // Check if the move is valid
                    if (IsValidMove(Jogador.transform.position, hexIndex))
                    {
                        // Move the player to the clicked hex
                        MovePlayerToHex(hexIndex);

                        // Notify PlayerOrderManager that the move is completed
                        playerOrderManager.PlayerMoveCompleted();
                    }
                }
            }
        }
    }

    // Move player to the center of the specified hex
void MovePlayerToHex(int hexIndex)
{
    // Get the corresponding hex GameObject based on the index
    GameObject targetHex = GetHexGameObject(hexIndex);

    // Maintain the existing Z coordinate of the player
    Vector3 newPosition = new Vector3(targetHex.transform.position.x, targetHex.transform.position.y, Jogador.transform.position.z);

    // Move the player to the new position
    Jogador.transform.position = newPosition;
}

    // Get the corresponding hex GameObject based on the index
    GameObject GetHexGameObject(int hexIndex)
    {
        switch (hexIndex)
        {
            case 0: return Hex0;
            case 1: return Hex1;
            case 2: return Hex2;
            case 3: return Hex3;
            case 4: return Hex4;
            case 5: return Hex5;
            case 6: return Hex6;
            case 7: return Hex7;
            case 8: return Hex8;
            default: return null; // Handle invalid index
        }
    }

    // Get the index of the hex GameObject
    int GetHexIndex(GameObject hex)
    {
        if (hex == Hex0) return 0;
        if (hex == Hex1) return 1;
        if (hex == Hex2) return 2;
        if (hex == Hex3) return 3;
        if (hex == Hex4) return 4;
        if (hex == Hex5) return 5;
        if (hex == Hex6) return 6;
        if (hex == Hex7) return 7;
        if (hex == Hex8) return 8;

        return -1; // Handle invalid hex
    }

    // Initialize the valid moves for each hex
    void InitializeValidMoves()
    {
        validMoves = new Dictionary<int, List<int>>();
        validMoves.Add(0, new List<int> { 1, 2 });
        validMoves.Add(1, new List<int> { 0, 1, 3, 4, 2 });
        validMoves.Add(2, new List<int> { 0, 1, 4, 5 });
        validMoves.Add(3, new List<int> { 1, 4, 6 });
        validMoves.Add(4, new List<int> { 1, 2, 3, 5, 6, 7 });
        validMoves.Add(5, new List<int> { 2, 4, 7 });
        validMoves.Add(6, new List<int> { 3, 4, 7, 8 });
        validMoves.Add(7, new List<int> { 5, 4, 6, 8 });
        validMoves.Add(8, new List<int> { 6, 7 });
    }

    // Check if the move is valid
    bool IsValidMove(Vector3 currentPosition, int destinationHex)
    {
        int currentHex = GetHexIndex(GetClosestHex(currentPosition));
        return validMoves[currentHex].Contains(destinationHex);
    }

    // Get the closest hex to a position
    GameObject GetClosestHex(Vector3 position)
    {
        GameObject[] hexes = { Hex0, Hex1, Hex2, Hex3, Hex4, Hex5, Hex6, Hex7, Hex8 };
        GameObject closestHex = hexes[0];
        float closestDistance = Vector3.Distance(position, hexes[0].transform.position);

        foreach (GameObject hex in hexes)
        {
            float distance = Vector3.Distance(position, hex.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestHex = hex;
            }
        }

        return closestHex;
    }
}



