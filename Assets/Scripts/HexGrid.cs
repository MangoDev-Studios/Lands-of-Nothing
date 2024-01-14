using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    Dictionary<Vector3Int, Hex> hexTileDict = new Dictionary<Vector3Int, Hex>();
    Dictionary<Vector3Int, List<Vector3Int>> hexTileNeighboursDict = new Dictionary<Vector3Int, List<Vector3Int>>();

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
    if (hexTileNeighboursDict.TryGetValue(hexCoordinates, out List<Vector3Int> cachedNeighbours))
    {
        return cachedNeighbours;
    }

    List<Vector3Int> neighbours = new List<Vector3Int>();

    foreach (Vector3Int direction in Direction.GetDirectionList(hexCoordinates.y))
    {
        Vector3Int neighborCoord = hexCoordinates + direction;
        if (hexTileDict.ContainsKey(neighborCoord))
        {
            neighbours.Add(neighborCoord);
        }
    }

    hexTileNeighboursDict[hexCoordinates] = neighbours;
    return neighbours;
}

    public Vector3Int GetNearestTilePosition(Vector3 worldPosition)
    {
        Vector3Int nearestTilePosition = Vector3Int.zero;
        float nearestDistance = float.MaxValue;

        foreach (var hexTile in hexTileDict.Values)
        {
            Vector3 tileWorldPosition = hexTile.transform.position;
            float distance = Vector3.Distance(worldPosition, tileWorldPosition);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestTilePosition = hexTile.HexCoords;
            }
        }

        return nearestTilePosition;
    }

    public Vector3 GetTileCenter(Vector3Int hexCoordinates)
    {
        if (hexTileDict.TryGetValue(hexCoordinates, out Hex hexTile))
        {
            return hexTile.transform.position;
        }
        return Vector3.zero;
    }

    public void UpdateHexagonPosition(Vector3Int hexCoordinates)
{
    if (hexTileNeighboursDict.ContainsKey(hexCoordinates))
    {
        // Invalidate the cache
        hexTileNeighboursDict.Remove(hexCoordinates);
    }
    // GetNeighboursFor will recalculate and cache the neighbors next time it's called.
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
