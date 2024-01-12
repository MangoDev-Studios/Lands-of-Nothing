using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int PlayerID { get; set; }
    private PlayerOrderManager playerOrderManager;

    public void PerformPlayerAction()
    {
        playerOrderManager.PlayerMoveCompleted();
        Debug.Log("Player " + PlayerID + " is performing an action.");
    }
}
