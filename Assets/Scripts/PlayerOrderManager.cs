using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerOrderManager : MonoBehaviour
{
    private struct PlayerRoll
    {
        public int playerId;
        public int rollValue;
    }

    private PlayerRoll[] playerRolls = new PlayerRoll[4];
    private bool playerOrderDetermined = false;
    private bool firstRound = true; // New flag to indicate the first round
    private int[] playerOrder;
    private int currentTurnCount = 0;
    private int currentTurn;
    public int currentPlayerIndex = 0;
    public GameObject player; 


    public Dictionary<int, GameObject> playerDictionary = new Dictionary<int, GameObject>();

    void Update()
    {
        if (currentPlayerIndex == 0)
        {
            this.player = GameObject.FindWithTag("Character_1");
        }
        else if (currentPlayerIndex == 1)
        {
            this.player = GameObject.FindWithTag("Character_2");
        }
        else if (currentPlayerIndex == 2)
        {
            this.player = GameObject.FindWithTag("Character_3");
        }
        else if (currentPlayerIndex == 3)
        {
            this.player = GameObject.FindWithTag("Character_4");
        }
    }

    public void RecordDiceRoll(int playerId, int rollResult)
    {
        if (!playerOrderDetermined)
        {
            playerRolls[playerId].playerId = playerId;
            playerRolls[playerId].rollValue += rollResult;

            Debug.Log("Player " + playerId + " rolled: " + rollResult);

            if (Array.TrueForAll(playerRolls, p => p.rollValue > 0))
            {
                DetermineStartingOrder();
            }
        }
        else
        {
            playerRolls[playerId].rollValue += rollResult;

            HandlePlayerOrder(playerId, rollResult);
        }
    }

    private void DetermineStartingOrder()
    {
        if (!playerOrderDetermined)
        {
            firstRound = false;

            currentTurnCount = 1;

            Array.Sort(playerRolls, (p1, p2) => p2.rollValue.CompareTo(p1.rollValue));

            playerOrder = Array.ConvertAll(playerRolls, p => p.playerId);

            Debug.Log("Player order: " + string.Join(", ", playerOrder));

            playerOrderDetermined = true;

            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            for (int i = 0; i < players.Length; i++)
            {
                PlayerController playerController = players[i].GetComponent<PlayerController>();
                if (playerController != null)
                {
                    int playerId = i;
                    playerController.PlayerID = playerId;

                    if (!playerDictionary.ContainsKey(playerId))
                    {
                        playerDictionary.Add(playerId, players[i]);
                    }
                    else
                    {
                        Debug.LogError("Duplicate PlayerID found: " + playerId);
                    }
                }
                else
                {
                    Debug.LogError("PlayerController not found on GameObject with tag 'Player'.");
                }
            }
        }
        else
        {
            Debug.LogWarning("Player order already determined.");
        }
    }

    private void HandlePlayerOrder(int playerId, int rollResult)
    {
        // Handle the player order following the defined order
        currentTurnCount++;

        int turnsToMove = rollResult;
        Debug.Log("Player " + playerId + " has " + turnsToMove + " turns to move.");
    }


    public int GetCurrentPlayerTurn()
    {
        if (playerOrderDetermined && playerOrder.Length > 0)
        {
            Debug.Log("CurrentPlayer" + currentPlayerIndex);
            Debug.Log("Array " + playerOrder[currentPlayerIndex]);
            int currentPlayerId = playerOrder[currentPlayerIndex];

            // call on end turn
            currentPlayerIndex += 1;
        
            if (playerDictionary.TryGetValue(currentPlayerId, out GameObject currentPlayerObject))
            {
                
                return currentPlayerObject.GetComponent<PlayerController>().PlayerID;
            }
        }

        return 999;
    }

    public int[] GetPlayerOrder()
    {
        return playerOrder;
    }

    public bool IsPlayerOrderDetermined()
    {
        return playerOrderDetermined;
    }

}
