using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This data is globally avaliable. Reference like so: "float health = GameManager.Instance.gameData.playerHealth;"     or     "GameManager.Instance.gameData.money += 100;"
// No methods can go inside this file as this data will be JSON serialized and stored as save data. The only way to edit contents is from outside using the above access method.

// Check/Unlock techs like this:
// if (GameManager.Instance.gameData.unlockedBetterShelves)
// {
//     Debug.Log("Better Shelves is unlocked!");
// }
// 
// GameManager.Instance.gameData.unlockedSecurityCameras = true;
// Debug.Log("Security Cameras unlocked!");


[System.Serializable]
public class GameData
{
    public Dictionary<string, PlayerData> allPlayers; // Stores all players by ID
    public GameData()
    {
        allPlayers = new Dictionary<string, PlayerData>();
    }

    public int saveSlot;
    public float playerHealth;
    public int money;




    // Tech list


}

