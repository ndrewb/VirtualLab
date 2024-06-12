using UnityEngine;

public class ParticleBehaviour : MonoBehaviour
{
    public ParticleSystem _system;

    // Start is called before the first frame update

    public void ChangeColor(Color color)
    {
        ParticleSystem.MainModule main = GetComponent<ParticleSystem>().main;
        color.a = 55.0f / 255.0f;
        main.startColor = color;
    }

    public void DecreaseIntensity()
    {
        ParticleSystem.MainModule main = GetComponent<ParticleSystem>().main;
        main.startLifetime = Mathf.Clamp(main.startLifetime.constant - 0.05f, 1f, 5f);
    }

    public void RestoreIntensity()
    {
        ParticleSystem.MainModule main = GetComponent<ParticleSystem>().main;
        main.startLifetime = 5;
    }
}