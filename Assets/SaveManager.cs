using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; } // Singleton instance

    private int saveSlot;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps SaveManager alive across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        saveSlot = PlayerPrefs.GetInt("SelectedSaveSlot", -1);

        if (saveSlot == -1)
        {
            Debug.LogWarning("No save slot selected! Defaulting to slot 3.");
            saveSlot = 3;
        }

        Debug.Log($"Game started with Save Slot: {saveSlot}");

        // Load game data from this slot
        GameManager.Instance.gameData = LoadGame(saveSlot);
    }

    public void SaveGame()
    {
        Debug.Log($"Saving game to slot {saveSlot}...");

        string saveFilePath = Application.persistentDataPath + $"/save_slot_{saveSlot}.json";

        string json = JsonUtility.ToJson(GameManager.Instance.gameData, true);
        File.WriteAllText(saveFilePath, json);

        Debug.Log($"Game Saved to {saveFilePath}!");
    }

    public GameData LoadGame(int slot)
    {
        string saveFilePath = Application.persistentDataPath + $"/save_slot_{slot}.json";

        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            Debug.Log($"Game Loaded from {saveFilePath}");
            return data;
        }
        else
        {
            Debug.LogWarning("No save file found. Creating new game data.");
            return new GameData { saveSlot = slot, money = 0 };
        }
    }

    public PlayerData LoadOrCreatePlayerData(string playerID, int saveSlot)
    {
        string uniqueKey = $"{saveSlot}_{playerID}"; // Scope playerID to the save slot

        if (GameManager.Instance.gameData.allPlayers.ContainsKey(uniqueKey))
        {
            Debug.Log($"Loaded existing data for Player ID: {playerID} in Save Slot {saveSlot}");
            return GameManager.Instance.gameData.allPlayers[uniqueKey];
        }
        else
        {
            Debug.Log($"Creating new player data for Player ID: {playerID} in Save Slot {saveSlot}");
            PlayerData newPlayer = new PlayerData(playerID);
            GameManager.Instance.gameData.allPlayers[uniqueKey] = newPlayer;
            return newPlayer;
        }
    }

}
