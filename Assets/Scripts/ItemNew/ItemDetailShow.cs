using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDetailShow : EventTrigger
{
    string itemname;
    string info;
    string effect;
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
        GameObject.Find("ItemDetail").GetComponent<TextMesh>().text = " ";

    }
    public override void OnPointerEnter(PointerEventData eventData)
    {

        base.OnPointerEnter(eventData);

        float num = float.Parse(gameObject.name);           //获取物品栏序号
        int num1 = (int)num;
        string itemdetail = null;

        string type = ItemMain.itemtype;                 //获取物品栏类型
        switch (type)
        {
            case "Alcohol":
                if (num1 < ItemMain.alcohol.Count)
                {
                    itemname = ItemMain.alcohol[num1].Name;
                    info = ItemMain.alcohol[num1].Information;
                    effect = ItemMain.alcohol[num1].Effect;
                    itemdetail = itemname + "\n\n" + Textnumchange(info, 32) + Textnumchange(effect, 32);
                }
                break;
            case "Food":
                if (num1 < ItemMain.food.Count)
                {
                    itemname = ItemMain.food[num1].Name;
                    info = ItemMain.food[num1].Information;
                    effect = ItemMain.food[num1].Effect;
                    itemdetail = itemname + "\n\n" + Textnumchange(info, 32) + Textnumchange(effect, 32);
                }
                break;
            case "Knife":
                if (num1 < ItemMain.knife.Count)
                {
                    itemname = ItemMain.knife[num1].Name;
                    info = ItemMain.knife[num1].Information;
                    effect = ItemMain.knife[num1].Effect;
                    itemdetail = itemname + "\n\n" + Textnumchange(info, 32) + Textnumchange(effect, 32);
                }
                break;
            case "Sword":
                if (num1 < ItemMain.sword.Count)
                {
                    itemname = ItemMain.sword[num1].Name;
                    info = ItemMain.sword[num1].Information;
                    effect = ItemMain.sword[num1].Effect;
                    itemdetail = itemname + "\n\n" + Textnumchange(info, 32) + Textnumchange(effect, 32);
                }
                break;
            case "Rod":
                if (num1 < ItemMain.rod.Count)
                {
                    itemname = ItemMain.rod[num1].Name;
                    info = ItemMain.rod[num1].Information;
                    effect = ItemMain.rod[num1].Effect;
                    itemdetail = itemname + "\n\n" + Textnumchange(info, 32) + Textnumchange(effect, 32);
                }
                break;
            case "Pellet":
                if (num1 < ItemMain.pellet.Count)
                {
                    itemname = ItemMain.pellet[num1].Name;
                    info = ItemMain.pellet[num1].Information;
                    effect = ItemMain.pellet[num1].Effect;
                    itemdetail = itemname + "\n\n" + Textnumchange(info, 32) + Textnumchange(effect, 32);
                }
                break;
            default:
                break;
        }
        GameObject.Find("ItemDetail").GetComponent<TextMesh>().text = itemdetail;

    }

    /*private void OnMouseEnter()
    {

        float num = float.Parse(gameObject.name);           //获取物品栏序号
        int num1 = (int)num;
        string itemdetail = null;

        string type = ItemMain.itemtype;                 //获取物品栏类型
        switch (type)
        {
            case "Alcohol":
                if (num1 < ItemMain.alcohol.Count)
                {
                    itemname = ItemMain.alcohol[num1].Name;
                    info = ItemMain.alcohol[num1].Information;
                    effect = ItemMain.alcohol[num1].Effect;
                    itemdetail = name + "\n\n" + Textnumchange(info, 32) + Textnumchange(effect, 32);
                }
                break;
            case "Food":
                if (num1 < ItemMain.food.Count)
                {
                    itemname = ItemMain.food[num1].Name;
                    info = ItemMain.food[num1].Information;
                    effect = ItemMain.food[num1].Effect;
                    itemdetail = name + "\n\n" + Textnumchange(info, 32) + Textnumchange(effect, 32);
                }
                break;
            case "Knife":
                if (num1 < ItemMain.knife.Count)
                {
                    itemname = ItemMain.knife[num1].Name;
                    info = ItemMain.knife[num1].Information;
                    effect = ItemMain.knife[num1].Effect;
                    itemdetail = name + "\n\n" + Textnumchange(info, 32) + Textnumchange(effect, 32);
                }
                break;
            case "Sword":
                if (num1 < ItemMain.sword.Count)
                {
                    itemname = ItemMain.sword[num1].Name;
                    info = ItemMain.sword[num1].Information;
                    effect = ItemMain.sword[num1].Effect;
                    itemdetail = name + "\n\n" + Textnumchange(info, 32) + Textnumchange(effect, 32);
                }
                break;
            case "Rod":
                if (num1 < ItemMain.rod.Count)
                {
                    itemname = ItemMain.rod[num1].Name;
                    info = ItemMain.rod[num1].Information;
                    effect = ItemMain.rod[num1].Effect;
                    itemdetail = name + "\n\n" + Textnumchange(info, 32) + Textnumchange(effect, 32);
                }
                break;
            case "Pellet":
                if (num1 < ItemMain.pellet.Count)
                {
                    itemname = ItemMain.pellet[num1].Name;
                    info = ItemMain.pellet[num1].Information;
                    effect = ItemMain.pellet[num1].Effect;
                    itemdetail = name + "\n\n" + Textnumchange(info, 32) + Textnumchange(effect, 32);
                }
                break;
            default:
                break;
        }
        GameObject.Find("ItemDetail").GetComponent<TextMesh>().text = itemdetail;

    }

    private void OnMouseExit()
    {

        //Debug.Log("能进来");
        GameObject.Find("ItemDetail").GetComponent<TextMesh>().text = " ";
    }
    */
}
