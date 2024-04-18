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
            // ���������, ������ �� ��� 7 ������ � ������� ������ ������
            if (Time.time - recordingStartTime < 7f)
            {
                Debug.Log("������ ������ ���������� ������, ��� ����� 7 ������ ����� ������.");
                return;
            }

            isRecording = false;
            // ���������� ���������� �� ��������
            pvGraphRenderer.DrawGraph(VolumeList, PressureList);
            ptGraphRenderer.DrawGraph(TemperatureList, PressureList);
            vtGraphRenderer.DrawGraph(TemperatureList, VolumeList);
        }
        else
        {
            isRecording = true;
            recordingStartTime = Time.time;
            // ������� ������ ����� ������� ������
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
            // ���������� ������� �������� � ������
            PressureList.Add(SceneBehaviour.Pressure);
            VolumeList.Add(SceneBehaviour.Volume * 1000);
            TemperatureList.Add(SceneBehaviour.Temperature);

            // �������� 0.5 ������� ����� ��������� �������
            yield return new WaitForSeconds(0.1f);
        }
    }
}
