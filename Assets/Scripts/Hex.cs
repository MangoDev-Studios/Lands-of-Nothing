using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SelectionBase]
public class Hex : MonoBehaviour
{
    private HexCoordinates hexCoordinates;

    public Vector3Int HexCoords => hexCoordinates.GetHexCoords();

    void Awake()
    {
        hexCoordinates = GetComponent<HexCoordinates>();
    }
}
