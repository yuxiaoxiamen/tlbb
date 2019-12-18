using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemShow : MonoBehaviour
{
    private Button button;
    PointerEventData eventData;

    void Start()
    {
        button = GetComponent<Button>();
        /*button.OnPointerEnter(eventData).AddListener(() =>
        {
            float num = float.Parse(gameObject.name);
            int num1 = (int)num;
            GameObject.Find("ItemName").GetComponent<TextMesh>().text = ButtonList.item[num1].Name;
            GameObject.Find("ItemIntro").GetComponent<TextMesh>().text = ButtonList.item[num1].Introduction;
            GameObject.Find("ItemEffect").GetComponent<TextMesh>().text = ButtonList.item[num1].Effect;
        });
        button.OnPointerEnter(eventData).AddListener(() =>
        {
            float num = float.Parse(gameObject.name);
            int num1 = (int)num;
            GameObject.Find("ItemIntro").GetComponent<TextMesh>().text = " ";
            GameObject.Find("ItemEffect").GetComponent<TextMesh>().text = " ";
            GameObject.Find("ItemName").GetComponent<TextMesh>().text = " ";
        });*/
    }
}
