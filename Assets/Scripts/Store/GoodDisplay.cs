using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoodDisplay : MonoBehaviour
{
    public List<Good> goods = new List<Good>();
    public GameObject oneLinePrefab1;
    public GameObject oneLinePrefab2;
    public static string storeType;
    public static bool isBuy;
    private Text moneyText;
    public static GoodDisplay instance;

    static GoodDisplay()
    {
        storeType = "Blacksmith";
        isBuy = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void SetItemList()
    {
        for(int i = 0; i < transform.childCount; ++i)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        goods = GetGood(storeType);
        int lines = goods.Count / 4;
        GameObject oneLinePrefab = oneLinePrefab1;
        if (storeType == "Blacksmith")
        {
            oneLinePrefab = oneLinePrefab2;
        }
        for (int i = 0; i < lines; ++i)
        {
            GameObject oneLineObject = Instantiate(oneLinePrefab);
            RectTransform oneLineTransform = oneLineObject.GetComponent<RectTransform>();
            oneLineTransform.SetParent(gameObject.GetComponent<RectTransform>());
            oneLineTransform.localPosition = Vector3.zero;
            oneLineTransform.localRotation = Quaternion.identity;
            oneLineTransform.localScale = Vector3.one;
            for (int j = 0; j < 4; ++j)
            {
                SetItem(oneLineTransform.GetChild(j).gameObject, goods[i + j]);
            }
        }

        int count = goods.Count % 4;
        if (count != 0)
        {
            GameObject oneLineObject = Instantiate(oneLinePrefab);
            RectTransform oneLineTransform = oneLineObject.GetComponent<RectTransform>();
            oneLineTransform.SetParent(gameObject.GetComponent<RectTransform>());
            oneLineTransform.localPosition = Vector3.zero;
            oneLineTransform.localRotation = Quaternion.identity;
            oneLineTransform.localScale = Vector3.one;
            for (int j = 0; j < count; ++j)
            {
                SetItem(oneLineTransform.GetChild(j).gameObject, goods[lines + j]);
            }
            for (int i = count; i < 4; ++i)
            {
                Destroy(oneLineTransform.GetChild(i).gameObject);
            }
        }
    }

    void SetItem(GameObject itemObject, Good item)
    {
        itemObject.name = item.Id + "";
        itemObject.transform.Find("name").GetComponent<Text>().text = item.Name;
        itemObject.transform.Find("price").GetComponent<Text>().text = item.SellingPrice+"钱";
        itemObject.transform.Find("img").GetComponent<Image>().sprite = Resources.Load<Sprite>("itemIcon/" + item.Id);
        itemObject.GetComponent<Button>().onClick.AddListener(()=>
        {
            if (!ConfirmOperation.instance.isInConfirm)
            {
                ConfirmOperation.instance.SetItem(item);
            }
        });
    }

    List<Good> GetGood(string type)
    {
        List<Good> items = new List<Good>();
        if(!isBuy)
        {
            items = GameRunningData.GetRunningData().belongings;
        }
        else
        {
            foreach (Good item in GlobalData.Items)
            {
                if(storeType == "Blacksmith")
                {
                    if(item.Type == ItemKind.Knife || item.Type == ItemKind.Sword || item.Type == ItemKind.Rod)
                    {
                        items.Add(item);
                    }
                }
                else
                {
                    if (Enum.GetName(typeof(ItemKind), item.Type) == type)
                    {
                        items.Add(item);
                    }
                }
            }
        }
        return items;
    }
}
