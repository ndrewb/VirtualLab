using System;
using UnityEngine;
using UnityEngine.UI;

public class SubMenuBehaviour : MonoBehaviour
{
    public CameraController cameraController;
    public MenuBehaviour MenuBehaviour;
    public UIBehaviour UIBehaviour;
    public TorchBehaviour TorchBehaviour;
    public TorchElectricBehaviour TorchElectricBehaviour;
    private bool changingState = false;
    private string foldingState = "unfolded";
    [SerializeField] private UnityEngine.UI.Button buttonCollapse;
    [SerializeField] private Button backToMenuButton;
    [SerializeField] private UnityEngine.UI.Image panel;
    [SerializeField] private GameObject Fire;

    [SerializeField] private Button Recorder;
    [SerializeField] public Image RecorderImage;
    [SerializeField] public Sprite StartRecordingSprite;
    [SerializeField] public Sprite StopRecordingSprite;

    [SerializeField] private Button WorkspaceChange;
    [SerializeField] public Image WorkspaceImage;
    [SerializeField] public Sprite LabSprite;
    [SerializeField] public Sprite WhiteboardSprite;

    private bool isDeskMode = true;
    private bool isButtonClickable = true;
    private const float buttonCooldown = 0.5f;

    private bool isButtonRecordingClickable = true;
    private bool isRecording = false;

    private void ChangeRecordingState()
    {
        if (!isButtonRecordingClickable)
            return;

        isButtonRecordingClickable = false;
        Invoke(nameof(EnableRecordButton), 7);

        if (isRecording)
        {
            isRecording = false;
            isButtonRecordingClickable = true;
            RecorderImage.sprite = StartRecordingSprite;
            //stoprecording
        }
        else
        {
            RecorderImage.sprite = StopRecordingSprite;
            Recorder.interactable = false;
            isRecording = true;
            isButtonRecordingClickable = false;
            //startrecording
        }

        Recorder.OnDeselect(null);
    }

    private void EnableRecordButton()
    {
        Recorder.interactable = true;
        isButtonRecordingClickable = true;
    }

    private void ChangeWorkspace()
    {
        if (!isButtonClickable)
            return;

        isButtonClickable = false;
        Invoke(nameof(EnableButton), buttonCooldown);

        if (isDeskMode)
        {
            ToWhiteboard();
        }
        else
        {
            ToDesk();
        }

        WorkspaceChange.OnDeselect(null);
    }

    private void ToDesk()
    {
        WorkspaceImage.sprite = WhiteboardSprite;
        cameraController.RotateCameraBackToZero();
        isDeskMode = true;
    }

    private void ToWhiteboard()
    {
        WorkspaceImage.sprite = LabSprite;
        cameraController.RotateCameraBy90Degrees();
        isDeskMode = false;
    }


    private void EnableButton()
    {
        isButtonClickable = true;
    }

    void Start()
    {
        Recorder.onClick.AddListener(ChangeRecordingState);
        WorkspaceChange.onClick.AddListener(ChangeWorkspace);
        backToMenuButton.onClick.AddListener(BackToMenu);
        buttonCollapse.onClick.AddListener(ChangeToolbarState);
    }

    private void BackToMenu()
    {
        Fire.SetActive(false);
        MenuBehaviour.DeactivateExperimentalLab();
        UIBehaviour.ResetLab();
        TorchBehaviour.ExternalShutdown();
        TorchElectricBehaviour.ExternalShutdown();
        SceneBehaviour.Pressure = SceneBehaviour.initialPressure;
        SceneBehaviour.Mass = SceneBehaviour.initialMass;
        SceneBehaviour.MassMolecular = SceneBehaviour.initialMassMolecular;
        SceneBehaviour.Volume = SceneBehaviour.initialVolume;
        SceneBehaviour.Temperature = SceneBehaviour.initialTemperature;
        SceneBehaviour.ForcedVolumeHold = false;
        SceneBehaviour.TorchActive = false;
        SceneBehaviour.VolumeFixed = false;
    }

    private void ChangeToolbarState()
    {
        switch (foldingState)
        {
            case "folded":
                buttonCollapse.interactable = false;
                changingState = true;
                break;
            case "unfolded":
                buttonCollapse.interactable = false;
                changingState = true;
                break;
        }
    }

    public bool AreEqual(float a, float b)
    {
        float epsilon = 1e-5f;
        return Math.Abs(a - b) < epsilon;
    }

    private void Fold()
    {
        var transform1 = panel.transform;
        Vector2 panelPosition = transform1.localPosition;
        if (panelPosition.y > -324)
        {
            transform1.localPosition = new Vector2(panelPosition.x, panelPosition.y - 5f);
        }
        else
        {
            changingState = false;
            buttonCollapse.interactable = true;
            foldingState = "folded";
        }
    }

    private void Unfold()
    {
        var transform1 = panel.transform;
        Vector2 panelPosition = transform1.localPosition;
        if (panelPosition.y < -269)
        {
            transform1.localPosition = new Vector2(panelPosition.x, panelPosition.y + 5f);
        }
        else
        {
            changingState = false;
            buttonCollapse.interactable = true;
            foldingState = "unfolded";
        }
    }

    private void FoldingController()
    {
        if (changingState)
        {
            switch (foldingState)
            {
                case "folded":
                    Unfold();
                    break;
                case "unfolded":
                    Fold();
                    break;
            }
        }
    }

    void Update()
    {
        FoldingController();
    }
}