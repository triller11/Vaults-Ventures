using UnityEngine;
using Steamworks; // Only if using Steam for multiplayer

public class PlayerController : MonoBehaviour
{
    public PlayerData playerData;
    public string playerID;

    private void Start()
    {
        // Try to get Steam ID if Steam is available
        if (SteamManager.Initialized)
        {
            playerID = SteamUser.GetSteamID().ToString();
            PlayerPrefs.SetString("BackupPlayerID", playerID); // Store Steam ID as backup
            PlayerPrefs.Save();
        }
        else
        {
            // If Steam isn't available, fall back to stored PlayerPrefs ID
            if (PlayerPrefs.HasKey("BackupPlayerID"))
            {
                playerID = PlayerPrefs.GetString("BackupPlayerID");
                Debug.Log($"Using Backup Player ID: {playerID} (Offline Mode)");
            }
            else
            {
                // No previous ID stored, generate a new one
                playerID = System.Guid.NewGuid().ToString();
                PlayerPrefs.SetString("BackupPlayerID", playerID);
                PlayerPrefs.Save();
                Debug.Log($"Generated new Backup Player ID: {playerID}");
            }
        }

        // Load the player's saved data, now correctly scoped to save slot
        playerData = SaveManager.Instance.LoadOrCreatePlayerData(playerID, PlayerPrefs.GetInt("SelectedSaveSlot", 1));
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
