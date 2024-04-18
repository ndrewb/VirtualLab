using UnityEngine;
using UnityEngine.EventSystems;

public class ManometerController : MonoBehaviour
{
    public Animator animatorComponent; // Компонент аниматора
    public string animationName; // Имя анимации
    void Start()
    {
        // Получаем компонент аниматора, если он не назначен
        if (animatorComponent == null)
            animatorComponent = GetComponent<Animator>();
    }

    void Update()
    {
        SetAnimationToPressure();
    }

    void SetAnimationToPressure()
    {
        AnimationClip[] clips = animatorComponent.runtimeAnimatorController.animationClips;
        double animationLengthInSeconds = clips[0].length;
        int totalFrames = (int)(animationLengthInSeconds * 24);
        double pressure = SceneBehaviour.Pressure/1000;
        if (pressure < 0)
            pressure = 0;
        else if (pressure > 1600)
            pressure = 1600;

        // Вычисляем текущий кадр
        int currentFrame = (int)((float)(pressure)/1600 * totalFrames);

        // Устанавливаем текущий кадр в аниматоре
        animatorComponent.Play(animationName, 0, (float)currentFrame / totalFrames);
    }


}
