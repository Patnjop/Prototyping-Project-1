using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public int playerNumber;
    public bool alive;
    public string name;
    public int head;
    public int body;
    public GameObject character; //Instantiated after object made
    public int points;
    public int playerClass; // 1 = toxic, 2 = warp, 3 = recoil, 4 = T-Shot
    public bool special = false;
    public int ammo = 0;
    public int range;
    public int turnPlacement = 0;
    
    public Player (int playerNo, string charName, int playerHead, int playerBody, int classNum, GameObject optChar = null) // Add more
    {
        playerNumber = playerNo;
        name = charName;
        head = playerHead;
        body = playerBody;
        character = optChar;
        playerClass = classNum;
        alive = true;
        ammo = 10;
        range = 5;
    }
}
