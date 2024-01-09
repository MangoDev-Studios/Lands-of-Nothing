using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchPlaces : MonoBehaviour
{
    public Image pieceimage;
    public Sprite zonaverde;
    public Sprite zonaseca;
    public void imagechange()
    {
        if (pieceimage == zonaseca) // troca para zona verde
        {
            pieceimage.sprite = zonaverde; 
        }
        if (pieceimage == zonaverde) // troca para zona seca
        {
            pieceimage.sprite = zonaseca;
        }
    }
}

