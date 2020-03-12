using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GUIMouseHandle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isEnterBuffIcon = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(gameObject.tag == "Buff")
        {
            isEnterBuffIcon = true;
        }
        else
        {
            FightGUI.SetDetailPanel(gameObject.name);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(gameObject.tag == "Buff")
        {
            isEnterBuffIcon = false;
        }
        else
        {
            FightGUI.HideDetailPanel();
        }
    }

    private void OnGUI()
    {
        if (isEnterBuffIcon)
        {
            AttackBuff buff = FightGUI.lookingPerson.AttackBuffs[int.Parse(gameObject.name)];
            string text = buff.StyleEffect.Name + "（" + buff.Duration + "）";
            GUI.contentColor = Color.white;
            GUI.Box(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 100, 25), text);
        }
    }
}
