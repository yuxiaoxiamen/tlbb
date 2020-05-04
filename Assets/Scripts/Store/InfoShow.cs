using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfoShow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Good item = GlobalData.Items[int.Parse(gameObject.name)];
        string s = item.Information + System.Environment.NewLine + item.Effect;
        GameObject.Find("info").transform.Find("text").GetComponent<Text>().text = s;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameObject.Find("info").transform.Find("text").GetComponent<Text>().text = "";
    }
}