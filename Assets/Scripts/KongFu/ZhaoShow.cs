using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZhaoShow : EventTrigger
{
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        //Debug.Log("离开" + this.gameObject.name);
        GameObject.Find("ZhaoIntro").GetComponent<TextMesh>().text = " ";

    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        //Debug.Log("进入" + this.gameObject.name);
        float num = float.Parse(gameObject.name);
        int num1 = (int)num;
        string info="";
        foreach(var item in ZhaoList.zh[num1].FixData.Effects)
        {
            info = info + item.Name+": "+ item.Detail+'\n';
        }
        info = info + ZhaoList.zh[num1].FixData.DetailInfo;

        GameObject.Find("ZhaoIntro").GetComponent<TextMesh>().text = info;

    }
}
