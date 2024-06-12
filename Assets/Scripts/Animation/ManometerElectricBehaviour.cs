using System;
using TMPro;
using UnityEngine;

public class ManometerElectricBehaviour : MonoBehaviour
{
    [SerializeField] private TMP_Text volumeForcedOutput;
    void Update()
    {
        string output = Convert.ToString(Math.Round(SceneBehaviour.Pressure / 1000, 1)) + " Íœ‡";
        volumeForcedOutput.text = output;
    }
}