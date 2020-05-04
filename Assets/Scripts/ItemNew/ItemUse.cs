using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUse : MonoBehaviour
{
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            GameObject root = GameObject.Find("Canvas");
            GameObject dialog = root.transform.Find("Dialog").gameObject;
            dialog.SetActive(true);
            ItemMain.dialogState = 1;
            //dialog.transform.Find("DialogTittle").gameObject.SetActive(true);
            //dialog.transform.Find("DialogText").gameObject.SetActive(true);

            string ButtonName = button.name;
            int num1 = 0;
            int.TryParse(ButtonName, out num1);

            string type = ItemMain.itemtype;                 //获取物品栏类型
            string effect="";                               //物品效果
            string tittle = "物品使用成功";

            switch (type)
            {
                case "Alcohol":
                    if (num1 < ItemMain.alcohol.Count)
                    {
                        effect=ItemMain.alcohol[num1].Effect;
                        ItemMain.alcohol[num1].Number--;
                        if(ItemMain.alcohol[num1].Number>0)
                        {
                            GameObject.Find("num" + num1.ToString()).gameObject.transform.Find("Text").GetComponent<Text>().text = ItemMain.alcohol[num1].Number.ToString();
                        }
                        else
                        {
                            foreach(var belonging in GameRunningData.GetRunningData().belongings)
                            {
                                if(ItemMain.alcohol[num1].Name.Equals(belonging.Name))
                                {
                                    GameRunningData.GetRunningData().belongings.Remove(belonging);
                                    break;
                                }
                            }
                            ItemMain.alcohol.Remove(ItemMain.alcohol[num1]);
                            //清空物品栏

                            for (int n = 0; n < 30; n++)
                            {
                                GameObject.Find(n.ToString()).GetComponent<Button>().image.color = new Color32(255, 255, 255, 0);
                                GameObject.Find("num" + n.ToString()).GetComponent<Image>().color = new Color32(241, 162, 65, 0);
                                GameObject.Find("num" + n.ToString()).gameObject.transform.Find("Text").GetComponent<Text>().text = "";
                            }
                            for (int n = 0; n < ItemMain.alcohol.Count; n++)
                            {
                                var sp = GameObject.Find(n.ToString()).GetComponent<Button>().image;
                                sp.sprite = Resources.Load<Sprite>("ItemIcon/" + ItemMain.alcohol[n].Id.ToString());
                                sp.color = new Color32(255, 255, 255, 255);

                                GameObject.Find("num" + n.ToString()).GetComponent<Image>().color = new Color32(241, 162, 65, 255);
                                GameObject.Find("num" + n.ToString()).gameObject.transform.Find("Text").GetComponent<Text>().text = ItemMain.alcohol[n].Number.ToString();
                            }
                        }
                    }
                    break;
                case "Food":
                    if (num1 < ItemMain.food.Count)
                    {
                        effect = ItemMain.food[num1].Effect;
                        ItemMain.food[num1].Number--;
                        if (ItemMain.food[num1].Number > 0)
                        {
                            GameObject.Find("num" + num1.ToString()).gameObject.transform.Find("Text").GetComponent<Text>().text = ItemMain.food[num1].Number.ToString();
                        }
                        else
                        {
                            foreach (var belonging in GameRunningData.GetRunningData().belongings)
                            {
                                if (ItemMain.food[num1].Name.Equals(belonging.Name))
                                {
                                    GameRunningData.GetRunningData().belongings.Remove(belonging);
                                    break;
                                }
                            }
                            ItemMain.food.Remove(ItemMain.food[num1]);
                            //清空物品栏

                            for (int n = 0; n < 30; n++)
                            {
                                GameObject.Find(n.ToString()).GetComponent<Button>().image.color = new Color32(255, 255, 255, 0);
                                GameObject.Find("num" + n.ToString()).GetComponent<Image>().color = new Color32(241, 162, 65, 0);
                                GameObject.Find("num" + n.ToString()).gameObject.transform.Find("Text").GetComponent<Text>().text = "";
                            }
                            for (int n = 0; n < ItemMain.food.Count; n++)
                            {
                                var sp = GameObject.Find(n.ToString()).GetComponent<Button>().image;
                                sp.sprite = Resources.Load<Sprite>("ItemIcon/" + ItemMain.food[n].Id.ToString());
                                sp.color = new Color32(255, 255, 255, 255);

                                GameObject.Find("num" + n.ToString()).GetComponent<Image>().color = new Color32(241, 162, 65, 255);
                                GameObject.Find("num" + n.ToString()).gameObject.transform.Find("Text").GetComponent<Text>().text = ItemMain.food[n].Number.ToString();
                            }
                        }
                    }
                    break;
                case "Pellet":
                    if (num1 < ItemMain.pellet.Count)
                    {
                        effect = ItemMain.pellet[num1].Effect;
                        ItemMain.pellet[num1].Number--;
                        if (ItemMain.pellet[num1].Number > 0)
                        {
                            GameObject.Find("num" + num1.ToString()).gameObject.transform.Find("Text").GetComponent<Text>().text = ItemMain.pellet[num1].Number.ToString();
                        }
                        else
                        {
                            foreach (var belonging in GameRunningData.GetRunningData().belongings)
                            {
                                if (ItemMain.pellet[num1].Name.Equals(belonging.Name))
                                {
                                    GameRunningData.GetRunningData().belongings.Remove(belonging);
                                    break;
                                }
                            }
                            ItemMain.pellet.Remove(ItemMain.pellet[num1]);
                            //清空物品栏

                            for (int n = 0; n < 30; n++)
                            {
                                GameObject.Find(n.ToString()).GetComponent<Button>().image.color = new Color32(255, 255, 255, 0);
                                GameObject.Find("num" + n.ToString()).GetComponent<Image>().color = new Color32(241, 162, 65, 0);
                                GameObject.Find("num" + n.ToString()).gameObject.transform.Find("Text").GetComponent<Text>().text = "";
                            }
                            for (int n = 0; n < ItemMain.pellet.Count; n++)
                            {
                                var sp = GameObject.Find(n.ToString()).GetComponent<Button>().image;
                                sp.sprite = Resources.Load<Sprite>("ItemIcon/" + ItemMain.pellet[n].Id.ToString());
                                sp.color = new Color32(255, 255, 255, 255);

                                GameObject.Find("num" + n.ToString()).GetComponent<Image>().color = new Color32(241, 162, 65, 255);
                                GameObject.Find("num" + n.ToString()).gameObject.transform.Find("Text").GetComponent<Text>().text = ItemMain.pellet[n].Number.ToString();
                            }
                        }

                    }
                    break;
                case "Knife":
                    if (num1 < ItemMain.knife.Count)
                    {
                        if (ItemMain.knife[num1].Name.Equals(ItemMain.equipmentNow))
                        {
                            effect = "  佩戴武器已取下，切换至空手状态 ";
                            tittle = "武器已取下";
                            GameRunningData.GetRunningData().player.EquippedWeapon = new Good();
                            ItemMain.equipmentNow = "无";
                            GameObject.Find("EquipmentValue").GetComponent<TextMesh>().text = ItemMain.equipmentNow;
                        }
                        else
                        {
                            effect = "  佩戴武器已切换至: " + ItemMain.knife[num1].Name;
                            tittle = "武器切换成功";
                            GameRunningData.GetRunningData().player.EquippedWeapon = ItemMain.knife[num1];
                            ItemMain.equipmentNow = ItemMain.knife[num1].Name;
                            GameObject.Find("EquipmentValue").GetComponent<TextMesh>().text = ItemMain.equipmentNow;
                        }
                    }
                    break;
                case "Sword":
                    if (num1 < ItemMain.sword.Count)
                    {
                        if (ItemMain.sword[num1].Name.Equals(ItemMain.equipmentNow))
                        {
                            effect = "  佩戴武器已取下，切换至空手状态 ";
                            tittle = "武器已取下";
                            GameRunningData.GetRunningData().player.EquippedWeapon = new Good();
                            ItemMain.equipmentNow = "无";
                            GameObject.Find("EquipmentValue").GetComponent<TextMesh>().text = ItemMain.equipmentNow;
                        }
                        else
                        {
                            effect = "  佩戴武器已切换至: " + ItemMain.sword[num1].Name;
                            tittle = "武器切换成功";
                            GameRunningData.GetRunningData().player.EquippedWeapon = ItemMain.sword[num1];
                            ItemMain.equipmentNow = ItemMain.sword[num1].Name;
                            GameObject.Find("EquipmentValue").GetComponent<TextMesh>().text = ItemMain.equipmentNow;
                        }
                    }
                    break;
                case "Rod":
                    if (num1 < ItemMain.rod.Count)
                    {
                        if (ItemMain.rod[num1].Name.Equals(ItemMain.equipmentNow))
                        {
                            effect = "  佩戴武器已取下，切换至空手状态 ";
                            tittle = "武器已取下";
                            GameRunningData.GetRunningData().player.EquippedWeapon = new Good();
                            ItemMain.equipmentNow = "无";
                            GameObject.Find("EquipmentValue").GetComponent<TextMesh>().text = ItemMain.equipmentNow;
                        }
                        else
                        {
                            effect = "  佩戴武器已切换至: " + ItemMain.rod[num1].Name;
                            tittle = "武器切换成功";
                            GameRunningData.GetRunningData().player.EquippedWeapon = ItemMain.rod[num1];
                            ItemMain.equipmentNow = ItemMain.rod[num1].Name;
                            GameObject.Find("EquipmentValue").GetComponent<TextMesh>().text = ItemMain.equipmentNow;
                        }
                    }
                    break;
                default:
                    break;
            }

            GameObject.Find("DialogText").GetComponent<Text>().text =effect;
            GameObject.Find("DialogTittle").GetComponent<Text>().text = tittle ;


        });

    }

    IEnumerator Count()
    {
            yield return new WaitForSeconds(1);
            //GameObject.Find("DialogTittle").gameObject.SetActive(false);
            //GameObject.Find("DialogText").gameObject.SetActive(false);
            GameObject.Find("Dialog").gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (ItemMain.dialogState==1)
        {
            StartCoroutine(Count());
            ItemMain.dialogState = 0;
        }
    }
}
