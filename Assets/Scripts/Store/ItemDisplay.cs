using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public static List<Good> item = new List<Good>();   //物品读取接口
    public GameObject itemButtonPrefab;                    //商品

    List<GameObject> temp = new List<GameObject>();
    List<Good> itemtemp = new List<Good>();

    // Start is called before the first frame update
    void Start()
    {
        string type = StoreMain.storetype;
        //Debug.Log(type);


        //导入人物物品
        //foreach (var x in GameRunningData.belongings){ itemtemp.Add(x); }
        //test
        itemtemp.Add(GlobalData.Items[0]);
        itemtemp.Add(GlobalData.Items[1]);
        itemtemp.Add(GlobalData.Items[2]);
        itemtemp.Add(GlobalData.Items[3]);
        itemtemp.Add(GlobalData.Items[4]);
        itemtemp.Add(GlobalData.Items[5]);
        itemtemp.Add(GlobalData.Items[10]);
        itemtemp.Add(GlobalData.Items[11]);
        itemtemp.Add(GlobalData.Items[12]);
        itemtemp.Add(GlobalData.Items[13]);
        itemtemp.Add(GlobalData.Items[14]);
        itemtemp.Add(GlobalData.Items[15]);
        itemtemp.Add(GlobalData.Items[22]);
        itemtemp.Add(GlobalData.Items[23]);
        itemtemp.Add(GlobalData.Items[24]);
        itemtemp.Add(GlobalData.Items[25]);
        itemtemp.Add(GlobalData.Items[47]);
        itemtemp.Add(GlobalData.Items[48]);
        itemtemp.Add(GlobalData.Items[49]);
        itemtemp.Add(GlobalData.Items[50]);
        itemtemp.Add(GlobalData.Items[73]);
        itemtemp.Add(GlobalData.Items[74]);

        for (int i = 0; i < itemtemp.Count; i++)
        {
            string kind = itemtemp[i].Type.ToString();
            //Debug.Log(i.ToString()+kind+type);

            if (string.Equals(type, kind))
            {
                item.Add(itemtemp[i]); 
            }
        }


        for (int i = 0; i < item.Count; ++i)
        {
            GameObject itemButton;

            itemButton = Instantiate(itemButtonPrefab);
            RectTransform itemButtonTransform = itemButton.GetComponent<RectTransform>();
            itemButtonTransform.SetParent(gameObject.GetComponent<RectTransform>());

            temp.Add(itemButton);
            temp[i].name = i.ToString();
            temp[i].transform.Find("Text").GetComponent<Text>().text = "  " + item[i].Name + "\n  金钱：" + item[i].ResumeValue;

        }

        GameObject.Find("Items").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
