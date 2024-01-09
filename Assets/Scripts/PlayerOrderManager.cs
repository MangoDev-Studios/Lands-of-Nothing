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
    private int[] playerOrder;

    // Dictionary to associate PlayerID with GameObject
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
            Debug.Log("Player " + playerId + " accumulated roll: " + playerRolls[playerId].rollValue);
        }
    }

    private void DetermineStartingOrder()
{
    if (!playerOrderDetermined)
    {
        Array.Sort(playerRolls, (p1, p2) => p2.rollValue.CompareTo(p1.rollValue));

        playerOrder = Array.ConvertAll(playerRolls, p => p.playerId);

        Debug.Log("Player order: " + string.Join(", ", playerOrder));

        playerOrderDetermined = true;

        // Populate the playerDictionary with PlayerID and corresponding GameObject
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            PlayerController playerController = players[i].GetComponent<PlayerController>();
            if (playerController != null)
            {
                // Assign a unique ID based on the order of players
                int playerId = i;
                playerController.PlayerID = playerId;

                // Check if the ID is already present before adding to the dictionary
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


    public int GetCurrentPlayerTurn()
{
    if (playerOrderDetermined && playerOrder.Length > 0)
    {
        int currentPlayerId = playerOrder[0];

        if (playerDictionary.TryGetValue(currentPlayerId, out GameObject currentPlayerObject))
        {
            return currentPlayerObject.GetComponent<PlayerController>().PlayerID;
        }
    }

    return -1;
}


    public int GetPlayerTurns(int playerId)
{
    if (playerOrderDetermined)
    {
        int index = Array.IndexOf(playerOrder, playerId);
        return index + 1;
    }

    return -1;
}

}
