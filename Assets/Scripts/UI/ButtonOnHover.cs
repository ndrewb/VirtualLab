using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ButtonOnHover : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button button;

    [FormerlySerializedAs("button_text")] [SerializeField]
    private TMP_Text buttonText;

    public void IsobaricHover()
    {
        MenuBehaviour.Hovered = buttonText.text;
        ColorUtility.TryParseHtmlString("#94e2d5", out var cyan);
        button.image.fillCenter = false;
        buttonText.color = cyan;
    }

    public void IsobaricLeaves()
    {
        ColorUtility.TryParseHtmlString("#313244", out var blekuha);
        button.image.fillCenter = true;
        buttonText.color = blekuha;
    }
}