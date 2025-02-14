using UnityEngine;

public class PCInteraction : MonoBehaviour
{
    public Camera playerCamera; // Assign the player's camera in the Inspector
    public float maxDistance = 5f; // Maximum distance to detect the PC
    public LayerMask pcLayer; // Layer mask for PC interaction
    public GameObject interactionPrompt; // Assign UI prompt here
    public ProximityDetection proximityScript; // Assign the ProximityDetection script
    public MonitorGUI monitorGUI; // Reference to the MonitorGUI script


    private void Update()
    {
        if (proximityScript.isPlayerInProximity && IsLookingAtPC())
        {
            interactionPrompt.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                interactionPrompt.SetActive(false); // Hide the prompt during the transition
                GetComponent<CameraPan>().StartCameraPan(); // Trigger the camera pan

                monitorGUI.ShowGUI(); // Show the monitor UI
            }
        }
        else
        {
            interactionPrompt.SetActive(false);
        }
    }

    private bool IsLookingAtPC()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, pcLayer))
        {
            return true;
        }

        return false;
    }
}
