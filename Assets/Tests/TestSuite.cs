using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.UI;


public class TestSuite : MonoBehaviour
{
    [UnityTest]
    public IEnumerator TestVolumeCalculation()
    {
 
        // Входные данные для теста
        float temperature = 79.92f; // в градусах Цельсия
        float mass = 0.001f; // в кг
        float massMolecular =  0.02897f; // в кг/моль
        float gasConstant = 8.314f; // универсальная газовая постоянная в Дж/(моль·К)
        float pressure = 101325f; // в кПа

        // Ожидаемый результат
        float expectedVolume =  0.001f;

        // Расчет объема с использованием формулы
        float calculatedVolume = CalculateVolume(temperature, mass, massMolecular, gasConstant, pressure);
        yield return new WaitForSeconds(0.1f);
        // Проверка соответствия результатов
        Assert.AreEqual(expectedVolume, calculatedVolume, 0.001f); // допустимая погрешность 0.001
    }
    // Метод для расчета объема с использованием формулы
    private float CalculateVolume(float temperature, float mass, float massMolecular, float gasConstant, float pressure)
    {
        return (temperature + 273.16f) * (mass / massMolecular) * gasConstant / pressure;
    }

    [UnityTest]
    public IEnumerator CameraMovementSmoothnessTest()
    {
        GameObject cameraObject = new GameObject("Camera");
        Camera cameraComponent = cameraObject.AddComponent<Camera>();
        Vector3 initialPosition = new Vector3(0f, 0f, 0f);
        cameraObject.transform.position = initialPosition;
        Vector3 targetPosition = new Vector3(0f, 220f, -2200f);
        float duration = 2f;
        var cameraMovementTest = new GameObject().AddComponent<TestSuite>();
        yield return cameraMovementTest.StartCoroutine(cameraMovementTest.MoveCameraToSetupCoroutine(cameraObject.transform, targetPosition, duration));
        Assert.AreEqual(targetPosition, cameraObject.transform.position, "Camera didn't reach the target position");
        GameObject.Destroy(cameraObject);
    }
    IEnumerator MoveCameraToSetupCoroutine(Transform cameraTransform, Vector3 targetPosition, float duration)
    {
        Vector3 startingPosition = cameraTransform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float progress = elapsedTime / duration;
            float smoothedProgress = SmoothStep(progress);

            cameraTransform.position = Vector3.Lerp(startingPosition, targetPosition, smoothedProgress);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraTransform.position = targetPosition;
    }

    float SmoothStep(float x)
    {
        return x * x * (3f - 2f * x);
    }
    
    [UnityTest]
    public IEnumerator ButtonRotationTest() {
        GameObject panelObject = new GameObject("Panel");
        RectTransform panelTransform = panelObject.AddComponent<RectTransform>();
        panelTransform.localPosition = new Vector3(-455f, 0f, 0f);
        GameObject buttonObject = new GameObject("Button");
        Button buttonCollapse = buttonObject.AddComponent<Button>();
        buttonCollapse.transform.rotation = Quaternion.identity;
        var panelFoldTest = new GameObject().AddComponent<TestSuite>();
        panelFoldTest.panel = panelTransform;
        panelFoldTest.buttonCollapse = buttonCollapse;
        panelFoldTest._buttonRotation = 0f;
        yield return panelFoldTest.StartCoroutine(panelFoldTest.FoldUntilCondition());
        Assert.AreEqual(180f, Mathf.Abs(Mathf.Round(panelFoldTest._buttonRotation)) % 360, "Button rotation is not a multiple of 180");
    }
    RectTransform panel;
    Button buttonCollapse;
    float _buttonRotation;
    private void Fold() {
        var transform1 = panel.transform;
        Vector2 panelPosition = transform1.localPosition;
        if (panelPosition.x > -606) {
            _buttonRotation = _buttonRotation - 5.8f;
            buttonCollapse.transform.rotation = Quaternion.Euler(0, 0, _buttonRotation);
            transform1.localPosition = new Vector2(panelPosition.x - 5, panelPosition.y);
        }
    }
    IEnumerator FoldUntilCondition() {
        while (panel.localPosition.x > -606){
            Fold();
            yield return null;
        }
    }
}