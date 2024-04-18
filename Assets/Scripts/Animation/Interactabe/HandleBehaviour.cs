using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Collider))]
public class HandleBehaviour : MonoBehaviour
{
    public AudioSource snapbackSound;
    private void OnMouseDrag()
    {
        SceneBehaviour.ForcedVolumeHold = true;

        // ������� ��� �� ������ � ����������� ��������� ����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // �������� ����� ����������� ���� � ���������� �������
            Vector3 objPosition = hit.point;

            // ��������� ����������� � ���������� X, ��� � ����� �������� �������. Y � Z ������������� ����.
            objPosition.y = 227.2264f;
            objPosition.z = 0; // ���������� ������ ������������� �������� Z, ���� ������ ������ ������������ � ���������, �������� �� 0
            objPosition.x = Mathf.Clamp(objPosition.x, -48f, 48f);

            transform.position = objPosition;
            
            SceneBehaviour.Volume = ((transform.position.x + 48) / 16.0) / 1000;
        }
    }
    
    public float targetVolume = 0.5f; // ��������� ��������, ����� ��������
    private Vector3 targetPosition;

    void Start()
    {
        // �������������� ��������� �������
        UpdateTargetPosition(targetVolume);
    }

    void Update()
    {
        if(!SceneBehaviour.ForcedVolumeHold && !SceneBehaviour.VolumeFixed)
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5); // 5 - �������� �����������, ����� ���������
    }

    public void ForceVolume(float volume)
    {
        UpdateTargetPosition(volume);
    }

    private void UpdateTargetPosition(float volume)
    {
        targetPosition = new Vector3(16000 * volume - 48, 227.2264f, 0);
    }
    private void OnMouseUp()
    {
        SceneBehaviour.ForcedVolumeHold = false;
        
        if(!SceneBehaviour.VolumeFixed)
            snapbackSound.Play(0);
    }
    
}
