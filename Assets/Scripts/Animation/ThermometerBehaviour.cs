using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThermometerController : MonoBehaviour
{
    public Animator animatorComponent; // Компонент аниматора
    public string animationName; // Имя анимации
    void Start()
    {
        // Получаем компонент аниматора, если он не назначен
        if (animatorComponent == null)
            animatorComponent = GetComponent<Animator>();
    }
    
    private void OnMouseDown()
    {
        Debug.Log("hello, thermo!");
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

        // Вычисляем текущий кадр
        int currentFrame = (int)((float)(temperature+200)/1200 * totalFrames);

        // Устанавливаем текущий кадр в аниматоре
        animatorComponent.Play(animationName, 0, (float)currentFrame / totalFrames);
    }


}
