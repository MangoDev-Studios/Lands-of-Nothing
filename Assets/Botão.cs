using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botão : MonoBehaviour
{
    Alterar alterar;
    // Start is called before the first frame update
    void Start()
    {
        alterar = new Alterar();
    }

    // Update is called once per frame
    void Update()
    {
        alterar.ToggleSprite();
    }
}
