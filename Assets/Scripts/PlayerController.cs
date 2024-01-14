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
    public AudioSource audioSource;

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
                audioSource.Play();
                MoveToPosition(targetPosition);
            }
        }
    }

    bool IsNeighbor(Vector3 targetPosition)
    {
        // Obtém a posição do hexágono mais próximo ao ponto de clique
        Vector3Int nearestTilePosition = hexGrid.GetNearestTilePosition(targetPosition);

        // Obtém a posição do hexágono atual do jogador
        Vector3Int currentPlayerTilePosition = hexGrid.GetNearestTilePosition(transform.position);

        // Calcula a diferença em coordenadas axiais entre os hexágonos
        int deltaX = Mathf.Abs(nearestTilePosition.x - currentPlayerTilePosition.x);
        int deltaY = Mathf.Abs(nearestTilePosition.y - currentPlayerTilePosition.y);

        // Verifica se a diferença está dentro dos limites para ser considerado um vizinho
        // Neste exemplo, assumimos que apenas hexágonos adjacentes horizontalmente e diagonalmente são vizinhos válidos
        return deltaX <= 1 && deltaY <= 1;
    }


    void MoveToPosition(Vector3 targetPosition)
    {
        Debug.Log("PlayerId" + PlayerID);

        // Garante que o jogador está exatamente na posição final
        transform.position = targetPosition;

        // Reduz o número de movimentos no PlayerOrderManager
        playerOrderManager.PlayerMoveCompleted();
    }
}
