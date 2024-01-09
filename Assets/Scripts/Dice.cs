using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private Sprite[] diceSides;
    private SpriteRenderer rend;
    private PlayerOrderManager playerOrderManager;
    private bool isRolling = false;
    private int currentPlayerId = 0;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
        playerOrderManager = FindObjectOfType<PlayerOrderManager>();
    }

    private void OnMouseDown()
    {
        StartCoroutine("RollTheDice");
    }

    private IEnumerator RollTheDice()
    {
        if (!isRolling)
        {
            isRolling = true;

            Debug.Log("RollDice: " + isRolling);

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

            playerOrderManager.RecordDiceRoll(currentPlayerId, finalSide);

            currentPlayerId = (currentPlayerId + 1) % 4;

            isRolling = false;
        }
    }
}
