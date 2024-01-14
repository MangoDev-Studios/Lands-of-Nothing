using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TrocarSprite : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;

    private SpriteRenderer spriteRendererAlvo; // Referência para o SpriteRenderer do objeto alvo
    private bool alternarSprite = false;

    // Método para configurar o objeto alvo
    public void ConfigurarObjetoAlvo(SpriteRenderer alvo)
    {
        spriteRendererAlvo = alvo;

        // Define o sprite inicial do objeto alvo
        if (spriteRendererAlvo != null)
        {
            spriteRendererAlvo.sprite = sprite1;
        }
    }

    // Método público para trocar o sprite do objeto alvo
    public void TrocarSpriteClick()
    {
        // Verifica se há um objeto alvo configurado
        if (spriteRendererAlvo != null)
        {
            // Alterna entre os sprites
            alternarSprite = !alternarSprite;

            // Atualiza o sprite do objeto alvo conforme a alternância
            if (alternarSprite)
            {
                spriteRendererAlvo.sprite = sprite2;
            }
            else
            {
                spriteRendererAlvo.sprite = sprite1;
            }
        }
        else
        {
            Debug.LogWarning("Objeto alvo não configurado. Configure usando ConfigurarObjetoAlvo antes de tentar trocar o sprite.");
        }
    }
}

/*public class SwitchPlaces : MonoBehaviour
{
    public GameObject hex;
    public Sprite zonaverde;
    public Sprite zonaseca;

    public void zonechange()
    {
         SpriteRenderer spriteRenderer = hex.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Debug.Log("Current Sprite: " + spriteRenderer.sprite.name); // Log current sprite name

            if (spriteRenderer.sprite == zonaseca) // troca para zona verde
            {
                spriteRenderer.sprite = zonaverde;
            }
            else if (spriteRenderer.sprite == zonaverde) // troca para zona seca
            {
                spriteRenderer.sprite = zonaseca;
            }
        }
        else
        {
            Debug.LogWarning("SpriteRenderer component not found on the GameObject.");
        }
    }
}*/

