using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private Sprite[] diceSides;
    private SpriteRenderer rend;
    private PlayerOrderManager playerOrderManager;
    private bool isRolling = false;
    private int currentPlayerId = 0;
    public AudioSource audioData;

 
    private void Start()
    {
        audioData = GetComponent<AudioSource>();
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
        playerOrderManager = FindObjectOfType<PlayerOrderManager>();
    }

    private void OnMouseDown()
    {
        if(playerOrderManager.waitingForPlayer == false)
        StartCoroutine("RollTheDice");
    }

    private IEnumerator RollTheDice()
{
    
    if (!isRolling)
    {
        audioData.Play();
        isRolling = true;

        int randomDiceSide = 0;
        int finalSide = 0;

        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0, 5);
            rend.sprite = diceSides[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
        }

        finalSide = randomDiceSide + 1;
        Debug.Log("Final Dice Value: " + finalSide);

        if (!playerOrderManager.IsPlayerOrderDetermined())
        {
            playerOrderManager.RecordDiceRoll(currentPlayerId, finalSide);
            currentPlayerId = (currentPlayerId + 1) % 4;
        }
        else
        {
            int currentTurn = playerOrderManager.GetCurrentPlayerTurn();
            Debug.Log("Current Player Turn: " + currentTurn);

            playerOrderManager.RecordDiceRoll(currentTurn, finalSide);
        }

        isRolling = false;
    }
}
}
