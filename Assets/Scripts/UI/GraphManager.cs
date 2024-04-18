using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GraphManager : MonoBehaviour
{
    public GraphRenderer pvGraphRenderer;
    public GraphRenderer ptGraphRenderer;
    public GraphRenderer vtGraphRenderer;
    public Button recordingButton;

    private List<double> PressureList = new List<double>();
    private List<double> VolumeList = new List<double>();
    private List<double> TemperatureList = new List<double>();
    private bool isRecording = false;
    private float recordingStartTime;

    private void Start()
    {
        recordingButton.onClick.AddListener(ToggleRecording);
    }

    public void ToggleRecording()
    {
        if (isRecording)
        {
            // ѕровер€ем, прошло ли уже 7 секунд с момента начала записи
            if (Time.time - recordingStartTime < 7f)
            {
                Debug.Log("«апись нельз€ остановить раньше, чем через 7 секунд после начала.");
                return;
            }

            isRecording = false;
            // ќтображаем статистику на графиках
            pvGraphRenderer.DrawGraph(VolumeList, PressureList);
            ptGraphRenderer.DrawGraph(TemperatureList, PressureList);
            vtGraphRenderer.DrawGraph(TemperatureList, VolumeList);
        }
        else
        {
            isRecording = true;
            recordingStartTime = Time.time;
            // ќчищаем списки перед началом записи
            PressureList.Clear();
            VolumeList.Clear();
            TemperatureList.Clear();

            StartCoroutine(RecordData());
        }
    }
    private IEnumerator RecordData()
    {
        while (isRecording)
        {
            // ƒобавление текущих значений в списки
            PressureList.Add(SceneBehaviour.Pressure);
            VolumeList.Add(SceneBehaviour.Volume * 1000);
            TemperatureList.Add(SceneBehaviour.Temperature);

            // ќжидание 0.5 секунды перед следующей записью
            yield return new WaitForSeconds(0.1f);
        }
    }
}
