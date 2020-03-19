using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreMain : MonoBehaviour
{
    public static List<Good> alcohol = new List<Good>();   //酒读取接口
    public GameObject itemButtonPrefab;                    //商品
    public static string storetype;                               //商店类型

    List<GameObject> temp = new List<GameObject>();

    static StoreMain()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        alcohol.Add(GlobalData.Items[0]);
        alcohol.Add(GlobalData.Items[1]);
        alcohol.Add(GlobalData.Items[2]);
        alcohol.Add(GlobalData.Items[3]);
        alcohol.Add(GlobalData.Items[4]);
        alcohol.Add(GlobalData.Items[5]);
        alcohol.Add(GlobalData.Items[6]);
        alcohol.Add(GlobalData.Items[7]);
        alcohol.Add(GlobalData.Items[8]);
        alcohol.Add(GlobalData.Items[9]);


        for (int i = 0; i < alcohol.Count; ++i)
        {
            GameObject itemButton;

            itemButton = Instantiate(itemButtonPrefab);
            RectTransform itemButtonTransform = itemButton.GetComponent<RectTransform>();
            itemButtonTransform.SetParent(gameObject.GetComponent<RectTransform>());

            temp.Add(itemButton);
            temp[i].name = i.ToString();
            temp[i].transform.Find("Text").GetComponent<Text>().text = "  名称："+alcohol[i].Name+"\n  金钱："+alcohol[i].SellingPrice;

        }

        storetype = "Alcohol";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
