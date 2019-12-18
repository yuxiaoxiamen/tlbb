using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonList : MonoBehaviour
{
    public static List<Good> item = new List<Good>();                               //物品读取接口
    public static Dictionary<string, int> itemNum = new Dictionary<string, int>();       //物品数量记录
    public static string equipmentNow = "无";
    int num;
    public GameObject itemButtonPrefab;
    List<GameObject> temp = new List<GameObject>();

    static ButtonList() {
        

    }

    // Start is called before the first frame update
    void Start()
    {
        
        item.Add(GlobalData.Items[0]);
        item.Add(GlobalData.Items[1]);
        item.Add(GlobalData.Items[2]);
        item.Add(GlobalData.Items[23]);
        item.Add(GlobalData.Items[54]);
        item.Add(GlobalData.Items[5]);
        item.Add(GlobalData.Items[77]);
        

        for (int i = 0; i < item.Count; ++i)
        {
            GameObject itemButton;

            itemButton = Instantiate(itemButtonPrefab);
            RectTransform itemButtonTransform = itemButton.GetComponent<RectTransform>();
            itemButtonTransform.SetParent(gameObject.GetComponent<RectTransform>());

            temp.Add(itemButton);
            temp[i].name=i.ToString();

            if(itemNum.ContainsKey(item[i].Name) == false)
            {
                num = 1;
                itemNum.Add(item[i].Name,num);
            }

            else
            {
                num = itemNum[item[i].Name];
                num++;
                itemNum[item[i].Name] = num;
            }

            temp[i].transform.Find("Text").GetComponent<Text>().text = item[i].Name+"         "+ item[i].SellingPrice + "       "+ num;

        }
        GameObject.Find("equipment").GetComponent<TextMesh>().text = equipmentNow;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
