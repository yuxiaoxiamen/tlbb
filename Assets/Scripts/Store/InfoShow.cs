using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfoShow : EventTrigger
{
    private Button button;

    public string Textnumchange(string s, int num)
    {
        string temp = null;
        if (s.Length < num)
            temp = s;
        else
        {
            int i = 0;
            for (int k = 0; k < s.Length / num + 1; k++)
            {
                for (; i < s.Length && i < num * (k + 1); i++)
                    temp = temp + s[i].ToString();
                if (i < s.Length)
                    temp = temp + "\r\n";
            }
        }
        temp = temp + "\n";

        return temp;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        //Debug.Log("离开" + this.gameObject.name);
        GameObject.Find("GoodIntro").GetComponent<TextMesh>().text = " ";

    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        //Debug.Log("进入" + this.gameObject.name);
        float num = float.Parse(gameObject.name);
        int num1 = (int)num;

        GameObject.Find("GoodIntro").GetComponent<TextMesh>().text = Textnumchange(GoodDisplay.good[num1].Effect, 18);

    }



}