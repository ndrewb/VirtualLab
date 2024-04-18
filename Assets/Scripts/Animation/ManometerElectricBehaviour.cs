using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManometerElectricBehaviour : MonoBehaviour
{
    [SerializeField] private TMP_Text volumeForcedOutput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string output = Convert.ToString( Math.Round(SceneBehaviour.Pressure / 1000,1)) + " Íœ‡";
        volumeForcedOutput.text = output;
    }
}
