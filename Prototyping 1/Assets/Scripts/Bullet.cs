using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bullet
{
    public int direction;
    public int movesLeft;
    public GameObject body;
    public int type; // 0 = default, 1 = toxic, 2 = warp, 3 = recoil, 4 = TShot

    public Bullet(int dir, int length, int bulletType = 0)
    {
        direction = dir;
        movesLeft = length;
        type = bulletType;
    }

}
