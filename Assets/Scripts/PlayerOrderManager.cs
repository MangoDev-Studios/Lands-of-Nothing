using UnityEngine;
using System;

public class PlayerOrderManager : MonoBehaviour
{
    private struct PlayerRoll
    {
        public int playerId;
        public int rollValue;
    }

    private PlayerRoll[] playerRolls = new PlayerRoll[4];
    private bool playerOrderDetermined = false;

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

            Debug.Log("Player order: " + string.Join(", ", Array.ConvertAll(playerRolls, p => p.playerId)));

            playerOrderDetermined = true;
        }
        else
        {
            Debug.LogWarning("Player order already determined.");
        }
    }
}
