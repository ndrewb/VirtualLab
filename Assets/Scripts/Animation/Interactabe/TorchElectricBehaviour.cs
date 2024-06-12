using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class TorchElectricBehaviour : MonoBehaviour
{
    private float temperature;
    public AudioSource torchSound;
    [SerializeField] public Color restoredColor;
    public Slider _slider;

    public void ExternalShutdown()
    {
        torchSound.Stop();
        SceneBehaviour.ElectricTorchActive = false;
        SceneBehaviour.customTempIncreaseValue = 0f;
        _slider.value = 0f;
    }


    public void ChangeColor(float temp)
    {
        if (temp > 5)
        {
            Color newColor = restoredColor;
            newColor.b = restoredColor.b - temp / 100;
            newColor.g = restoredColor.g - temp / 100;
            gameObject.GetComponent<Renderer>().material.color = newColor;
            //myRenderer.material.SetColor("newColor",newColor);
        }
        else if (temp < 5)
        {
            Color newColor = restoredColor;
            newColor.r = restoredColor.r + temp / 100;
            newColor.g = restoredColor.g + temp / 100;
            gameObject.GetComponent<Renderer>().material.color = newColor;
            //myRenderer.material.SetColor("newColor",newColor);
        }
        else
        {
            Renderer myRenderer = GetComponent<Renderer>();
            myRenderer.material.SetColor("defaultColor", restoredColor);
        }
    }


    public void Start()
    {
        var meshRenderer = GetComponent<Renderer>();
        restoredColor = meshRenderer.material.color;
    }

    public void KeepPlaying()
    {
        if (!torchSound.isPlaying)
        {
            torchSound.Play();
        }
    }

    public void Temperature(float temp)
    {
        temperature = temp;
        if (Math.Abs(temp) > 5)
        {
            SceneBehaviour.customTempIncreaseValue = temp / 100f;
        }
        else
        {
            SceneBehaviour.customTempIncreaseValue = 0f;
        }

        torchSound.volume = math.abs(temp / 2000f);
        ChangeColor(temp);
    }

    private void SoundPlayer()
    {
        if (Math.Abs(temperature) > 5)
        {
            KeepPlaying();
        }
        else
        {
            torchSound.Stop();
        }
    }

    public void Update()
    {
        SoundPlayer();
    }
}