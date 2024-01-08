using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private Sprite[] diceSides;
    private SpriteRenderer rend;
    private PlayerOrderManager playerOrderManager;
    private bool isRolling = false;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
        playerOrderManager = FindObjectOfType<PlayerOrderManager>();
    }

    private void OnMouseDown()
    {
<<<<<<< HEAD
        if (Input.GetMouseButtonDown(0) && !isRolling)
        {
            Debug.Log("Entrou: " + isRolling);
            isRolling = true;
            StartCoroutine(RollTheDice());
        }
=======
            StartCoroutine("RollTheDice");
            Debug.Log("Update Count: " + count);
            count++;
>>>>>>> 4749805db1714ad591d746478e7504061a705106
    }

    private IEnumerator RollTheDice()
    {
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

        isRolling = false;
    }
}
