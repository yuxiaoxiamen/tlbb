using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMain : MonoBehaviour
{
    public static List<Good> item = new List<Good>();                               //物品读取接口
    public static List<Good> alcohol = new List<Good>();
    public static List<Good> food = new List<Good>();
    public static List<Good> knife = new List<Good>();
    public static List<Good> sword = new List<Good>();
    public static List<Good> rod = new List<Good>();
    public static string itemtype;
    public static Dictionary<string, int> itemNum = new Dictionary<string, int>();       //物品数量记录
    public static string equipmentNow = "无";

    static ItemMain()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        item.Add(GlobalData.Items[0]);
        item.Add(GlobalData.Items[1]);
        item.Add(GlobalData.Items[2]);
        item.Add(GlobalData.Items[3]);
        item.Add(GlobalData.Items[4]);
        item.Add(GlobalData.Items[5]);
        item.Add(GlobalData.Items[10]);
        item.Add(GlobalData.Items[11]);
        item.Add(GlobalData.Items[12]);
        item.Add(GlobalData.Items[13]);
        item.Add(GlobalData.Items[14]);
        item.Add(GlobalData.Items[15]);
        item.Add(GlobalData.Items[22]);
        item.Add(GlobalData.Items[23]);
        item.Add(GlobalData.Items[24]);
        item.Add(GlobalData.Items[25]);
        item.Add(GlobalData.Items[47]);
        item.Add(GlobalData.Items[48]);
        item.Add(GlobalData.Items[49]);
        item.Add(GlobalData.Items[50]);
        item.Add(GlobalData.Items[73]);
        item.Add(GlobalData.Items[74]);

        int num;

        for(int i=0;i<item.Count;i++)
        {
            string kind = item[i].Type.ToString();
            switch(kind)
            {
                case "Alcohol":
                    alcohol.Add(item[i]);
                    break;
                case "Food":
                    food.Add(item[i]);
                    break;
                case "Knife":
                    knife.Add(item[i]);
                    break;
                case "Sword":
                    sword.Add(item[i]);
                    break;
                case "Rod":
                    rod.Add(item[i]);
                    break;
                default:
                    break;
            }

            if (itemNum.ContainsKey(item[i].Name) == false)
            {
                num = 1;
                itemNum.Add(item[i].Name, num);
            }

            else
            {
                num = itemNum[item[i].Name];
                num++;
                itemNum[item[i].Name] = num;
            }
        }

        //默认显示所有酒
        for(int n=0;n<alcohol.Count;n++)
            GameObject.Find(n.ToString()).GetComponent<SpriteRenderer>().color= new Color32(100, 100, 100, 255);

        itemtype = "Alcohol";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
