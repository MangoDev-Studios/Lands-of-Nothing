using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
[SerializeField]
private Camera mainCamera;

public LayerMask selectionMask;

public HexGrid hexGrid;
private void Awake()
{
    if(mainCamera == null)
        mainCamera = Camera.main;
}
public void HandleClick(Vector3 mousePosition)
{
    GameObject result;
    if(FindTarget(mousePosition, out result))
    {
            Hex selectedHex = result.GetComponent<Hex>();
            List<Vector3Int> neighbours = hexGrid.GetNeighboursFor(selectedHex.HexCoords);
            Debug.Log($"Neighbours for {selectedHex.HexCoords} are:");
            foreach (Vector3Int neighbourPos in neighbours)
                {
                    Debug.Log(neighbourPos);
                }
    }
}
private bool FindTarget(Vector3 mousePosition, out GameObject result)
{
    RaycastHit hit;
    Ray ray = mainCamera.ScreenPointToRay(mousePosition);
    if (Physics.Raycast(ray, out hit, 100, selectionMask))
    {
        result = hit.collider.gameObject;
        return true;
    }
    result = null;
    return false;
}
}