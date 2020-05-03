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

                for (int n = 0; n < 30; n++)
                { GameObject.Find(n.ToString()).GetComponent<Button>().image.color = new Color32(255, 255, 255, 0);
                  GameObject.Find("num" + n.ToString()).GetComponent<Image>().color = new Color32(241, 162, 65, 0);
                  GameObject.Find("num" + n.ToString()).gameObject.transform.Find("Text").GetComponent<Text>().text = "";
                }

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

                GameObject.Find(type).GetComponent<Button>().image.color = new Color32(255, 255, 255, 255);
                GameObject.Find(ButtonName).GetComponent<Button>().image.color = new Color32(226, 128, 106, 255);

                switch (ButtonName)
                {
                    case "Alcohol":
                        for (int n = 0; n < ItemMain.alcohol.Count; n++)
                        {/*
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
                            */
                            var sp = GameObject.Find(n.ToString()).GetComponent<Button>().image;
                            sp.sprite = Resources.Load<Sprite>("ItemIcon/" + ItemMain.alcohol[n].Id.ToString());
                            sp.color = new Color32(255, 255, 255, 255);

                            GameObject.Find("num" + n.ToString()).GetComponent<Image>().color = new Color32(241, 162, 65, 255);
                            GameObject.Find("num" + n.ToString()).gameObject.transform.Find("Text").GetComponent<Text>().text = ItemMain.alcohol[n].Number.ToString();
                        }
                        break;
                    case "Food":
                        for (int n = 0; n < ItemMain.food.Count; n++)
                        {/*
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
                            */
                            var sp = GameObject.Find(n.ToString()).GetComponent<Button>().image;
                            sp.sprite = Resources.Load<Sprite>("ItemIcon/" + ItemMain.food[n].Id.ToString());
                            sp.color = new Color32(255, 255, 255, 255);

                            GameObject.Find("num" + n.ToString()).GetComponent<Image>().color = new Color32(241, 162, 65, 255);
                            GameObject.Find("num" + n.ToString()).gameObject.transform.Find("Text").GetComponent<Text>().text = ItemMain.food[n].Number.ToString();
                        }
                        break;
                    case "Knife":
                        for (int n = 0; n < ItemMain.knife.Count; n++)
                        {/*
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
                            */
                            var sp = GameObject.Find(n.ToString()).GetComponent<Button>().image;
                            sp.sprite = Resources.Load<Sprite>("ItemIcon/" + ItemMain.knife[n].Id.ToString());
                            sp.color = new Color32(255, 255, 255, 255);

                            GameObject.Find("num" + n.ToString()).GetComponent<Image>().color = new Color32(241, 162, 65, 255);
                            GameObject.Find("num" + n.ToString()).gameObject.transform.Find("Text").GetComponent<Text>().text = ItemMain.knife[n].Number.ToString();
                        }
                        break;
                    case "Sword":
                        for (int n = 0; n < ItemMain.sword.Count; n++)
                        {/*
                            float spritex, spritey;
                            float offSetx, offSety;
                            var sp = GameObject.Find(n.ToString()).GetComponent<SpriteRenderer>();
                            spritex = sp.bounds.size.x;
                            spritey = sp.bounds.size.y;
                            sp.sprite = Resources.Load<Sprite>("ItemIcon/" + ItemMain.sword[n].Id.ToString());
                            offSetx = sp.transform.localScale.x;
                            offSety = sp.transform.localScale.y;
                            sp.transform.localScale = new Vector3(offSetx * spritex / sp.bounds.size.x, offSety * spritey / sp.bounds.size.y, 1);
                            sp.color = new Color32(255, 255, 255, 255);*/
                            var sp = GameObject.Find(n.ToString()).GetComponent<Button>().image;
                            sp.sprite = Resources.Load<Sprite>("ItemIcon/" + ItemMain.sword[n].Id.ToString());
                            sp.color = new Color32(255, 255, 255, 255);

                            GameObject.Find("num" + n.ToString()).GetComponent<Image>().color = new Color32(241, 162, 65, 255);
                            GameObject.Find("num" + n.ToString()).gameObject.transform.Find("Text").GetComponent<Text>().text = ItemMain.sword[n].Number.ToString();
                        }
                        break;
                    case "Rod":
                        for (int n = 0; n < ItemMain.rod.Count; n++)
                        {/*
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
                            */
                            var sp = GameObject.Find(n.ToString()).GetComponent<Button>().image;
                            sp.sprite = Resources.Load<Sprite>("ItemIcon/" + ItemMain.rod[n].Id.ToString());
                            sp.color = new Color32(255, 255, 255, 255);

                            GameObject.Find("num" + n.ToString()).GetComponent<Image>().color = new Color32(241, 162, 65, 255);
                            GameObject.Find("num" + n.ToString()).gameObject.transform.Find("Text").GetComponent<Text>().text = ItemMain.rod[n].Number.ToString();
                        }
                        break;
                    case "Pellet":
                        for (int n = 0; n < ItemMain.pellet.Count; n++)
                        {/*
                            float spritex, spritey;
                            float offSetx, offSety;
                            var sp = GameObject.Find(n.ToString()).GetComponent<SpriteRenderer>();
                            spritex = sp.bounds.size.x;
                            spritey = sp.bounds.size.y;
                            sp.sprite = Resources.Load<Sprite>("ItemIcon/" + ItemMain.pellet[n].Id.ToString());
                            offSetx = sp.transform.localScale.x;
                            offSety = sp.transform.localScale.y;
                            sp.transform.localScale = new Vector3(offSetx * spritex / sp.bounds.size.x, offSety * spritey / sp.bounds.size.y, 1);
                            sp.color = new Color32(255, 255, 255, 255);*/
                            var sp = GameObject.Find(n.ToString()).GetComponent<Button>().image;
                            sp.sprite = Resources.Load<Sprite>("ItemIcon/" + ItemMain.pellet[n].Id.ToString());
                            sp.color = new Color32(255, 255, 255, 255);

                            GameObject.Find("num" + n.ToString()).GetComponent<Image>().color = new Color32(241, 162, 65, 255);
                            GameObject.Find("num" + n.ToString()).gameObject.transform.Find("Text").GetComponent<Text>().text = ItemMain.pellet[n].Number.ToString();
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
