using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTemplate
{
    public string[,] nodeMap;
    public List<Vector3> waypoints;
    public int rows;
    public int cols;

    public MapTemplate(int rows, int cols)
    {
        MapGenerator mapGenerator = new MapGenerator(rows, cols);
        this.nodeMap = mapGenerator.GenerateMap();
        this.waypoints = mapGenerator.GenerateWaypoints();
        this.rows = rows;
        this.cols = cols;
    }
}
