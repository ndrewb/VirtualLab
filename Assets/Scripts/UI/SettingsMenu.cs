using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _displayResolution;
    [SerializeField] private TMP_Dropdown _displayMode;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private TMP_Text _soundLevel;
    [SerializeField] private Canvas settingsCanvas;
    [SerializeField] private Canvas menuCanvas;

    List<ScreenMode> _screenModes = new()
    {
        new ScreenMode("Full Screen", FullScreenMode.FullScreenWindow),
        new ScreenMode("Windowed", FullScreenMode.Windowed),
        new ScreenMode("Maximized Window", FullScreenMode.MaximizedWindow)
    };

    private void Start()
    {
        DisplayResolution();
        DisplayMode();
    }

    public void DisplayResolution()
    {
        List<Resolution> resolutions = Screen.resolutions.Reverse().ToList();

        foreach (Resolution resolution in resolutions)
        {
            TMP_Dropdown.OptionData option = new();
            option.text = resolution.ToString();

            _displayResolution.options.Add(option);
        }

        _displayResolution.value = resolutions.Count;
        _displayResolution.value = resolutions.FindIndex(x => x.ToString() == Screen.currentResolution.ToString());
    }

    public void DisplayMode()
    {
        foreach (ScreenMode screenMode in _screenModes)
        {
            TMP_Dropdown.OptionData option = new();
            option.text = screenMode.Name;

            _displayMode.options.Add(option);
        }

        _displayMode.value = _screenModes.Count;
        _displayMode.value = Screen.fullScreen ? 0 : 1;
    }

    static float ScaleVolume(float volume)
    {
        // ������������ �������� �� ��������� -40..0 � �������� 0..1
        // ������� ��� ���������������:
        // scaledValue = (value - minValue) / (maxValue - minValue)
        float minVolume = -40;
        float maxVolume = 0;

        // �������� �� ����� �� ������� ���������
        if (volume < minVolume)
            volume = minVolume;
        else if (volume > maxVolume)
            volume = maxVolume;

        // ���������������
        float scaledVolume = (volume - minVolume) / (maxVolume - minVolume);

        return scaledVolume;
    }

    public void Sound(float volume)
    {
        _audioMixer.SetFloat("Volume", volume);
        _soundLevel.text = $"{Mathf.Round(ScaleVolume(volume) * 100)}%";
    }

    public void Quality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SaveDisplayResolution()
    {
        List<Resolution> resolutions = Screen.resolutions.ToList();

        Resolution resolution =
            resolutions.Find(x => x.ToString() == _displayResolution.options[_displayResolution.value].text);
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
    }

    public void SaveDisplayMode()
    {
        FullScreenMode screenMode = _screenModes.Find(x => x.Name == _displayMode.options[_displayMode.value].text)
            .FullScreenMode;
        Screen.SetResolution(Screen.width, Screen.height, screenMode);
    }

    public void GoBackToMenu()
    {
        settingsCanvas.enabled = false;
        menuCanvas.enabled = true;
    }
    
}