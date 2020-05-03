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
    public static List<Good> pellet = new List<Good>();
    public static string itemtype;
    public static string equipmentNow = "";
    public static int dialogState = 0;

    static ItemMain()
    {

    }


    // Start is called before the first frame update
    void Start()
    {

        foreach(var belonging in GameRunningData.GetRunningData().belongings)
        {
            item.Add(belonging);
        }

        /*
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


    */

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
                case "Pellet":
                    pellet.Add(item[i]);
                    break;
                default:
                    break;
            }

        }

        //默认显示所有酒
      

        for(int n = 0; n < alcohol.Count; n++)
        {
            /*
            float spritex, spritey;
            float offSetx, offSety;
            var sp = GameObject.Find(n.ToString()).GetComponent<SpriteRenderer>();
            spritex =sp.bounds.size.x;
            spritey = sp.bounds.size.y;
            sp.sprite = Resources.Load<Sprite>("ItemIcon/" + alcohol[n].Id.ToString());
            offSetx = sp.transform.localScale.x;
            offSety = sp.transform.localScale.y;
            sp.transform.localScale = new Vector3(offSetx * spritex / sp.bounds.size.x, offSety * spritey / sp.bounds.size.y, 1);
            sp.color = new Color32(255, 255, 255, 255);
            */
            var sp= GameObject.Find(n.ToString()).GetComponent<Button>().image;
            sp.sprite = Resources.Load<Sprite>("ItemIcon/" + alcohol[n].Id.ToString());
            sp.color = new Color32(255, 255, 255, 255);

            GameObject.Find("num" + n.ToString()).GetComponent<Image>().color= new Color32(241, 162, 65, 255);
            GameObject.Find("num" + n.ToString()).gameObject.transform.Find("Text").GetComponent<Text>().text = alcohol[n].Number.ToString();
        }

        if (GameRunningData.GetRunningData().player.EquippedWeapon == null)
            equipmentNow = "无";
        else
            equipmentNow = GameRunningData.GetRunningData().player.EquippedWeapon.Name.ToString();


        GameObject.Find("MoneyValue").GetComponent<TextMesh>().text = GameRunningData.GetRunningData().money.ToString();
        GameObject.Find("EquipmentValue").GetComponent<TextMesh>().text = equipmentNow;


        itemtype = "Alcohol";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
