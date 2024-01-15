using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class HealHex : MonoBehaviour
{
    public GameObject hex;
    HexesBoard hexesBoard;
    public PlayerOrderManager playerOrderManager;
    public PlayerController playerController;
    public Sprite originalSprite;
    public Sprite newSprite;
     public Alterar alterar;
    // Start is called before the first frame update
    void Start()
    {
        hexesBoard = new HexesBoard();
        
    }

    // Update is called once per frame
    void Update()
    {
        int currentHex = hexesBoard.currentPosition;
        
        if (Input.GetMouseButtonDown(0))
        {
            // Assuming you want to call toggleSprite when the left mouse button is clicked
            ToggleSprite();
        }

    }

    public void ToggleSprite()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = (spriteRenderer.sprite == originalSprite) ? newSprite : originalSprite;
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found!");
        }
    }

}
