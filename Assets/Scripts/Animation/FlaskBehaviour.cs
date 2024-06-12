using UnityEngine;
using UnityEngine.EventSystems;

public class VolumeController : MonoBehaviour
{
    public Animator animatorComponent; // Компонент аниматора
    public string animationName; // Имя анимации
    [SerializeField] private Canvas FlaskUI;

    private double targetVolume; // Целевой объем для плавного перехода
    private double currentVolume; // Текущий объем для плавного изменения

    void Start()
    {
        // Получаем компонент аниматора, если он не назначен
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
        // Получаем текущий целевой объём из класса SceneBehaviour.Volume и конвертируем его в нужный диапазон
        targetVolume = (double)(SceneBehaviour.Volume) * 1000;

        // Проверяем, включен ли режим ForcedVolumeHold
        if (SceneBehaviour.ForcedVolumeHold)
        {
            // Если режим включен, работаем по-старому, без плавного изменения
            currentVolume = targetVolume;
        }
        else
        {
            // Плавно обновляем текущий объем до целевого значения
            currentVolume =
                Mathf.Lerp((float)currentVolume, (float)targetVolume,
                    Time.deltaTime * 5); // 5 - скорость изменения объема
        }

        // Устанавливаем анимацию в соответствии с текущим объемом
        SetAnimationToVolume(currentVolume);
    }

    void SetAnimationToVolume(double volume)
    {
        AnimationClip[] clips = animatorComponent.runtimeAnimatorController.animationClips;
        double animationLengthInSeconds = clips[0].length;
        // Предполагаем, что анимация имеет 600 кадров
        int totalFrames = (int)(animationLengthInSeconds * 24);

        // Проверяем, чтобы объем не превышал 6 литров
        volume = Mathf.Clamp((float)volume, 0, 6);

        // Вычисляем текущий кадр
        float frameProgress = (float)volume / 6f;

        // Устанавливаем текущий кадр в аниматоре, используя нормализованное время
        animatorComponent.Play(animationName, 0, frameProgress);
        animatorComponent.speed = 0; // Останавливаем анимацию, чтобы она не проигрывалась автоматически
    }
}