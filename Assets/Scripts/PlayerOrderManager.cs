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

            Debug.Log("Player " + currentPlayerIndex + " rolled: " + rollResult);

            currentPlayerIndex++;

            if (currentPlayerIndex == 4)
            {
                DetermineStartingOrder();
            }
        }
        else
        {
            Debug.Log("Player " + currentPlayerIndex + " accumulated roll: " + accumulatedRolls[currentPlayerIndex]);

            // Move to the next player
            currentPlayerIndex = (currentPlayerIndex + 1) % 4;
        }
    }


    // Precisa de ordenar plyer id e n o valor retornado do dice
    private void DetermineStartingOrder()
    {
        if (!playerOrderDetermined)
        {
            System.Array.Sort(accumulatedRolls);
            System.Array.Reverse(accumulatedRolls);

            Debug.Log("Player order: " + string.Join(", ", accumulatedRolls));

            playerOrderDetermined = true;
        }
        else
        {
            Debug.LogWarning("Player order already determined.");
        }
    }
}
