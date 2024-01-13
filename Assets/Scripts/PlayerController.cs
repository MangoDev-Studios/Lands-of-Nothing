using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform movePoint;
    public float moveSpeed = 5f;
    public GameObject topRight;
    public GameObject bottomRight;
    public GameObject bottomLeft;
    public GameObject topleft;
    public GameObject top;
    public GameObject bottom;
    public GameObject CurrentObject;
    public Camera Cam;
    private Vector3 targetPosition;
    public HexGrid hexGrid;
    private PlayerOrderManager playerOrderManager;

    // PlayerID property with a public set accessor
    public int PlayerID { get; set; }

    // Additional player-related code can be added here

    // Example method to handle player-specific behavior
    public void PerformPlayerAction()
    {
        Debug.Log("Player " + PlayerID + " is performing an action.");
        // Add your player-specific logic here
    }

    void Start()
    {
        Cam = Camera.main;
        playerOrderManager = FindObjectOfType<PlayerOrderManager>();
    }

     void Update()
    {
        if (hexGrid == null)
        {
            Debug.LogError("HexGrid not assigned to PlayerController.");
            return;
        }

        if (Input.GetMouseButtonDown(0)) // Left click
        {
            CheckBlock();
        }
    }
    public void CheckBlock()
    {
        Vector3 mousePos = Cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rayMousePos = new Vector3(mousePos.x, mousePos.y, 1f);

        RaycastHit hit;
        if (Physics.Raycast(rayMousePos, Vector3.back, out hit))
        {
            Vector3 targetPosition = new Vector3(hit.transform.position.x, hit.transform.position.y, -1f);

            // Verifica se o hexágono alvo é um vizinho válido
            if (IsNeighbor(targetPosition))
            {
                StartCoroutine(MoveToPosition(targetPosition));
            }
        }
    }

     bool IsNeighbor(Vector3 targetPosition)
    {
        // Obtém a posição do hexágono mais próximo ao ponto de clique
        Vector3Int nearestTilePosition = hexGrid.GetNearestTilePosition(targetPosition);

        // Obtém a lista de vizinhos do hexágono mais próximo
        var neighbors = hexGrid.GetNeighboursFor(nearestTilePosition);

        // Verifica se o hexágono clicado é um vizinho válido
        return neighbors.Contains(hexGrid.GetNearestTilePosition(transform.position));
    }
    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        float remainingDistance = Vector3.Distance(transform.position, targetPosition);

        // Lógica para o movimento suave do jogador para a próxima posição
        while (remainingDistance > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
            remainingDistance = Vector3.Distance(transform.position, targetPosition);
            yield return null;
        }

        // Garante que o jogador está exatamente na posição final
        transform.position = targetPosition;

        // Reduz o número de movimentos no PlayerOrderManager
        playerOrderManager.PlayerMoveCompleted();
    }


}
