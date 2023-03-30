using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private float pointBuffer = .1f;

    private GameObject currentLine;
    private LineRenderer lineRenderer;
    private List<Vector2> mousePositions = new List<Vector2>();
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            CreateLine();

        if (Input.GetMouseButton(0))
        {
            Vector2 temp = cam.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(temp, mousePositions[mousePositions.Count - 1]) > pointBuffer)
            {
                UpdateLine(temp);
            }
        }
    }

    private void CreateLine()
    {
        currentLine = Instantiate(linePrefab, Vector2.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        mousePositions.Clear();

        print(cam.ScreenToWorldPoint(Input.mousePosition));
        mousePositions.Add(cam.ScreenToWorldPoint(Input.mousePosition));
        mousePositions.Add(cam.ScreenToWorldPoint(Input.mousePosition));

        lineRenderer.SetPosition(0, mousePositions[0]);
        lineRenderer.SetPosition(1, mousePositions[1]);
    }

    private void UpdateLine(Vector2 newMousePosition)
    {
        mousePositions.Add(newMousePosition);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newMousePosition);

    }
}
