using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public int playerNumber;
    public string name;
    public GameObject appearance;
    public GameObject character; //Instantiated after object made
    
    public Player (int playerNo, string charName, GameObject charObject) // Add more
    {
        playerNumber = playerNo;
        name = charName;
        appearance = charObject;
    }
}
