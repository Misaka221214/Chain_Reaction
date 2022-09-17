using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverFactor = 0.05f;
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<RectTransform>().localScale = new Vector3(GetComponent<RectTransform>().localScale.x + hoverFactor, GetComponent<RectTransform>().localScale.y + hoverFactor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<RectTransform>().localScale = new Vector3(GetComponent<RectTransform>().localScale.x - hoverFactor, GetComponent<RectTransform>().localScale.y - hoverFactor);
    }

}
