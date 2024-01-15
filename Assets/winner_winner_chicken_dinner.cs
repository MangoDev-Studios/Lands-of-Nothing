using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class winner_winner_chicken_dinner : MonoBehaviour
{
    int loadedInt = 0;
    public TMP_Text playerTurnText;
    void Start()
    {
        loadedInt = PlayerPrefs.GetInt("win");
        playerTurnText.text = $"Jogador " + (loadedInt + 1) + " é o Vencedor!";
    }

    // Update is called once per frame
    void Update()
    {
        playerTurnText.text = $"Jogador " + (loadedInt + 1) + " é o Vencedor!";
    }
}
