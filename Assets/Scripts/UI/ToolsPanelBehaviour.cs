
using UnityEngine;

public class ToolsPanelBehaviour : MonoBehaviour
{
    private float _buttonRotation = 0;
    public bool changingState = false;
    public string foldingState = "unfolded";
    [SerializeField] private UnityEngine.UI.Button buttonCollapse;

    [SerializeField] private UnityEngine.UI.Image panel;
    // Start is called before the first frame update
    void Start()
    {
        buttonCollapse.onClick.AddListener(ChangeToolbarState);
    }

    private void ChangeToolbarState()
    {
        switch (foldingState)
        {
            case "folded":
                buttonCollapse.interactable = false;
                changingState = true;
                break;
            case "unfolded":
                buttonCollapse.interactable = false;
                changingState = true;
                break;   
        }
    }

    private void Fold()
    {
        var transform1 = panel.transform;
        Vector2 panelPosition = transform1.localPosition;
        if (panelPosition.x > -605)
        {
            _buttonRotation = _buttonRotation - 5.8f;
            buttonCollapse.transform.rotation = Quaternion.Euler(0,0,_buttonRotation);
            transform1.localPosition = new Vector2(panelPosition.x - 5, panelPosition.y);
        }
        else
        {
            buttonCollapse.transform.rotation = Quaternion.Euler(0,0,180);
            changingState = false;
            buttonCollapse.interactable = true;
            foldingState="folded";
        }
    }

    private void Unfold()
    {
        var transform1 = panel.transform;
        Vector2 panelPosition = transform1.localPosition;
        if (panelPosition.x < -455)
        {
            _buttonRotation = _buttonRotation - 5.8f;
            buttonCollapse.transform.rotation = Quaternion.Euler(0,0,_buttonRotation);
            transform1.localPosition = new Vector2(panelPosition.x + 5, panelPosition.y);
        }
        else
        {
            buttonCollapse.transform.rotation = Quaternion.Euler(0,0,0);
            changingState = false;
            buttonCollapse.interactable = true;
            foldingState="unfolded";
        }
    }

    private void FoldingController()
    {
        if (changingState)
        {
            switch (foldingState)
            {
                case "folded":
                    Unfold();
                    break;
                case "unfolded":
                    Fold();
                    break;   
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        FoldingController();
    }
}
