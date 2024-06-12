using System.Collections.Generic;
using UnityEngine;

public class GraphRenderer : MonoBehaviour
{
    public RectTransform graphContainer; // Указатель на контейнер графика
    public GameObject spherePrefab; // Префаб Sphere, который будет использоваться для точек

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
        // Инициализация размеров графика
        graphWidth = graphContainer.sizeDelta.x;
        graphHeight = graphContainer.sizeDelta.y;

        // Рассчитываем размер сферы
        CalculateSphereSize();
    }

    // Функция для отрисовки графика
// Функция для отрисовки графика с учетом экстремумов по данным Y и X
    public void DrawGraph(List<double> dataListX, List<double> dataListY)
    {
        ClearGraph(); // Очистка предыдущего графика

        if (dataListX.Count != dataListY.Count || dataListX.Count < 2)
        {
            return;
        }

        // Находим минимальные и максимальные значения по данным Y и X
        double minYValue = FindMinValue(dataListY);
        double maxYValue = FindMaxValue(dataListY);
        double minXValue = FindMinValue(dataListX);
        double maxXValue = FindMaxValue(dataListX);

        // Преобразование данных в координаты графика с учетом найденных экстремумов
        List<Vector2> graphCoordinates =
            TransformDataToGraphCoordinates(dataListX, dataListY, minXValue, maxXValue, minYValue, maxYValue);

        // Рисуем точки на графике
        foreach (Vector2 point in graphCoordinates)
        {
            DrawPoint(point);
        }
    }

// Функция для нахождения минимального значения в списке
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

// Функция для нахождения максимального значения в списке
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

// Функция преобразования списка значений в координаты для отображения на графике с учетом экстремумов и смещения
    private List<Vector2> TransformDataToGraphCoordinates(List<double> dataListX, List<double> dataListY,
        double minXValue, double maxXValue, double minYValue, double maxYValue)
    {
        List<Vector2> coordinates = new List<Vector2>();

        double scaleX, scaleY;

        // Проверяем разницу между minXValue и maxXValue с использованием эпсилона
        if (Mathf.Approximately((float)minXValue, (float)maxXValue))
        {
            scaleX = graphWidth / 2;
        }
        else
        {
            scaleX = graphWidth / (maxXValue - minXValue);
        }

        // Проверяем разницу между minYValue и maxYValue с использованием эпсилона
        if (Mathf.Approximately((float)minYValue, (float)maxYValue))
        {
            scaleY = graphHeight / 2;
        }
        else
        {
            scaleY = graphHeight / (maxYValue - minYValue);
        }

        // Рассчитываем смещение нулевой точки графика
        float offsetX = graphWidth * 2 / 16;
        float offsetY = graphHeight * 2 / 16;

        for (int i = 0; i < dataListX.Count; i++)
        {
            // Преобразование значений из данных в координаты с учетом найденных экстремумов и смещения
            float x = (float)(((dataListX[i] - minXValue) * scaleX) - (graphWidth / 2)) + offsetX;
            float y = (float)(((dataListY[i] - minYValue) * scaleY) - (graphHeight / 2)) + offsetY;

            // Проверка на выход за пределы graphContainer
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


    // Функция для рисования точки на графике
    private void DrawPoint(Vector2 point)
    {
        GameObject sphere = Instantiate(spherePrefab, graphContainer);
        sphere.transform.localPosition = point;
        sphere.transform.localScale = Vector3.one * sphereSize;
        spheres.Add(sphere);
    }

    // Функция для очистки графика
    private void ClearGraph()
    {
        foreach (var sphere in spheres)
        {
            Destroy(sphere);
        }

        spheres.Clear();
    }

    // Функция для расчета размера сферы
    private void CalculateSphereSize()
    {
        // Вычисляем минимальное значение из ширины и высоты графика
        float minGraphSize = Mathf.Min(graphWidth, graphHeight);

        // Задаем размер сферы как доля от минимальной стороны графика
        sphereSize = minGraphSize / 50f; // Например, пусть размер сферы будет 1% от минимальной стороны графика
    }
}