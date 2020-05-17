using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMain : MonoBehaviour
{
    public List<Good> item = new List<Good>();                               //物品读取接口
    public static List<Good> alcohol = new List<Good>();
    public static List<Good> food = new List<Good>();
    public static List<Good> knife = new List<Good>();
    public static List<Good> sword = new List<Good>();
    public static List<Good> rod = new List<Good>();
    public static List<Good> pellet = new List<Good>();
    public static string itemtype;
    public static string equipmentNow = "";
    public static int dialogState = 0;
    public static ItemMain instance;

    public static void ClearItems()
    {
        alcohol.Clear();
        food.Clear();
        knife.Clear();
        sword.Clear();
        rod.Clear();
        pellet.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        foreach(var belonging in GameRunningData.GetRunningData().belongings)
        {
            item.Add(belonging);
        }

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
            SetItem(n, transform.Find(n + "").gameObject, alcohol);
        }

        SetWeapon();
        transform.Find("MoneyValue").GetComponent<Text>().text = GameRunningData.GetRunningData().money.ToString();
        itemtype = "Alcohol";
    }

    public void SetWeapon()
    {
        if(ItemUse.user != null)
        {
            if (ItemUse.user.EquippedWeapon == null)
                equipmentNow = "无";
            else
                equipmentNow = ItemUse.user.EquippedWeapon.Name.ToString();
            transform.Find("EquipmentValue").GetComponent<Text>().text = equipmentNow;
        }
    }

    public static void SetItem(int n, GameObject itemObject, List<Good> items)
    {
        var sp = itemObject.GetComponent<Button>().image;
        sp.sprite = Resources.Load<Sprite>("ItemIcon/" + items[n].Id.ToString());
        sp.color = new Color32(255, 255, 255, 255);

        itemObject.transform.Find("num" + n.ToString()).GetComponent<Image>().color = new Color32(241, 162, 65, 255);
        itemObject.transform.Find("num" + n.ToString()).gameObject.transform.Find("Text").GetComponent<Text>().text = items[n].Number.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
