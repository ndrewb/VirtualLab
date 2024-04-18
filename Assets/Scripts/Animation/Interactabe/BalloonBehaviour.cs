using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;


public class BalloonBehaviour : MonoBehaviour
{
    public ParticleBehaviour _particleBehaviour;

    readonly List<(string, float, string)> _gasList = new()
    {
        ("air",0.02897f,"#dce0e8"),
        ("helium", 0.0040026f,"#ea76cb"),
        ("argon", 0.039944f,"#d20f39"),
        ("hydrogen",0.0020159f,"#1e66f5"),
        ("nitrogen",0.0280134f,"#df8e1d"),
        ("oxygen",0.0319968f,"#04a5e5"),
        ("chlorine",0.070904f,"#40a02b"),
    };

    [SerializeField] private Canvas balloonMenu;
    [SerializeField] private GameObject objectGas;
    [SerializeField] private GameObject objectBalloonSticker;
    
    public Material gasMaterial;
    public Material stickerMaterial;
    
    public void ChangeColorPreset(string gas)
    {
        var gasNew = _gasList.FirstOrDefault(i => i.Item1 == gas);
        
        ColorUtility.TryParseHtmlString(gasNew.Item3, out var newColor);
        SceneBehaviour.MassMolecular = gasNew.Item2;
        _particleBehaviour.ChangeColor(newColor);
        newColor.a = 0.5f;
        Material newStickerMaterial = stickerMaterial;
        Material newGasMaterial = gasMaterial;
        newGasMaterial.color = newColor;
        newStickerMaterial.color = newColor;
        
    }

    private void OnMouseDown()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
         balloonMenu.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeColorPreset("air");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
