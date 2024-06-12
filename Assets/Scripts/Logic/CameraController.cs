using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 cameraMenuPos = new Vector3(0, 220, -2200);
    private Vector3 cameraLabPos = new Vector3(-30, 220, -130);
    public float transitionTime = 1f; // ����� �������� � �������
    public float rotationTime = 0.5f;

    public void MoveCameraToMenu()
    {
        // ��������� �������� ����������� ������
        StartCoroutine(MoveCameraToSetupCoroutine(cameraMenuPos, transitionTime));
        RotateCameraBackToZero();
    }

    public void MoveCameraToLab()
    {
        // ��������� �������� ����������� ������
        StartCoroutine(MoveCameraToSetupCoroutine(cameraLabPos, transitionTime));
    }

    public void RotateCameraBy90Degrees()
    {
        // ������������ ������ �� 90 �������� �� ��� Y
        Quaternion targetRotation = Quaternion.Euler(0, 90, 0);
        StartCoroutine(RotateCameraCoroutine(targetRotation, rotationTime));
    }

    public void RotateCameraBackToZero()
    {
        // ������������ ������ ������� �� 0 �������� �� ��� Y
        Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
        StartCoroutine(RotateCameraCoroutine(targetRotation, rotationTime));
    }

    IEnumerator MoveCameraToSetupCoroutine(Vector3 targetPosition, float duration)
    {
        Vector3 startingPosition = transform.position; // ������� ������� ������
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float progress = elapsedTime / duration;
            float smoothedProgress = SmoothStep(progress);

            transform.position = Vector3.Lerp(startingPosition, targetPosition, smoothedProgress);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }

    IEnumerator RotateCameraCoroutine(Quaternion targetRotation, float duration)
    {
        Quaternion startingRotation = transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float progress = elapsedTime / duration;
            float smoothedProgress = SmoothStep(progress);

            transform.rotation = Quaternion.Slerp(startingRotation, targetRotation, smoothedProgress);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
    }

    float SmoothStep(float t)
    {
        return t * t * (3f - 2f * t);
    }
}