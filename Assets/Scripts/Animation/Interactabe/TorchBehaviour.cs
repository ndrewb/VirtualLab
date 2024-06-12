using UnityEngine;

public class TorchBehaviour : MonoBehaviour
{
    public AudioSource torchSound;
    [SerializeField] private GameObject Fire;

    private void OnMouseDown()
    {
        Fire.SetActive(!Fire.activeInHierarchy);
        torchSound.Play();
        SceneBehaviour.TorchActive = SceneBehaviour.TorchActive != true;
        SoundState(SceneBehaviour.TorchActive);
    }

    public void ExternalShutdown()
    {
        Fire.SetActive(false);
        torchSound.Stop();
        SceneBehaviour.TorchActive = false;
    }

    private void SoundState(bool play)
    {
        if (play)
        {
            torchSound.Play();
        }
        else
        {
            torchSound.Stop();
        }
    }
}