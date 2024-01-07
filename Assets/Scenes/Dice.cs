using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private Sprite[] diceSides;
    private SpriteRenderer rend;
    private PlayerOrderManager playerOrderManager;
    private bool isRolling = false;
    private int count = 0;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
        playerOrderManager = FindObjectOfType<PlayerOrderManager>();
    }

    private void OnMouseDown()
    {
            StartCoroutine("RollTheDice");
            Debug.Log("Update Count: " + count);
            count++;
    }

    private IEnumerator RollTheDice()
    {
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

        // Pass the dice value to the PlayerOrderManager for recording
        playerOrderManager.RecordDiceRoll(finalSide);

        isRolling = false;
    }
}
