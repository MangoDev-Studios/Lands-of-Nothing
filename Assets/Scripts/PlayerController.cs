using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // PlayerID property with a public set accessor
    public int PlayerID { get; set; }

    // Additional player-related code can be added here

    // Example method to handle player-specific behavior
    public void PerformPlayerAction()
    {
        Debug.Log("Player " + PlayerID + " is performing an action.");
        // Add your player-specific logic here
    }
}
