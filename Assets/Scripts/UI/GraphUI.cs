using System.Collections.Generic;
using UnityEngine;

public class GraphRenderer : MonoBehaviour
{
    public RectTransform graphContainer; // ��������� �� ��������� �������
    public GameObject spherePrefab; // ������ Sphere, ������� ����� �������������� ��� �����

    private List<GameObject> spheres = new List<GameObject>();

    private double minX = 0;
    private double maxX = 10;
    private double minY = 0;
    private double maxY = 100;

    private float graphWidth;
    private float graphHeight;
    private float sphereSize;

    private void Awake()
    {
        // ������������� �������� �������
        graphWidth = graphContainer.sizeDelta.x;
        graphHeight = graphContainer.sizeDelta.y;

        // ������������ ������ �����
        CalculateSphereSize();
    }

    // ������� ��� ��������� �������
// ������� ��� ��������� ������� � ������ ����������� �� ������ Y � X
    public void DrawGraph(List<double> dataListX, List<double> dataListY)
    {
        ClearGraph(); // ������� ����������� �������

        if (dataListX.Count != dataListY.Count || dataListX.Count < 2)
        {
            return;
        }

        // ������� ����������� � ������������ �������� �� ������ Y � X
        double minYValue = FindMinValue(dataListY);
        double maxYValue = FindMaxValue(dataListY);
        double minXValue = FindMinValue(dataListX);
        double maxXValue = FindMaxValue(dataListX);

        // �������������� ������ � ���������� ������� � ������ ��������� �����������
        List<Vector2> graphCoordinates =
            TransformDataToGraphCoordinates(dataListX, dataListY, minXValue, maxXValue, minYValue, maxYValue);

        // ������ ����� �� �������
        foreach (Vector2 point in graphCoordinates)
        {
            DrawPoint(point);
        }
    }

// ������� ��� ���������� ������������ �������� � ������
    private double FindMinValue(List<double> dataList)
    {
        double minValue = double.MaxValue;
        foreach (double value in dataList)
        {
            if (value < minValue)
                minValue = value;
        }

        return minValue;
    }

// ������� ��� ���������� ������������� �������� � ������
    private double FindMaxValue(List<double> dataList)
    {
        double maxValue = double.MinValue;
        foreach (double value in dataList)
        {
            if (value > maxValue)
                maxValue = value;
        }

        return maxValue;
    }

// ������� �������������� ������ �������� � ���������� ��� ����������� �� ������� � ������ ����������� � ��������
    private List<Vector2> TransformDataToGraphCoordinates(List<double> dataListX, List<double> dataListY,
        double minXValue, double maxXValue, double minYValue, double maxYValue)
    {
        List<Vector2> coordinates = new List<Vector2>();

        double scaleX, scaleY;

        // ��������� ������� ����� minXValue � maxXValue � �������������� ��������
        if (Mathf.Approximately((float)minXValue, (float)maxXValue))
        {
            scaleX = graphWidth / 2;
        }
        else
        {
            scaleX = graphWidth / (maxXValue - minXValue);
        }

        // ��������� ������� ����� minYValue � maxYValue � �������������� ��������
        if (Mathf.Approximately((float)minYValue, (float)maxYValue))
        {
            scaleY = graphHeight / 2;
        }
        else
        {
            scaleY = graphHeight / (maxYValue - minYValue);
        }

        // ������������ �������� ������� ����� �������
        float offsetX = graphWidth * 2 / 16;
        float offsetY = graphHeight * 2 / 16;

        for (int i = 0; i < dataListX.Count; i++)
        {
            // �������������� �������� �� ������ � ���������� � ������ ��������� ����������� � ��������
            float x = (float)(((dataListX[i] - minXValue) * scaleX) - (graphWidth / 2)) + offsetX;
            float y = (float)(((dataListY[i] - minYValue) * scaleY) - (graphHeight / 2)) + offsetY;

            // �������� �� ����� �� ������� graphContainer
            if (Mathf.Approximately((float)minXValue, (float)maxXValue))
            {
                x = 0;
            }


            if (Mathf.Approximately((float)minYValue, (float)maxYValue))
            {
                y = 0;
            }


            if (Mathf.Abs((float)maxXValue - (float)minXValue) < 0.01f * Mathf.Abs((float)maxXValue))
            {
                x = 0;
            }

            if (Mathf.Abs((float)maxYValue - (float)minYValue) < 0.01f * Mathf.Abs((float)maxYValue))
            {
                y = 0;
            }

            x = Mathf.Clamp(x, -graphWidth / 2, graphWidth / 2);
            y = Mathf.Clamp(y, -graphHeight / 2, graphHeight / 2);

            coordinates.Add(new Vector2(x, y));
        }

        return coordinates;
    }


    // ������� ��� ��������� ����� �� �������
    private void DrawPoint(Vector2 point)
    {
        GameObject sphere = Instantiate(spherePrefab, graphContainer);
        sphere.transform.localPosition = point;
        sphere.transform.localScale = Vector3.one * sphereSize;
        spheres.Add(sphere);
    }

    // ������� ��� ������� �������
    private void ClearGraph()
    {
        foreach (var sphere in spheres)
        {
            Destroy(sphere);
        }

        spheres.Clear();
    }

    // ������� ��� ������� ������� �����
    private void CalculateSphereSize()
    {
        // ��������� ����������� �������� �� ������ � ������ �������
        float minGraphSize = Mathf.Min(graphWidth, graphHeight);

        // ������ ������ ����� ��� ���� �� ����������� ������� �������
        sphereSize = minGraphSize / 50f; // ��������, ����� ������ ����� ����� 1% �� ����������� ������� �������
    }
}