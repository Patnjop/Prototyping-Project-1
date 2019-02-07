using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridCalculator
{
    public static Vector2 PointsToGrid(Vector2 points)
    {
        points.x -= 4.5f;
        points.y += 4.5f;
        return points;
    }
}
