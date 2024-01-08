using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SelectionBase]
public class Hex : MonoBehaviour
{
    private HexCoordinates hexCoordinates;

    //public Vector3Int HexCoords => hexCoordinates.GetHexCoords();

    public Vector3Int HexCoords
    {
        get
        {
            if (hexCoordinates != null)
            {
                return hexCoordinates.GetHexCoords();
            }
            else
            {
                Debug.LogError("HexCoordinates não está associado a nenhum Objeto.");
                return Vector3Int.zero;
            }
        }
    }

    void Awake()
    {
        hexCoordinates = GetComponent<HexCoordinates>();
    }
}
