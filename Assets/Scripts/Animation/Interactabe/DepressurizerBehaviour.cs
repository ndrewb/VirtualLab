using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DepressurizerBehaviour : MonoBehaviour
{
    public Material outline;
    public Material outline_off;
    public Shader _outlineShader;
    public AudioSource gasLeak;
    [SerializeField] private GameObject Gas;
    public ParticleBehaviour particleBehaviour;

    private void OnMouseDown()
    {
        gasLeak.Play(0);
    }

    private void OnMouseOver()
    {
        MeshRenderer myRenderer = GetComponent<MeshRenderer>();
        myRenderer.materials[1].shader = _outlineShader;
    }

    private void OnMouseExit()
    {
        MeshRenderer myRenderer = GetComponent<MeshRenderer>();
        myRenderer.materials[1].shader = null;
    }

    private void Start()
    {
        MeshRenderer myRenderer = GetComponent<MeshRenderer>();
        myRenderer.materials[1].shader = null;
    }

    private void OnMouseDrag()
    {
        gasLeak.volume -= 0.005f;
        Gas.SetActive(true);
        particleBehaviour.DecreaseIntensity();
        if (SceneBehaviour.Mass > 0)
            SceneBehaviour.Mass -= SceneBehaviour.Mass / 200f;
        //SceneBehaviour.Mass = Mathf.Clamp((float)(SceneBehaviour.Mass - SceneBehaviour.Mass / 130.7004f/2000f - 0.0512f/2000f),0.0001f,6f);
        if (transform.localPosition.z < -0.198f)
            transform.localPosition += new Vector3(0f, 0f, Time.deltaTime);
    }

    private void OnMouseUp()
    {
        gasLeak.volume = 0.5f;
        gasLeak.Stop();
        particleBehaviour.RestoreIntensity();
        Gas.SetActive(false);
        while (transform.localPosition.z > -0.298f)
            transform.localPosition -= new Vector3(0f, 0f, Time.deltaTime / 60);
    }
}