using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

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
    public GameObject dice;
    public GameObject player;
    public int remainingMoves = 0;
    public bool waitingForPlayer = false;
    public int currentPlayerId;
     private PlayerController playerController;


    public Dictionary<int, GameObject> playerDictionary = new Dictionary<int, GameObject>();

    void Update()
    {
        if (currentPlayerId == 0)
        {
            this.player = GameObject.Find("Character_1");
        }
        else if (currentPlayerId == 1)
        {
            this.player = GameObject.Find("Character_2");
        }
        else if (currentPlayerId == 2)
        {
            this.player = GameObject.Find("Character_3");
        }
        else if (currentPlayerId == 3)
        {
            this.player = GameObject.Find("Character_4");
        }

        //this.dice = GameObject.FindGameObjectWithTag("Dice");
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
        currentTurnCount++;

        int turnsToMove = rollResult;
        remainingMoves = turnsToMove;

        Debug.Log("Player " + playerId + " has " + turnsToMove + " turns to move.");

        StartCoroutine(WaitForPlayerMoves());
    }


    public int GetCurrentPlayerTurn()
    {
        if (playerOrderDetermined && playerOrder.Length > 0)
        {
            Debug.Log("CurrentPlayer" + currentPlayerIndex);
            Debug.Log("Array " + playerOrder[currentPlayerIndex]);

            if (currentPlayerIndex < playerOrder.Length)
            {
                currentPlayerId = playerOrder[currentPlayerIndex];

                Debug.Log("PlayerOrder[currentPlayerIndex] " + currentPlayerIndex);

                if (playerDictionary.TryGetValue(currentPlayerId, out GameObject currentPlayerObject))
                {
                    Debug.Log("CurrentPlayerObject " + currentPlayerObject.GetComponent<PlayerController>().PlayerID);
                    return currentPlayerObject.GetComponent<PlayerController>().PlayerID;
                }
            }
        }
        return -1;
    }

   public IEnumerator WaitForPlayerMoves()
    {
        waitingForPlayer = true;

        while (remainingMoves > 0)
        {
            dice.SetActive(false);  // Disable the dice GameObject
            playerController.CheckBlock();
            yield return null; // Aguarda o próximo frame
        }

        waitingForPlayer = false;
        dice.SetActive(true);  // Enable the dice GameObject
        AdvanceToNextPlayer();
    }

    public void AdvanceToNextPlayer()
    {
        currentPlayerIndex += 1;
        currentPlayerId = GetCurrentPlayerTurn();
        remainingMoves = 0; // Reinicia os movimentos para o próximo jogador
    }

    public void PlayerMoveCompleted()
    {
        remainingMoves--;
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
