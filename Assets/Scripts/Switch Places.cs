using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchPlaces : MonoBehaviour
{
    int image = 0;
      // zona seca = 0
      // zona verde = 1

    public Image pieceimage;
    public Sprite zonaverde;
    public Sprite zonaseca;

    public void imagechange()
    {
        if (image == 0) // troca para zona verde
        {
            pieceimage.sprite = zonaverde; 
            image = 1;
        }
        if (image == 1) // troca para zona seca
        {
            pieceimage.sprite = zonaseca;
            image = 0;
        }
    }
}
