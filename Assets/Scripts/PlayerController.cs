using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int PlayerID { get; private set; }

        public void PerformPlayerAction()
    {
        Debug.Log("Player " + PlayerID + " is performing an action.");
    }
}
