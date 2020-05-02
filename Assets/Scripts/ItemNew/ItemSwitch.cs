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

                for(int n=0;n<30;n++)
                    GameObject.Find(n.ToString()).GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);

                /*
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
                    case "Pellet":
                        for (int n = 0; n < ItemMain.pellet.Count; n++)
                            GameObject.Find(n.ToString()).GetComponent<SpriteRenderer>().color = new Color32(239, 249, 250, 255);
                        break;
                    default:
                        break;
                }*/

                ItemMain.itemtype = ButtonName;

                switch (ButtonName)
                {
                    case "Alcohol":
                        for (int n = 0; n < ItemMain.alcohol.Count; n++)
                        {
                            float spritex, spritey;
                            float offSetx, offSety;
                            var sp = GameObject.Find(n.ToString()).GetComponent<SpriteRenderer>();
                            spritex = sp.bounds.size.x;
                            spritey = sp.bounds.size.y;
                            sp.sprite = Resources.Load<Sprite>("ItemIcon/" + ItemMain.alcohol[n].Id.ToString());
                            offSetx = sp.transform.localScale.x;
                            offSety = sp.transform.localScale.y;
                            sp.transform.localScale = new Vector3(offSetx * spritex / sp.bounds.size.x, offSety * spritey / sp.bounds.size.y, 1);
                            sp.color = new Color32(255, 255, 255, 255);
                        }
                        break;
                    case "Food":
                        for (int n = 0; n < ItemMain.food.Count; n++)
                        {
                            float spritex, spritey;
                            float offSetx, offSety;
                            var sp = GameObject.Find(n.ToString()).GetComponent<SpriteRenderer>();
                            spritex = sp.bounds.size.x;
                            spritey = sp.bounds.size.y;
                            sp.sprite = Resources.Load<Sprite>("ItemIcon/" + ItemMain.food[n].Id.ToString());
                            offSetx = sp.transform.localScale.x;
                            offSety = sp.transform.localScale.y;
                            sp.transform.localScale = new Vector3(offSetx * spritex / sp.bounds.size.x, offSety * spritey / sp.bounds.size.y, 1);
                            sp.color = new Color32(255, 255, 255, 255);
                        }
                        break;
                    case "Knife":
                        for (int n = 0; n < ItemMain.knife.Count; n++)
                        {
                            float spritex, spritey;
                            float offSetx, offSety;
                            var sp = GameObject.Find(n.ToString()).GetComponent<SpriteRenderer>();
                            spritex = sp.bounds.size.x;
                            spritey = sp.bounds.size.y;
                            sp.sprite = Resources.Load<Sprite>("ItemIcon/" + ItemMain.knife[n].Id.ToString());
                            offSetx = sp.transform.localScale.x;
                            offSety = sp.transform.localScale.y;
                            sp.transform.localScale = new Vector3(offSetx * spritex / sp.bounds.size.x, offSety * spritey / sp.bounds.size.y, 1);
                            sp.color = new Color32(255, 255, 255, 255);
                        }
                        break;
                    case "Sword":
                        for (int n = 0; n < ItemMain.sword.Count; n++)
                        {
                            float spritex, spritey;
                            float offSetx, offSety;
                            var sp = GameObject.Find(n.ToString()).GetComponent<SpriteRenderer>();
                            spritex = sp.bounds.size.x;
                            spritey = sp.bounds.size.y;
                            sp.sprite = Resources.Load<Sprite>("ItemIcon/" + ItemMain.sword[n].Id.ToString());
                            offSetx = sp.transform.localScale.x;
                            offSety = sp.transform.localScale.y;
                            sp.transform.localScale = new Vector3(offSetx * spritex / sp.bounds.size.x, offSety * spritey / sp.bounds.size.y, 1);
                            sp.color = new Color32(255, 255, 255, 255);
                        }
                        break;
                    case "Rod":
                        for (int n = 0; n < ItemMain.rod.Count; n++)
                        {
                            float spritex, spritey;
                            float offSetx, offSety;
                            var sp = GameObject.Find(n.ToString()).GetComponent<SpriteRenderer>();
                            spritex = sp.bounds.size.x;
                            spritey = sp.bounds.size.y;
                            sp.sprite = Resources.Load<Sprite>("ItemIcon/" + ItemMain.rod[n].Id.ToString());
                            offSetx = sp.transform.localScale.x;
                            offSety = sp.transform.localScale.y;
                            sp.transform.localScale = new Vector3(offSetx * spritex / sp.bounds.size.x, offSety * spritey / sp.bounds.size.y, 1);
                            sp.color = new Color32(255, 255, 255, 255);
                        }
                        break;
                    case "Pellet":
                        for (int n = 0; n < ItemMain.pellet.Count; n++)
                        {
                            float spritex, spritey;
                            float offSetx, offSety;
                            var sp = GameObject.Find(n.ToString()).GetComponent<SpriteRenderer>();
                            spritex = sp.bounds.size.x;
                            spritey = sp.bounds.size.y;
                            sp.sprite = Resources.Load<Sprite>("ItemIcon/" + ItemMain.pellet[n].Id.ToString());
                            offSetx = sp.transform.localScale.x;
                            offSety = sp.transform.localScale.y;
                            sp.transform.localScale = new Vector3(offSetx * spritex / sp.bounds.size.x, offSety * spritey / sp.bounds.size.y, 1);
                            sp.color = new Color32(255, 255, 255, 255);
                        }
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
