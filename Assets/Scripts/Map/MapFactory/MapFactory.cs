using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFactory : MonoBehaviour
{
    private Vector3 _startPos = new Vector3(3f, 1f, 1f);
    public int rows = 20;
    public int cols = 20;

    private void Awake()
    {
        BuildMap();
        Destroy(this);
    }

    void BuildMap()
    {
        MapTemplate template = new MapTemplate(rows, cols);

        BuildNodes(template);
        BuildWaypoints(template);
    }

    void BuildNodes(MapTemplate template)
    {
        string[,] nodeMap = template.nodeMap;

        for (int i = 0; i < template.rows; i++)
            for (int j = 0; j < template.cols; j++)
            {
                if (nodeMap[i, j] == "Terrain")
                {
                    GameObject newNode = Instantiate(PrefabManager.instance.Get(PrefabType.TERRAIN), new Vector3(i, 0, j) + _startPos, Quaternion.identity, transform.Find("Nodes"));
                    newNode.AddComponent<TerrainNodeController>();
                }
                else if (nodeMap[i, j] == "Path")
                {
                    Instantiate(PrefabManager.instance.Get(PrefabType.PATH), new Vector3(i, 0, j) + _startPos, Quaternion.identity, transform.Find("Nodes"));
                }
            }
    }

    void BuildWaypoints(MapTemplate mapTemplate)
    {
        List<Vector3> waypoints = mapTemplate.waypoints;

        for (int i = 0; i < waypoints.Count; i++)
        {
            Instantiate(PrefabManager.instance.Get(PrefabType.WAYPOINT), waypoints[i], Quaternion.identity, transform.Find("Waypoints"));
        }
    }
}
