using UnityEngine;
using TMPro;
using System;
using Button = UnityEngine.UI.Button;
using Toggle = UnityEngine.UI.Toggle;


public class FlaskUIBehaviour : MonoBehaviour
{
    [SerializeField] private TMP_Text volume;
    [SerializeField] private Toggle isFixed;
    [SerializeField] private Button flaskMenuExit;

    [SerializeField] private Canvas flaskMenu;

    void Start()
    {
        isFixed.onValueChanged.AddListener(OnToggleValueChanged);
        flaskMenuExit.onClick.AddListener(CloseFlaskMenu);
    }

    private void OnToggleValueChanged(bool isOn)
    {
        SceneBehaviour.VolumeFixed = isOn;
    }

    public void CloseFlaskMenu()
    {
        flaskMenu.enabled = false;
    }

    private void Update()
    {
        volume.text = Convert.ToString(String.Format("{0:f3} ë", SceneBehaviour.Volume * 1000));
    }
}