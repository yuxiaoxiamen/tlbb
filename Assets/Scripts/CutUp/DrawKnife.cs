using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawKnife : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Queue<Vector3> positions = new Queue<Vector3>();

    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.gray;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.3f;
        lineRenderer.numCapVertices = 5;
        lineRenderer.numCornerVertices = 5;
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (Input.GetMouseButton(0))
        {
            var position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));
            positions.Enqueue(position);
            
            if (positions.Count >= 10)
            {
                positions.Dequeue();
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            positions.Clear();
            lineRenderer.positionCount = 0;
        }
        if(positions.Count > 0)
        {
            lineRenderer.positionCount = positions.Count;
            var posArray = positions.ToArray();
            for(int i = posArray.Length - 1; i >= 0; --i)
            {
                lineRenderer.SetPosition(i, posArray[i]);
            }
        }
    }
}