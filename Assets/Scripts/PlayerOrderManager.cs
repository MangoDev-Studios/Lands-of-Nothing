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
    private int currentPlayerIndex = 0;

    private Dictionary<int, GameObject> playerDictionary = new Dictionary<int, GameObject>();

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
            currentTurn = GetCurrentPlayerTurn();

            playerRolls[currentTurn].rollValue += rollResult;
            int turnsToMove = rollResult;
            Debug.Log("Player " + currentTurn + " has " + turnsToMove + " turns to move.");
        }
    }



    private void DetermineStartingOrder()
    {
        if (!playerOrderDetermined)
        {
            firstRound = false; // First round is completed

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
        int currentPlayerId = playerOrder[currentPlayerIndex];

        if (playerDictionary.TryGetValue(currentPlayerId, out GameObject currentPlayerObject))
        {
            return currentPlayerObject.GetComponent<PlayerController>().PlayerID;
        }

        // Increment the index for the next turn
        currentPlayerIndex = (currentPlayerIndex + 1) % playerOrder.Length;
    }

    return -1;
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
