using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BalloonUIBehaviour : MonoBehaviour
{
    public BalloonBehaviour balloonBehaviour;

    private readonly List<(string, string)> _gasLocalizeList = new()
    {
        ("air", "������"),
        ("helium", "�����"),
        ("argon", "�����"),
        ("hydrogen", "�������"),
        ("nitrogen", "����"),
        ("oxygen", "��������"),
        ("chlorine", "����")
    };

    [SerializeField] private TMP_Text mass;
    [SerializeField] private TMP_Text massMolar;
    [SerializeField] private TMP_Dropdown gasTypeDropdown;
    [SerializeField] private Canvas balloonMenu;
    [SerializeField] private Button balloonMenuExit;

    // Start is called before the first frame update
    void Start()
    {
        gasTypeDropdown.onValueChanged.AddListener(ChangeGas);
        balloonMenuExit.onClick.AddListener(CloseBalloonMenu);
    }

    string GasLocalize(string gasTyped)
    {
        var gasNewName = _gasLocalizeList.FirstOrDefault(i => i.Item2 == gasTyped);
        return gasNewName.Item1;
    }

    void ChangeGas(int newNumber)
    {
        string gasType = GasLocalize(gasTypeDropdown.options[gasTypeDropdown.value].text);
        balloonBehaviour.ChangeColorPreset(gasType);
    }

    public void CloseBalloonMenu()
    {
        balloonMenu.enabled = false;
    }
    
    void Update()
    {
        mass.text = Convert.ToString(String.Format("{0:f3} ��", SceneBehaviour.Mass));
        massMolar.text = Convert.ToString(String.Format("{0:f3} ��/����", SceneBehaviour.MassMolecular));
    }
}