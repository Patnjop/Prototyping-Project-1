using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public int player;
    public int moveNumber;
    public int moveType; //1 = move, 2 = shoot
    public int direction; //1 = top, right = 2, etc

    public Move(int p, int moveNo, int moveT, int dir)
    {
        player = p;
        moveNumber = moveNo;
        moveType = moveT;
        direction = dir;
    }
}
