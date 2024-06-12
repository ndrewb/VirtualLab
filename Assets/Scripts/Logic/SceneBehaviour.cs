using System;
using UnityEngine;

public class SceneBehaviour : MonoBehaviour
{
    public BalloonBehaviour balloon;
    public HandleBehaviour handle;
    public static bool ForcedVolumeHold = false;
    public static bool TorchActive = false;
    public static bool ElectricTorchActive = false;
    public static bool VolumeFixed = false;
    public static double initialPressure = 101325;
    public static double initialTemperature = 79.915341592495;
    public static double initialVolume = 0.001;
    public static double initialMass = 0.001;
    public static double initialMassMolecular = 0.02897;
    public static double Pressure = 101325;
    public static double Volume = 0.001;
    public static double Temperature = 79.915341592495;
    public static double GasConstant = 8.314;
    public static float customTempIncreaseValue = 0f;
    public static string GasUsed = "air";
    public static double Mass = 0.001;
    public static double MassMolecular = 0.02897;


    private float initialIsochoricEq = (float)initialPressure / ((float)initialTemperature + 273.16f);
    private double _tempIncreaseValue = 0.25;

    public bool AreEqual(float a, float b)
    {
        float epsilon = 1e-5f;
        return Math.Abs(a - b) < epsilon;
    }
    // Update is called once per frame


    void Update()
    {
        if (!AreEqual(initialIsochoricEq, (float)Pressure / (float)(Temperature + 273.16))
            && !VolumeFixed
            && !ForcedVolumeHold)
        {
            Volume = (Temperature + 273.16) * (Mass / MassMolecular) * GasConstant / initialPressure;
            Volume = Mathf.Clamp((float)Volume, 0f, 0.006f);
            handle.ForceVolume((float)Volume);

            //Баланс несоблюдён, попытка восстановить условия
        }


        if (TorchActive)
        {
            Temperature += _tempIncreaseValue;
        }

        if (ElectricTorchActive)
        {
            Temperature += customTempIncreaseValue;
        }

        Temperature = Mathf.Clamp((float)Temperature, -273f, 9999f);
        Pressure = (Temperature + 273.16) * (Mass / MassMolecular) * GasConstant / Volume;
    }

    private void Start()
    {
        balloon.ChangeColorPreset("air");
    }
}