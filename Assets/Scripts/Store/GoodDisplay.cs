using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoodDisplay : MonoBehaviour
{
    public static List<Good> good = new List<Good>();   //商品读取接口
    public GameObject itemButtonPrefab;                    //商品

    List<GameObject> temp = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        string type = StoreMain.storetype;
        //导入商店商品
        switch (type)
        {
            case "Alcohol":
                for (int i = 0; i < 10; i++)
                    good.Add(GlobalData.Items[i]);
                break;
            case "Food":
                for (int i = 10; i < 22; i++)
                    good.Add(GlobalData.Items[i]);
                break;
            case "Weapon":
                for (int i = 22; i < 80; i++)
                    good.Add(GlobalData.Items[i]);
                break;
            default:
                break;
        }

        //按钮颜色变化
        GameObject.Find("buy").GetComponent<Image>().color = Color.clear;
        GameObject.Find("sell").GetComponent<Image>().color = Color.gray;




        //显示默认状态下的商品信息
        for (int i = 0; i < good.Count; ++i)
        {
            GameObject itemButton;

            itemButton = Instantiate(itemButtonPrefab);
            RectTransform itemButtonTransform = itemButton.GetComponent<RectTransform>();
            itemButtonTransform.SetParent(gameObject.GetComponent<RectTransform>());

            temp.Add(itemButton);
            temp[i].name = i.ToString();
            temp[i].transform.Find("Text").GetComponent<Text>().text = "  " + good[i].Name + "\n  金钱：" + good[i].SellingPrice;

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
