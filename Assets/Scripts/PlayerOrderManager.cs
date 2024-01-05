using UnityEngine;

public class PlayerOrderManager : MonoBehaviour
{
    private int[] accumulatedRolls = new int[4];
    private bool playerOrderDetermined = false;
    private int currentPlayerIndex = 0;

    public void RecordDiceRoll(int rollResult)
    {
        if (!playerOrderDetermined)
        {
            accumulatedRolls[currentPlayerIndex] += rollResult;

            // Display the current player's roll
            Debug.Log("Player " + currentPlayerIndex + " rolled: " + rollResult);

            currentPlayerIndex++;

            if (currentPlayerIndex == 4)
            {
                // All players rolled in this round, reset index for the next round
                currentPlayerIndex = 0;
            }
        }
        else
        {
            // Display each player's accumulated roll
            Debug.Log("Player " + currentPlayerIndex + " accumulated roll: " + accumulatedRolls[currentPlayerIndex]);

            // Increment the player index for the next player's turn
            currentPlayerIndex = (currentPlayerIndex + 1) % 4;
        }
    }

    public void DetermineStartingOrder()
    {
        System.Array.Sort(accumulatedRolls);
        System.Array.Reverse(accumulatedRolls);

        Debug.Log("Player order: " + string.Join(", ", accumulatedRolls));

        playerOrderDetermined = true;
    }
}
