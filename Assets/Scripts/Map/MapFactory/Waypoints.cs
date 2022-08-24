using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Waypoints
{
    public static List<Vector3> waypointList = new List<Vector3>();

    public static List<Vector3> GenerateWaypoints(List<Vector2Int> pathList, Vector3 startPos)
    {
        waypointList.Clear();
        Vector3 current = startPos;
        Vector3 previous = new Vector3();
        bool changeZ = false;
        bool changeY = false;

        for(int i = 0; i < pathList.Count; i++)
        {
            previous = current;
            current = new Vector3(pathList[i].x, 0, pathList[i].y) + startPos;

            if (previous.x != current.x && changeY)
            {
                waypointList.Add(previous);
            }

            if (previous.z != current.z && changeZ)
            {
                waypointList.Add(previous);
            }

            if (previous.x != current.x)
            {
                changeZ = true;
                changeY = false;
            }

            if (previous.z != current.z)
            {
                changeY = true;
                changeZ = false;
            }
        }

        waypointList.Add(new Vector3(pathList[pathList.Count - 1].x, 0 , pathList[pathList.Count-1].y) + startPos);
        return waypointList;
    }

    public static void AddWaypoint(Vector3 waypoint)
    {
        waypointList.Add(waypoint);
    }

    public static Vector3 GetStartPosition()
    {
        return waypointList[0];
    }

    public static Vector3 GetWaypoint(int index)
    {
        return waypointList[index];
    }
}
