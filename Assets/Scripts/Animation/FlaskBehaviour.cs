using UnityEngine;
using UnityEngine.EventSystems;

public class VolumeController : MonoBehaviour
{
    public Animator animatorComponent; // ��������� ���������
    public string animationName; // ��� ��������
    [SerializeField] private Canvas FlaskUI;

    private double targetVolume; // ������� ����� ��� �������� ��������
    private double currentVolume; // ������� ����� ��� �������� ���������

    void Start()
    {
        // �������� ��������� ���������, ���� �� �� ��������
        if (animatorComponent == null)
            animatorComponent = GetComponent<Animator>();
        targetVolume = currentVolume = 0;
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            FlaskUI.enabled = true;
    }

    void Update()
    {
        // �������� ������� ������� ����� �� ������ SceneBehaviour.Volume � ������������ ��� � ������ ��������
        targetVolume = (double)(SceneBehaviour.Volume) * 1000;

        // ���������, ������� �� ����� ForcedVolumeHold
        if (SceneBehaviour.ForcedVolumeHold)
        {
            // ���� ����� �������, �������� ��-�������, ��� �������� ���������
            currentVolume = targetVolume;
        }
        else
        {
            // ������ ��������� ������� ����� �� �������� ��������
            currentVolume =
                Mathf.Lerp((float)currentVolume, (float)targetVolume,
                    Time.deltaTime * 5); // 5 - �������� ��������� ������
        }

        // ������������� �������� � ������������ � ������� �������
        SetAnimationToVolume(currentVolume);
    }

    void SetAnimationToVolume(double volume)
    {
        AnimationClip[] clips = animatorComponent.runtimeAnimatorController.animationClips;
        double animationLengthInSeconds = clips[0].length;
        // ������������, ��� �������� ����� 600 ������
        int totalFrames = (int)(animationLengthInSeconds * 24);

        // ���������, ����� ����� �� �������� 6 ������
        volume = Mathf.Clamp((float)volume, 0, 6);

        // ��������� ������� ����
        float frameProgress = (float)volume / 6f;

        // ������������� ������� ���� � ���������, ��������� ��������������� �����
        animatorComponent.Play(animationName, 0, frameProgress);
        animatorComponent.speed = 0; // ������������� ��������, ����� ��� �� ������������� �������������
    }
}