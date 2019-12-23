using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GUIMouseHandle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        FightGUI.SetDetailPanel(gameObject.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FightGUI.HideDetailPanel();
    }
}
