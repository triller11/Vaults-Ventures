using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour
{
    public PCInteraction pcInteractionScript; // Reference to the PCInteraction script

    public Transform focusPoint; // Target position and rotation for the camera
    public Camera playerCamera; // Reference to the player's camera
    public FirstPersonController firstPersonController; // Reference to the player controller
    public Canvas monitorCanvas; // Reference to the GUI Canvas
    public float transitionDuration = 1f; // Time for the pan

    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;

    private bool isInteracting = false; // Tracks if the player is interacting

    public GameObject interactMessage; // "Press E to interact"
    public GameObject exitMessage; // "Press Esc to exit"

    public GameObject reticle; // reticle object



    private void Start()
    {
        // Ensure the interaction message is shown initially
        if (interactMessage != null)
            interactMessage.SetActive(true);

        // Ensure the exit message is hidden initially
        if (exitMessage != null)
            exitMessage.SetActive(false);

        // Ensure the reticle is visible at the start
        if (reticle != null)
            reticle.SetActive(true);

        // Ensure the GUI is hidden at the start
        if (monitorCanvas != null)
            monitorCanvas.gameObject.SetActive(false);
    }


    public void StartCameraPan()
    {
        // Disable the PCInteraction script
        if (pcInteractionScript != null)
            pcInteractionScript.enabled = false;

        // Hide the interaction prompt
        if (interactMessage != null)
            interactMessage.SetActive(false);

        // Show the exit message
        if (exitMessage != null)
            exitMessage.SetActive(true);

        // Hide the reticle
        if (reticle != null)
            reticle.SetActive(false);

        // Store the camera's original state
        originalCameraPosition = playerCamera.transform.position;
        originalCameraRotation = playerCamera.transform.rotation;

        // Disable player controls
        firstPersonController.playerCanMove = false;
        firstPersonController.cameraCanMove = false;

        StartCoroutine(PanToFocus());
    }




    private IEnumerator PanToFocus()
    {
        Vector3 startPosition = playerCamera.transform.position;
        Quaternion startRotation = playerCamera.transform.rotation;

        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;

            // Interpolate camera position and rotation
            playerCamera.transform.position = Vector3.Lerp(startPosition, focusPoint.position, elapsedTime / transitionDuration);
            playerCamera.transform.rotation = Quaternion.Lerp(startRotation, focusPoint.rotation, elapsedTime / transitionDuration);

            yield return null;
        }

        // Finalize the transition
        playerCamera.transform.position = focusPoint.position;
        playerCamera.transform.rotation = focusPoint.rotation;

        // Show the GUI
        if (monitorCanvas != null)
        {
            monitorCanvas.gameObject.SetActive(true);
        }
        isInteracting = true;

        // Unlock the cursor for interaction
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        // Check for exit interaction key
        if (isInteracting && Input.GetKeyDown(KeyCode.Escape))
        {
            EndInteraction();
        }
    }

    private void EndInteraction()
    {
        // Re-enable the PCInteraction script
        if (pcInteractionScript != null)
            pcInteractionScript.enabled = true;

        // Show the interaction prompt
        if (interactMessage != null)
            interactMessage.SetActive(true);

        // Hide the exit message
        if (exitMessage != null)
            exitMessage.SetActive(false);

        // Show the reticle
        if (reticle != null)
            reticle.SetActive(true);

        // Hide the monitor GUI
        if (monitorCanvas != null)
            monitorCanvas.gameObject.SetActive(false);

        // Restore the camera's original state
        playerCamera.transform.position = originalCameraPosition;
        playerCamera.transform.rotation = originalCameraRotation;

        // Re-enable player controls
        firstPersonController.playerCanMove = true;
        firstPersonController.cameraCanMove = true;

        // Reset the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isInteracting = false;
    }
}
