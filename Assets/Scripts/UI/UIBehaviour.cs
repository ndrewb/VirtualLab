using UnityEngine;
using Button = UnityEngine.UI.Button;

public class UIBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _thermometerClassic;
    [SerializeField] private GameObject _thermometerElectric;
    [SerializeField] private GameObject _manometerClassic;
    [SerializeField] private GameObject _manometerElectric;
    [SerializeField] private GameObject _torchClassic;
    [SerializeField] private GameObject _torchElectric;
    [SerializeField] private GameObject _depressurizerClassic;
    [SerializeField] private GameObject _balloonClassic;

    public TorchBehaviour torch_classic;
    public TorchElectricBehaviour torch_electric;
    
    //Кнопки панели инструментов
    [SerializeField] private Button thermometerButton;
    [SerializeField] private Button thermometerElectricButton;
    [SerializeField] private Button manometerButton;
    [SerializeField] private Button manometerElectricButton;
    [SerializeField] private Button torchButton;
    [SerializeField] private Button torchElectricButton;
    [SerializeField] private Button balloonButton;
    public void Start()
    {
        balloonButton.onClick.AddListener(ChangeBalloon);
        thermometerButton.onClick.AddListener(ChangeThermometerToClassic);
        thermometerElectricButton.onClick.AddListener(ChangeThermometerToElectric);
        
        manometerElectricButton.onClick.AddListener(ChangeManometerToElectric);
        manometerButton.onClick.AddListener(ChangeManometerToClassic);
        
        torchButton.onClick.AddListener(ChangeTorchToClassic);
        torchElectricButton.onClick.AddListener(ChangeTorchToElectric);
    }

    public void ResetLab()
    {
        _balloonClassic.SetActive(false);
        _depressurizerClassic.SetActive(false);
        _thermometerElectric.SetActive(false);
        _thermometerClassic.SetActive(false);
        _manometerElectric.SetActive(false);
        _manometerClassic.SetActive(false);
        _torchClassic.SetActive(false);
        _torchElectric.SetActive(false);
    }
    private void ChangeBalloon()
    {
        _balloonClassic.SetActive(true);
        _depressurizerClassic.SetActive(true);
    }
    private void ChangeThermometerToClassic()
    {
        _thermometerElectric.SetActive(false);
        _thermometerClassic.SetActive(true);
    }
    
    private void ChangeThermometerToElectric()
    {
        _thermometerElectric.SetActive(true);
        _thermometerClassic.SetActive(false);
    }

    private void ChangeManometerToClassic()
    {
        _manometerClassic.SetActive(true);
        _manometerElectric.SetActive(false);
    }

    private void ChangeManometerToElectric()
    {
        _manometerClassic.SetActive(false);
        _manometerElectric.SetActive(true);
    }
    
    private void ChangeTorchToClassic()
    {
        SceneBehaviour.ElectricTorchActive = false;
        torch_electric.ExternalShutdown();
        _torchClassic.SetActive(true);
        _torchElectric.SetActive(false);
    }
    
    private void ChangeTorchToElectric()
    {
        SceneBehaviour.ElectricTorchActive = true;
        torch_classic.ExternalShutdown();
        _torchClassic.SetActive(false);
        _torchElectric.SetActive(true);
    }
   
}
