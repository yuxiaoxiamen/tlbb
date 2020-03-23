using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GUIMouseHandle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public static bool isMouseOver;

    private void Start()
    {
        isMouseOver = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.tag != "GameController")
        {
            FightGUI.SetDetailPanel(gameObject.name);
        }
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (gameObject.tag != "GameController")
        {
            FightGUI.HideDetailPanel();
        } 
        isMouseOver = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isMouseOver = false;
        FightGUI.HideDetailPanel();
    }
}
