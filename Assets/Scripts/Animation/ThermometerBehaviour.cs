using UnityEngine;

public class ThermometerController : MonoBehaviour
{
    public Animator animatorComponent; // ��������� ���������
    public string animationName; // ��� ��������
    void Start()
    {
        // �������� ��������� ���������, ���� �� �� ��������
        if (animatorComponent == null)
            animatorComponent = GetComponent<Animator>();
    }
    void Update()
    {
        SetAnimationToTemperature();
    }
    void SetAnimationToTemperature()
    {
        AnimationClip[] clips = animatorComponent.runtimeAnimatorController.animationClips;
        double animationLengthInSeconds = clips[0].length;
        int totalFrames = (int)(animationLengthInSeconds * 24);
        double temperature = SceneBehaviour.Temperature;
        if (temperature < 0)
            temperature = 0;
        else if (temperature > 1200)
            temperature = 1200;
        int currentFrame = (int)((float)(temperature + 200) / 1200 * totalFrames);
        animatorComponent.Play(animationName, 0, (float)currentFrame / totalFrames);
    }
}