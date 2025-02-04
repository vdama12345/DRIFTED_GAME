using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSpriteHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Image buttonImage; // The button image component
    public Sprite normalSprite; // Default sprite
    public Sprite highlightedSprite; // Highlight sprite when hovered
    public Sprite pressedSprite; // Sprite when button is pressed

    private void Start()
    {
        // Set the initial sprite to the normal state
        if (buttonImage != null && normalSprite != null)
        {
            buttonImage.sprite = normalSprite;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonImage != null && highlightedSprite != null)
        {
            buttonImage.sprite = highlightedSprite;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonImage != null && normalSprite != null)
        {
            buttonImage.sprite = normalSprite;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (buttonImage != null && pressedSprite != null)
        {
            buttonImage.sprite = pressedSprite;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (buttonImage != null && highlightedSprite != null)
        {
            buttonImage.sprite = highlightedSprite;
        }
    }
}
