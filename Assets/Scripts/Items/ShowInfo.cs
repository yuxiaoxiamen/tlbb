using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ShowInfo : EventTrigger
{

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        //Debug.Log("离开" + this.gameObject.name);
        GameObject.Find("ItemIntro").GetComponent<TextMesh>().text = " ";
        GameObject.Find("ItemEffect").GetComponent<TextMesh>().text = " ";
        GameObject.Find("ItemName").GetComponent<TextMesh>().text = " ";

    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        //Debug.Log("进入" + this.gameObject.name);
        float num = float.Parse(gameObject.name);
        int num1 = (int)num;
        GameObject.Find("ItemName").GetComponent<TextMesh>().text = ButtonList.item[num1].Name;
        GameObject.Find("ItemIntro").GetComponent<TextMesh>().text = ButtonList.item[num1].Information;
        GameObject.Find("ItemEffect").GetComponent<TextMesh>().text = ButtonList.item[num1].Effect;
    }

}
