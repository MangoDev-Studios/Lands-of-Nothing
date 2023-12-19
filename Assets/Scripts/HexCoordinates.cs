using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCoordinates : MonoBehaviour
{
    public static float xOffset = 2, yOffset = 1.73f, zOffset = 1;

    internal Vector3Int GetHexCoords()
        => offsetCoordinates;

    [Header("Offset Coordinates")]
    [SerializeField]
    private Vector3Int offsetCoordinates;

    private void Awake()
    {
        offsetCoordinates = ConvertPositionToOffset(transform.position);
    }

    private Vector3Int ConvertPositionToOffset(Vector3 position)
    {
        int x = Mathf.CeilToInt(position.x / xOffset);
        int y = Mathf.CeilToInt(position.y / yOffset);
        int z = Mathf.CeilToInt(position.z / zOffset);
        return new Vector3Int(x, y, z);
    }
}
