using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSwitch : MonoBehaviour
{
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            string ButtonName = button.name;
            string type = ItemMain.itemtype;                 //获取切换前物品栏类型

            if (!string.Equals(ButtonName, type))
            {
                ClearGrid(transform.parent);
                ItemMain.itemtype = ButtonName;

                transform.parent.Find(type).GetComponent<Button>().image.color = new Color32(255, 255, 255, 255);
                transform.parent.Find(ButtonName).GetComponent<Button>().image.color = new Color32(226, 128, 106, 255);

                switch (ButtonName)
                {
                    case "Alcohol":
                        for (int n = 0; n < ItemMain.alcohol.Count; n++)
                        {
                            ItemMain.SetItem(n, transform.parent.Find(n.ToString()).gameObject, ItemMain.alcohol);
                        }
                        break;
                    case "Food":
                        for (int n = 0; n < ItemMain.food.Count; n++)
                        {
                            ItemMain.SetItem(n, transform.parent.Find(n.ToString()).gameObject, ItemMain.food);
                        }
                        break;
                    case "Knife":
                        for (int n = 0; n < ItemMain.knife.Count; n++)
                        {
                            ItemMain.SetItem(n, transform.parent.Find(n.ToString()).gameObject, ItemMain.knife);
                        }
                        break;
                    case "Sword":
                        for (int n = 0; n < ItemMain.sword.Count; n++)
                        {
                            ItemMain.SetItem(n, transform.parent.Find(n.ToString()).gameObject, ItemMain.sword);
                        }
                        break;
                    case "Rod":
                        for (int n = 0; n < ItemMain.rod.Count; n++)
                        {
                            ItemMain.SetItem(n, transform.parent.Find(n.ToString()).gameObject, ItemMain.rod);
                        }
                        break;
                    case "Pellet":
                        for (int n = 0; n < ItemMain.pellet.Count; n++)
                        {
                            ItemMain.SetItem(n, transform.parent.Find(n.ToString()).gameObject, ItemMain.pellet);
                        }
                        break;
                    default:
                        break;

                }
            }

        });
    }

    public static void ClearGrid(Transform parent)
    {
        for (int n = 0; n < 30; n++)
        {
            Transform grid = parent.Find(n.ToString());
            grid.GetComponent<Button>().image.color = new Color32(255, 255, 255, 0);
            grid.Find("num" + n.ToString()).GetComponent<Image>().color = new Color32(241, 162, 65, 0);
            grid.Find("num" + n.ToString()).gameObject.transform.Find("Text").GetComponent<Text>().text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
