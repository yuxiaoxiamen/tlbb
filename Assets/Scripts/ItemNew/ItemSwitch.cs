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
                //清空物品栏
                switch (type)
                {
                    case "Alcohol":
                        for (int n = 0; n < ItemMain.alcohol.Count; n++)
                            GameObject.Find(n.ToString()).GetComponent<SpriteRenderer>().color = new Color32(239, 249, 250, 255);
                        break;
                    case "Food":
                        for (int n = 0; n < ItemMain.food.Count; n++)
                            GameObject.Find(n.ToString()).GetComponent<SpriteRenderer>().color = new Color32(239, 249, 250, 255);
                        break;
                    case "Knife":
                        for (int n = 0; n < ItemMain.knife.Count; n++)
                            GameObject.Find(n.ToString()).GetComponent<SpriteRenderer>().color = new Color32(239, 249, 250, 255);
                        break;
                    case "Sword":
                        for (int n = 0; n < ItemMain.sword.Count; n++)
                            GameObject.Find(n.ToString()).GetComponent<SpriteRenderer>().color = new Color32(239, 249, 250, 255);
                        break;
                    case "Rod":
                        for (int n = 0; n < ItemMain.rod.Count; n++)
                            GameObject.Find(n.ToString()).GetComponent<SpriteRenderer>().color = new Color32(239, 249, 250, 255);
                        break;
                    default:
                        break;
                }

                ItemMain.itemtype = ButtonName;

                switch (ButtonName)
                {
                    case "Alcohol":
                        for (int n = 0; n < ItemMain.alcohol.Count; n++)
                            GameObject.Find(n.ToString()).GetComponent<SpriteRenderer>().color = new Color32(100, 100, 100, 255);
                        break;
                    case "Food":
                        for (int n = 0; n < ItemMain.food.Count; n++)
                            GameObject.Find(n.ToString()).GetComponent<SpriteRenderer>().color = new Color32(30, 130, 160, 255);
                        break;
                    case "Knife":
                        for (int n = 0; n < ItemMain.knife.Count; n++)
                            GameObject.Find(n.ToString()).GetComponent<SpriteRenderer>().color = new Color32(10, 160, 10, 255);
                        break;
                    case "Sword":
                        for (int n = 0; n < ItemMain.sword.Count; n++)
                            GameObject.Find(n.ToString()).GetComponent<SpriteRenderer>().color = new Color32(90, 90, 10, 255);
                        break;
                    case "Rod":
                        for (int n = 0; n < ItemMain.rod.Count; n++)
                            GameObject.Find(n.ToString()).GetComponent<SpriteRenderer>().color = new Color32(220, 20, 22, 255);
                        break;
                    default:
                        break;

                }
            }

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
