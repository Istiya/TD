using UnityEngine;

public static class CircleDrawer
{
    public static void DrawCircle(this GameObject container, float radius, float lineWidth, LineRenderer rend = null)
    {
        var segments = 360;

        var line = new LineRenderer();

        if (rend == null)
        {
            line = container.AddComponent<LineRenderer>();
        }
        else
        {
            line = rend;
        }

        line.useWorldSpace = false;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.positionCount = segments + 1;

        var pointCount = segments + 1;
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
        }

        line.material.color = Color.blue;
        line.SetPositions(points);
    }
}