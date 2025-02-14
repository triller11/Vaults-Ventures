using UnityEngine;
using Steamworks; // Only if using Steam for multiplayer

public class PlayerController : MonoBehaviour
{
    public PlayerData playerData;
    public string playerID;

    private void Start()
    {
        // Load the Player ID from PlayerPrefs (or Steam ID if online)
        if (PlayerPrefs.HasKey("PlayerID"))
        {
            playerID = PlayerPrefs.GetString("PlayerID");
        }
        else
        {
            playerID = System.Guid.NewGuid().ToString();
            PlayerPrefs.SetString("PlayerID", playerID);
            PlayerPrefs.Save();
        }

        if (SteamManager.Initialized)
        {
            playerID = SteamUser.GetSteamID().ToString();
        }

        // Load the player's saved data
        playerData = SaveManager.Instance.LoadOrCreatePlayerData(playerID);

        // **THIS APPLIES THE LOADED STATE TO THE PLAYER!**
        ApplyLoadedData();
    }

    private void ApplyLoadedData()
    {
        // Restore health
        SetHealth(playerData.health);

        // Restore inventory (if applicable)
        foreach (string item in playerData.inventory)
        {
            AddItemToInventory(item);
        }

        Debug.Log($"Player {playerID} loaded with {playerData.health} health and {playerData.inventory.Count} inventory items.");
    }

    private void SetHealth(float health)
    {
        // Example of setting health (Modify this to match your health system)
        Debug.Log($"Setting player health to: {health}");
    }

    private void AddItemToInventory(string item)
    {
        // Example function that adds items back into player's inventory
        Debug.Log($"Adding item to inventory: {item}");
    }
}
