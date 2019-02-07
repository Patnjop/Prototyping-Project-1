using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public int playerNumber;
    public string name;
    public GameObject character;
    
    public Player (int playerNo, string charName, GameObject charObject) // Add more
    {
        playerNumber = playerNo;
        name = charName;
        character = charObject;
    }
}
