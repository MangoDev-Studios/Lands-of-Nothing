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

     void Start()
    {
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

     void Update()
    {
        if (remainingMoves == 0 && !isMoving)
        {
            StartCoroutine(MoveToNextPlayer());
        }
    }

    public void RollDice(int rollResult)
    {
        // Update the number of moves with the dice result
        remainingMoves = rollResult;

        // Update the current player index
        currentPlayerIndex = playerOrderManager.GetCurrentPlayerTurn() - 1;
    }

    IEnumerator MoveToNextPlayer()
    {
        isMoving = true;

        // Aguarda um curto período de tempo antes de mudar para o próximo jogador
        yield return new WaitForSeconds(1f);

        playerOrderManager.AdvanceToNextPlayer();

        // Obtém o próximo jogador
        currentPlayerIndex = playerOrderManager.GetCurrentPlayerTurn() - 1;

        // Move o jogador atual para a posição inicial (pode ser ajustado conforme necessário)
        transform.position = hexGrid.GetTileCenter(Vector3Int.zero);

        isMoving = false;
    }

    IEnumerator MovePlayer()
    {
        while (remainingMoves > 0)
        {
            Vector3Int nextPosition = hexGrid.GetNearestTilePosition(transform.position);
            yield return StartCoroutine(MoveToPosition(nextPosition));
            yield return new WaitForSeconds(1f / moveSpeed);
            remainingMoves--;
        }
    }

    IEnumerator MoveToPosition(Vector3Int targetPosition)
    {
        Vector3 targetPosition3D = hexGrid.GetTileCenter(targetPosition);
        while (Vector3.Distance(transform.position, targetPosition3D) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition3D, Time.deltaTime * moveSpeed);
            yield return null;
        }
    }

}
