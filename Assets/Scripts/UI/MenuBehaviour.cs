using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBehaviour : MonoBehaviour
{
    [SerializeField] public Canvas menuCanvas;
    [SerializeField] public Canvas settingsCanvas;
    [SerializeField] public Button settingsButton;
    
    public float duration = 1f; // ������������ �������� � ��������
    public float moveDistance = (float)Screen.height*2.0f; // ����������, �� ������� Canvas ������ ���� ��������� ����

    public void ShowSettings()
    {
        settingsCanvas.enabled = true;
        menuCanvas.enabled = false;
    }
    public void HideMenu()
    {
       moveDistance = (float)Screen.height; // ����������, �� ������� Canvas ������ ���� ��������� ����
    StartCoroutine(HideMenuCoroutine());
    }
    private IEnumerator HideMenuCoroutine()
    {
        float elapsedTime = 0;

        // �������� ��������� ������� ���� �������� ���������
        RectTransform[] children = menuUI.GetComponentsInChildren<RectTransform>();

        Vector3[] startPositions = new Vector3[children.Length];
        Vector3[] targetPositions = new Vector3[children.Length];

        for (int i = 0; i < children.Length; i++)
        {
            // ���������� ��� RectTransform �������
            if (children[i] == menuUI.GetComponent<RectTransform>()) continue;

            startPositions[i] = children[i].position;
            // ���������� ����, � �� ������
            targetPositions[i] = children[i].position + Vector3.down * moveDistance; // �������� �����
        }

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            // ��������� SmoothStep ��� ��������� ������ � ����� ��������
            t = t * t * (3 - 2 * t);

            for (int i = 0; i < children.Length; i++)
            {
                // ����� ���������� RectTransform �������
                if (children[i] == menuUI.GetComponent<RectTransform>()) continue;

                children[i].position = Vector3.Lerp(startPositions[i], targetPositions[i], t);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ������������� �������� ������� ��� ���������� ����� ����������� ��-�� ����������
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i] == menuUI.GetComponent<RectTransform>()) continue;

            children[i].position = targetPositions[i];
        }
    }
    public void ShowMenu()
    {
        // ����������, ����������, �� ������� Canvas ������ ���� ��������� ������� �����
        moveDistance = (float)Screen.height; 
        StartCoroutine(ShowMenuCoroutine());
    }

    private IEnumerator ShowMenuCoroutine()
    {
        float elapsedTime = 0;

        // �������� ��������� ������� ���� �������� ���������
        RectTransform[] children = menuUI.GetComponentsInChildren<RectTransform>();

        Vector3[] startPositions = new Vector3[children.Length];
        Vector3[] targetPositions = new Vector3[children.Length];

        for (int i = 0; i < children.Length; i++)
        {
            // ���������� ��� RectTransform �������
            if (children[i] == menuUI.GetComponent<RectTransform>()) continue;

            // ����� ��������� ������� ��� ������� ����, ��� ��� ������� ������� ������ ���� ������� �����
            startPositions[i] = children[i].position;
            targetPositions[i] = children[i].position - Vector3.down * moveDistance; // �������� ����������� �� �����
        }

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            // ��������� SmoothStep ��� ��������� ������ � ����� ��������
            t = t * t * (3 - 2 * t);

            for (int i = 0; i < children.Length; i++)
            {
                // ����� ���������� RectTransform �������
                if (children[i] == menuUI.GetComponent<RectTransform>()) continue;

                children[i].position = Vector3.Lerp(startPositions[i], targetPositions[i], t);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ������������� �������� ������� ��� ���������� ����� ����������� ��-�� ����������
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
