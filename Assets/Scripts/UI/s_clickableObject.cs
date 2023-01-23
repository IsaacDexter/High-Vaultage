using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class s_clickableObject : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    virtual protected void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;  //Make it so the player cannot click the transparent part of the image.
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Button button = gameObject.GetComponent<Button>();
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                EventSystem.current.SetSelectedGameObject(null);
                LeftClick();
                break;
            case PointerEventData.InputButton.Right:
                button.Select();
                RightClick();
                break;
            case PointerEventData.InputButton.Middle:
                break;
            default:
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Button button = gameObject.GetComponent<Button>();
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                break;
            case PointerEventData.InputButton.Right:
                EventSystem.current.SetSelectedGameObject(null);
                break;
            case PointerEventData.InputButton.Middle:
                break;
            default:
                break;
        }
    }

    void SetButtonPressedColour(Button button, Color color)
    {
        var colors = button.colors;
        colors.pressedColor = color;
        button.colors = colors;
    }

    virtual protected void LeftClick()
    {
        
    }

    virtual protected void RightClick()
    {
        
    }
}