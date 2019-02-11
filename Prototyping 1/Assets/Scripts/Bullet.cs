using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet
{
    public int direction;
    public int movesLeft;
    public GameObject body;
    public int type; // 0 = default

    public Bullet(int dir, int length, int bulletType = 0)
    {
        direction = dir;
        movesLeft = length;
        type = bulletType;
    }

}
