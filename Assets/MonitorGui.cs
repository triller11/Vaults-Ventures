using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MonitorGUI : MonoBehaviour
{
    public Canvas monitorCanvas;
    public PCInteraction pcInteraction; // Reference to re-enable interaction
    public GameObject initialPanel;
    public GameObject newGamePanel;
    public GameObject loadGamePanel;

    // button references

    public UnityEngine.UI.Button newGameButton;

    public UnityEngine.UI.Button newSlot1;
    public UnityEngine.UI.Button newSlot2;
    public UnityEngine.UI.Button newSlot3;
    public UnityEngine.UI.Button newBackButton;

    public UnityEngine.UI.Button loadGameButton;

    public UnityEngine.UI.Button loadSlot1;
    public UnityEngine.UI.Button loadSlot2;
    public UnityEngine.UI.Button loadSlot3;
    public UnityEngine.UI.Button loadBackButton;


    private int selectedSlot = -1; // Stores which slot the player picks (default: none)

    private void Start()
    {
        monitorCanvas.gameObject.SetActive(false); // Hide GUI initially
        newGamePanel.gameObject.SetActive(false);
        loadGamePanel.gameObject.SetActive(false);

        // Hook up button click events
        newGameButton.onClick.AddListener(ShowNewGamePanel);
        newBackButton.onClick.AddListener(NewToMainMenu);

        loadGameButton.onClick.AddListener(ShowLoadGamePanel);
        loadBackButton.onClick.AddListener(LoadToMainMenu);

        // Assign placeholders for save slots (Future logic can go here)
        newSlot1.onClick.AddListener(() => SelectSaveSlot(1));
        newSlot2.onClick.AddListener(() => SelectSaveSlot(2));
        newSlot3.onClick.AddListener(() => SelectSaveSlot(3));

        loadSlot1.onClick.AddListener(() => Debug.Log("Load Slot 1 Selected"));
        loadSlot2.onClick.AddListener(() => Debug.Log("Load Slot 2 Selected"));
        loadSlot3.onClick.AddListener(() => Debug.Log("Load Slot 3 Selected"));

    }
    private void Update()
    {
        if (monitorCanvas.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            HideGUI();
        }
    }

    public void ShowGUI()
    {
        monitorCanvas.gameObject.SetActive(true);
    }

    public void HideGUI()
    {
        monitorCanvas.gameObject.SetActive(false);
        UnityEngine.Cursor.lockState = CursorLockMode.Locked; // Lock the mouse back to gameplay
        UnityEngine.Cursor.visible = false;
        pcInteraction.enabled = true; // Re-enable interaction
    }

    // Called when "New Game" button is pressed
    public void ShowNewGamePanel()
    {
        initialPanel.SetActive(false);   // Hide the main panel
        newGamePanel.SetActive(true); // Show the new game selection panel
    }

    // Called when "Back" button in new game panel is pressed
    public void NewToMainMenu()
    {
        newGamePanel.SetActive(false); // Hide the new game selection panel
        initialPanel.SetActive(true);     // Show the main panel again
    }

    // Opens the Load Game Panel
    public void ShowLoadGamePanel()
    {
        initialPanel.SetActive(false);
        loadGamePanel.SetActive(true);
    }

    // Returns from Load Game Panel to Main Menu
    public void LoadToMainMenu()
    {
        loadGamePanel.SetActive(false);
        initialPanel.SetActive(true);
    }

    public void SelectSaveSlot(int slot)
    {
        selectedSlot = slot;
        Debug.Log($"Save Slot {slot} selected.");
        PlayerPrefs.SetInt("SelectedSaveSlot", selectedSlot); // Store slot persistently
        PlayerPrefs.Save(); // Write to disk
        StartNewGame(); // Now that a slot is selected, start the game
    }


    public void StartNewGame()
    {
        if (selectedSlot == -1)
        {
            Debug.LogWarning("No save slot selected! Cannot start game.");
            return;
        }

        Debug.Log($"Starting New Game in Slot {selectedSlot}...");
        PlayerPrefs.SetInt("SelectedSaveSlot", selectedSlot); // Store slot selection
        PlayerPrefs.Save(); // Save data persistently

        SceneManager.LoadScene("GameWorld"); // Use the exact name of your game scene
    }

}

