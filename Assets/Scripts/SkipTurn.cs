using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMan : MonoBehaviour
{
    public int turn;
    public GameObject dice;
    public GameObject player;
    private int receivedDiceValue;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        turn = receivedDiceValue;
        if (turn == 1)
        {
            this.player = GameObject.FindWithTag("Player1");
        }
        else if (turn == 2)
        {
            this.player = GameObject.FindWithTag("Player2");
        }
        else if (turn == 3)
        {
            this.player = GameObject.FindWithTag("Player3");
        }
        else if (turn == 4)
        {
            this.player = GameObject.FindWithTag("Player4");
        }
    }

        public void ReceiveDiceValue(int value) {
        receivedDiceValue = value;

        // Do whatever you need to do with the received value
        Debug.Log("Received Dice Value: " + receivedDiceValue);
    }
    
}
