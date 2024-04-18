using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBehaviour : MonoBehaviour
{
    [SerializeField] public Canvas menuCanvas;
    [SerializeField] public Canvas settingsCanvas;
    [SerializeField] public Button settingsButton;
    
    public float duration = 1f; // Длительность анимации в секундах
    public float moveDistance = (float)Screen.height*2.0f; // Расстояние, на которое Canvas должен быть перемещён вниз

    public void ShowSettings()
    {
        settingsCanvas.enabled = true;
        menuCanvas.enabled = false;
    }
    public void HideMenu()
    {
       moveDistance = (float)Screen.height; // Расстояние, на которое Canvas должен быть перемещён вниз
    StartCoroutine(HideMenuCoroutine());
    }
    private IEnumerator HideMenuCoroutine()
    {
        float elapsedTime = 0;

        // Получаем начальные позиции всех дочерних элементов
        RectTransform[] children = menuUI.GetComponentsInChildren<RectTransform>();

        Vector3[] startPositions = new Vector3[children.Length];
        Vector3[] targetPositions = new Vector3[children.Length];

        for (int i = 0; i < children.Length; i++)
        {
            // Игнорируем сам RectTransform канваса
            if (children[i] == menuUI.GetComponent<RectTransform>()) continue;

            startPositions[i] = children[i].position;
            // Перемещаем вниз, а не вправо
            targetPositions[i] = children[i].position + Vector3.down * moveDistance; // Изменено здесь
        }

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            // Применяем SmoothStep для плавности начала и конца движения
            t = t * t * (3 - 2 * t);

            for (int i = 0; i < children.Length; i++)
            {
                // Снова игнорируем RectTransform канваса
                if (children[i] == menuUI.GetComponent<RectTransform>()) continue;

                children[i].position = Vector3.Lerp(startPositions[i], targetPositions[i], t);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Устанавливаем конечные позиции для устранения любых неточностей из-за округления
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i] == menuUI.GetComponent<RectTransform>()) continue;

            children[i].position = targetPositions[i];
        }
    }
    public void ShowMenu()
    {
        // Аналогично, расстояние, на которое Canvas должен быть перемещён обратно вверх
        moveDistance = (float)Screen.height; 
        StartCoroutine(ShowMenuCoroutine());
    }

    private IEnumerator ShowMenuCoroutine()
    {
        float elapsedTime = 0;

        // Получаем начальные позиции всех дочерних элементов
        RectTransform[] children = menuUI.GetComponentsInChildren<RectTransform>();

        Vector3[] startPositions = new Vector3[children.Length];
        Vector3[] targetPositions = new Vector3[children.Length];

        for (int i = 0; i < children.Length; i++)
        {
            // Игнорируем сам RectTransform канваса
            if (children[i] == menuUI.GetComponent<RectTransform>()) continue;

            // Здесь начальные позиции уже смещены вниз, так что целевые позиции должны быть обратно вверх
            startPositions[i] = children[i].position;
            targetPositions[i] = children[i].position - Vector3.down * moveDistance; // Изменено направление на вверх
        }

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            // Применяем SmoothStep для плавности начала и конца движения
            t = t * t * (3 - 2 * t);

            for (int i = 0; i < children.Length; i++)
            {
                // Снова игнорируем RectTransform канваса
                if (children[i] == menuUI.GetComponent<RectTransform>()) continue;

                children[i].position = Vector3.Lerp(startPositions[i], targetPositions[i], t);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Устанавливаем конечные позиции для устранения любых неточностей из-за округления
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i] == menuUI.GetComponent<RectTransform>()) continue;

            children[i].position = targetPositions[i];
        }
    }

    public CameraController cameraController;
    
    public Light lightExperimental;
    public Light lightWhiteboard;
    
    [SerializeField] private UnityEngine.UI.Button buttonExperimental;
    [SerializeField] private Canvas labUI;
    [SerializeField] private Canvas menuUI;
    [SerializeField] private Canvas flaskUI;
    [SerializeField] private Canvas submenuUI;
    [SerializeField] private Canvas balloonUI;
    public static string Hovered;
    // Start is called before the first frame update
    void Start()
    {
        buttonExperimental.onClick.AddListener(ActivateExperimentalLab);
    }
    
    public void DeactivateExperimentalLab()
    {
        ShowMenu();
        cameraController.MoveCameraToMenu();
        lightExperimental.enabled = false;
        lightExperimental.range = 2000;
        lightWhiteboard.enabled = false;
        labUI.enabled = false;
        submenuUI.enabled = false;
        flaskUI.enabled = false;
        balloonUI.enabled = false;
    }
    
    public void ActivateExperimentalLab()
    {
        HideMenu();
        cameraController.MoveCameraToLab();
        lightExperimental.enabled = true;
        lightWhiteboard.enabled = true;
        lightExperimental.range = 2000;
        labUI.enabled = true;
        submenuUI.enabled = true;
    }

}
