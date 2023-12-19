using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    Dictionary<Vector3Int, Hex> hexTileDict =  new Dictionary<Vector3Int, Hex>();
    Dictionary<Vector3Int, List<Vector3Int>> hexTileNeighoursDict = new Dictionary<Vector3Int, List<Vector3Int>>();

    private void Start()
    {
        foreach (Hex hex in FindObjectsOfType<Hex>())
        {
            hexTileDict[hex.HexCoords] = hex;
        }
    }

    public Hex GetTileAt(Vector3Int hexCoordinates)
    {
        Hex result = null;
        hexTileDict.TryGetValue(hexCoordinates, out result);
        return result;
    }

    public List<Vector3Int> GetNeighboursFor(Vector3Int hexCoordinates)
    {
        if (hexTileDict.ContainsKey(hexCoordinates) == false)
            return new List<Vector3Int>();

        if (hexTileNeighoursDict.ContainsKey(hexCoordinates))
            return hexTileNeighoursDict[hexCoordinates];
        
        hexTileNeighoursDict.Add(hexCoordinates, new List<Vector3Int>());

        foreach (Vector3Int direction in Direction.GetDirectionList(hexCoordinates.y))
        {
            if (hexTileDict.ContainsKey(hexCoordinates + direction))
            {
                hexTileNeighoursDict[hexCoordinates].Add(hexCoordinates + direction);
            } 
        }
        return hexTileNeighoursDict[hexCoordinates];       
    }
    

    public static class Direction
    {
        public static List<Vector3Int> directionOffsetOdd = new List<Vector3Int>
        {
            new Vector3Int(-1,1, 0),
            new Vector3Int(0,1,0),
            new Vector3Int(0,-1,0),
            new Vector3Int(-1, -1, 0),
            new Vector3Int(-1,0,0)
        };
        public static List<Vector3Int> directionOffsetEven = new List<Vector3Int>
        {
            new Vector3Int(0,1,0),
            new Vector3Int(1,1, 0),
            new Vector3Int(1,0,0),
            new Vector3Int(1,-1,0),
            new Vector3Int(0,-1,0),
            new Vector3Int(-1,0, 0)
            
        };
    public static List<Vector3Int> GetDirectionList(int y)
        => y % 2 == 0 ? directionOffsetEven : directionOffsetOdd;
    }


}
