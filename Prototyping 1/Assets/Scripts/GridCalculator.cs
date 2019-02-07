using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridCalculator
{
    public static Vector2 PointsToGrid(Vector2 points)
    {
        //Debug.Log(points.x + "," + points.y);
        points.x -= 4.5f;
        points.y -= 4.5f;
        //Debug.Log(points.x + "," + points.y);
        return points;
    }
}
