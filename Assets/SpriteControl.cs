using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteControl : MonoBehaviour
{
    // Start is called before the first frame update
    // Exemplo de configuração do objeto alvo
public class SeuScript : MonoBehaviour
{
    public SpriteRenderer objetoAlvo;

    void Start()
    {
        TrocarSprite trocarSpriteScript = GetComponent<TrocarSprite>();
        trocarSpriteScript.ConfigurarObjetoAlvo(objetoAlvo);
    }
}


    // Update is called once per frame
    void Update()
    {
        
    }
}
