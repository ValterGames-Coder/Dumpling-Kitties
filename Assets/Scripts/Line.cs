using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    [HideInInspector] public List<Vector3> points = new List<Vector3>();

    public void SetPosition(Vector2 position)
    {
        if(CanAppend(position) == false) return;

        points.Add(position);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, position);
    }
    
    private bool CanAppend(Vector2 position)
    {
        if (lineRenderer.positionCount == 0) return true;

        return Vector2.Distance(lineRenderer.GetPosition(lineRenderer.positionCount - 1), position) > DrawLine.RESOLUTION;
    }
}
