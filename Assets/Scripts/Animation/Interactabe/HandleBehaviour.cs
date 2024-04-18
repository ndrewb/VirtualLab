using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Collider))]
public class HandleBehaviour : MonoBehaviour
{
    public AudioSource snapbackSound;
    private void OnMouseDrag()
    {
        SceneBehaviour.ForcedVolumeHold = true;

        // —оздаем луч из камеры в направлении указател€ мыши
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // ѕолучаем точку пересечени€ луча с плоскостью объекта
            Vector3 objPosition = hit.point;

            // ѕримен€ем ограничени€ к координате X, как в вашем исходном примере. Y и Z устанавливаем €вно.
            objPosition.y = 227.2264f;
            objPosition.z = 0; // ”становите нужное фиксированное значение Z, если объект должен перемещатьс€ в плоскости, отличной от 0
            objPosition.x = Mathf.Clamp(objPosition.x, -48f, 48f);

            transform.position = objPosition;
            
            SceneBehaviour.Volume = ((transform.position.x + 48) / 16.0) / 1000;
        }
    }
    
    public float targetVolume = 0.5f; // Ќачальное значение, можно изменить
    private Vector3 targetPosition;

    void Start()
    {
        // »нициализируем начальную позицию
        UpdateTargetPosition(targetVolume);
    }

    void Update()
    {
        if(!SceneBehaviour.ForcedVolumeHold && !SceneBehaviour.VolumeFixed)
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5); // 5 - скорость перемещени€, можно настроить
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
