using System;
using TMPro;
using UnityEngine;

public class ThermometerElectricBehaviour : MonoBehaviour
{
    [SerializeField] private TMP_Text temperatureForcedOutputC;
    [SerializeField] private TMP_Text temperatureForcedOutputK;

    void Update()
    {
        string outputC = Convert.ToString(String.Format("{0:f1}°C", SceneBehaviour.Temperature));
        string outputK = Convert.ToString(String.Format("{0:f1} K", SceneBehaviour.Temperature + 273.16));

        temperatureForcedOutputC.text = outputC;
        temperatureForcedOutputK.text = outputK;
    }
}