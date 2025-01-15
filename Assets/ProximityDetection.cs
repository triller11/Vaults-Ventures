using UnityEngine;

public class ProximityDetection : MonoBehaviour
{
    public bool isPlayerInProximity = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the "Player" tag
        {
            isPlayerInProximity = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInProximity = false;
        }
    }
}
