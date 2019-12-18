using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GongShow : EventTrigger
{
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        //Debug.Log("离开" + this.gameObject.name);
        GameObject.Find("GongIntro").GetComponent<TextMesh>().text = " ";

    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        //Debug.Log("进入" + this.gameObject.name);
        float num = float.Parse(gameObject.name);
        int num1 = (int)num;

        string info = "默认效果： "+GongList.gong[num1].DefaultEffect+'\n' + "第一重效果： "+GongList.gong[num1].FirstEffect+'\n'+ "第六重效果： " + GongList.gong[num1].SixthEffect + '\n'+ "第十重效果： " + GongList.gong[num1].TenthEffect + '\n';

        GameObject.Find("GongIntro").GetComponent<TextMesh>().text = info;

    }
}
