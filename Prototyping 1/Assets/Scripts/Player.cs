using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public int playerNumber;
    public bool alive;
    public string name;
    public GameObject appearance;
    public GameObject character; //Instantiated after object made
    public int points;
    public int playerClass; // 1 = toxic, 2 = warp, 3 = recoil, 4 = T-Shot
    public bool special = false;
    public int ammo = 0;
    public int range;
    
    public Player (int playerNo, string charName, GameObject appearObject, int classNum, GameObject optChar = null) // Add more
    {
        playerNumber = playerNo;
        name = charName;
        appearance = appearObject;
        character = optChar;
        playerClass = classNum;
        alive = true;
        ammo = 10;
        range = 5;
    }
}
