using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string playerID; // Unique identifier for each player
    public float health;
    public List<string> inventory;

    public PlayerData(string id)
    {
        playerID = id;
        health = 100f;
        inventory = new List<string>();
    }
}
