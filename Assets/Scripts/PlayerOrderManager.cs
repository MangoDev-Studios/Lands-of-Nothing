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

            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            foreach (var player in players)
            {
                int playerId = player.GetComponent<PlayerController>().PlayerID;
                playerDictionary.Add(playerId, player);
            }
        }
        else
        {
            Debug.LogWarning("Player order already determined.");
        }
    }

    public int GetCurrentPlayerTurn()
    {
        if (playerOrderDetermined)
        {
            int currentPlayerId = playerOrder[0];

            if (playerDictionary.TryGetValue(currentPlayerId, out GameObject currentPlayerObject))
            {
                return currentPlayerObject.GetComponent<PlayerController>().PlayerID + 1;
            }
        }

        return -1;
    }
}
