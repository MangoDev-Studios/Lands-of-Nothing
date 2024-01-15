using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class PlayerOrderManager : MonoBehaviour
{
    private struct PlayerRoll
    {
        public int playerId;
        public int rollValue;
    }
    
    public TMP_Text playerTurnText;
    public TMP_Text currentTurnText;

    public TMP_Text numActionsText;
    
    public int rolls;
    private PlayerRoll[] playerRolls = new PlayerRoll[4];
    private bool playerOrderDetermined = false;
    private bool firstRound = true; // New flag to indicate the first round
    private int[] playerOrder;
    int winnerId = -1;
    private int currentTurnCount = 0;
    public int currentTurn;
    public int currentPlayerIndex = 0;
    public GameObject dice;
    public GameObject player;
    public int[] greensidesCount = new int[4];
    public int remainingMoves = 0;
    public bool waitingForPlayer = false;
    public int currentPlayerId;
     private PlayerController playerController;
   private int rollCounter = 0;
    private const int rollsPerTurn = 4;

    public Dictionary<int, GameObject> playerDictionary = new Dictionary<int, GameObject>();

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        currentTurnText.text = " ";
        playerTurnText.text = " ";
    }

    

    void Update()
    {
        if(currentTurn == 11)
        {
                SceneManager.LoadScene("GameEnd");

                CheckWinner();
        }
        numActionsText.text = "Moves restantes: " + remainingMoves.ToString();
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
        this.playerController = player.GetComponent<PlayerController>();

        //this.dice = GameObject.FindGameObjectWithTag("Dice");
    }

    public void RecordDiceRoll(int playerId, int rollResult)
    {
        rolls++;

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

        if (rolls % 4 == 0)
        {
            currentTurn++;
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

            // Find all player GameObjects with the "Player" tag
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            // Sort the players array based on the order of player IDs in playerOrder array
            Array.Sort(players, (go1, go2) =>
            {
                string name1 = go1.name;
                string name2 = go2.name;
                return name1.CompareTo(name2);
            });

            // Create a dictionary to map player IDs to their corresponding GameObjects
            Dictionary<int, GameObject> playerDictionaryTemp = new Dictionary<int, GameObject>();

            for (int i = 0; i < players.Length; i++)
            {
                PlayerController playerController = players[i].GetComponent<PlayerController>();
                if (playerController != null)
                {
                    int playerId = i; // Assign player ID based on the sorted order
                    playerController.PlayerID = playerId;

                    if (!playerDictionaryTemp.ContainsKey(playerId))
                    {
                        playerDictionaryTemp.Add(playerId, players[i]);
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

            // Assign the sorted dictionary to playerDictionary
            playerDictionary = playerDictionaryTemp;
        }
        else
        {
            Debug.LogWarning("Player order already determined.");
        }
    }

    private void HandlePlayerOrder(int playerId, int rollResult)
    {
        int turnsToMove = rollResult;
        remainingMoves = turnsToMove;

        if (playerDictionary.TryGetValue(playerId, out GameObject currentPlayerObject))
        {
            playerController = currentPlayerObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.PlayerID = playerId;

                // Update TextMeshProUGUI elements
                Debug.Log("Setting Player Turn Text: " + "Player Turn: " + (playerId + 1));
                playerTurnText.text = "Vez do jogador:  " + (playerId + 1);
                Debug.Log("Setting Current Turn Text: " + "Current Turn: " + currentTurn);
                currentTurnText.text = "Turno atual: " + currentTurn;


                Debug.Log("Player " + playerId + " has " + turnsToMove + " turns to move.");
                StartCoroutine(WaitForPlayerMoves());
            }
            else
            {
                Debug.LogError("PlayerController not found on GameObject with PlayerID: " + playerId);
            }
        }
        else
        {
            Debug.LogError("PlayerID not found in the playerDictionary: " + playerId);
        }
    }

    public int GetCurrentPlayerTurn()
    {
        if (playerOrderDetermined && playerOrder.Length > 0)
        {
            Debug.Log("CurrentPlayer -> " + currentPlayerIndex);

            if (currentPlayerIndex < playerOrder.Length)
            {
                currentPlayerId = playerOrder[currentPlayerIndex];

                Debug.Log("Current Player Id -> " + currentPlayerId);

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
            yield return null; // Aguarda o próximo frame
        }
        waitingForPlayer = false;
        playerTurnText.text = " ";
        currentTurnText.text = "Role o dado";
        AdvanceToNextPlayer();

}

    public void AdvanceToNextPlayer()
    {
        currentPlayerIndex += 1;
        if (currentPlayerIndex == 4)
            currentPlayerIndex = 0;
        currentPlayerId = GetCurrentPlayerTurn();
        remainingMoves = 0; // Reinicia os movimentos para o próximo jogador
        this.playerController = player.GetComponent<PlayerController>();
    }

    public void PlayerMoveCompleted()
    {
        remainingMoves--;
    }

    public void HexHealed()
    {
        remainingMoves -= 2;
    }

    public int[] GetPlayerOrder()
    {
        return playerOrder;
    }

    public bool IsPlayerOrderDetermined()
    {
        return playerOrderDetermined;
    }
 
            private void CheckWinner()
        {
            int maxGreensides = 0;
            

            for (int i = 0; i < playerRolls.Length; i++)
            {
                if (greensidesCount[i] > maxGreensides)
                {
                    maxGreensides = greensidesCount[i];
                    winnerId = playerRolls[i].playerId;
                }
            }
                PlayerPrefs.SetInt("win", winnerId);
                PlayerPrefs.Save();

            Debug.Log("Player " + winnerId + " is the winner!");
        }

    
}