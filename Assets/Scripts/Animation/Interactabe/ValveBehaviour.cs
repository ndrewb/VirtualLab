using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ValveBehaviour : MonoBehaviour
{
    public AudioSource squeakSound;
    private float rotationAngle = 6;
    private int rotationSteps = 0;

    private void OnMouseDown()
    {
        squeakSound.Play(0);
    }

    private void OnMouseDrag()
    {
        if (rotationSteps <= 60)
        {
            transform.rotation = Quaternion.Euler(-90f, 0f, rotationSteps * rotationAngle);
            rotationSteps += 1;
        }

        SceneBehaviour.Mass += SceneBehaviour.MassMolecular / 1000f;
    }

    private void OnMouseUp()
    {
        StartCoroutine(ReturnValve());
    }

    IEnumerator ReturnValve()
    {
        float returnSpeed = 2f; // Adjust the return speed as needed
        Quaternion startRotation = transform.rotation;
        float elapsedTime = 0f;

        while (rotationSteps >= 0)
        {
            elapsedTime += Time.deltaTime * returnSpeed;
            transform.rotation = Quaternion.Slerp(startRotation,
                Quaternion.Euler(-90f, 0f, rotationSteps * rotationAngle), elapsedTime);
            rotationSteps -= 1;
            yield return null;
        }
    }
}