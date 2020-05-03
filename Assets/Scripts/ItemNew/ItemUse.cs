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
                    }
                    break;
                case "Food":
                    if (num1 < ItemMain.food.Count)
                    {
                        effect = ItemMain.food[num1].Effect;

                    }
                    break;
                case "Pellet":
                    if (num1 < ItemMain.pellet.Count)
                    {
                        effect = ItemMain.pellet[num1].Effect;

                    }
                    break;
                case "Knife":
                    if (num1 < ItemMain.knife.Count)
                    {
                        effect = "  佩戴武器已切换至: "+ItemMain.knife[num1].Name;
                        tittle = "武器切换成功";
                        GameRunningData.GetRunningData().player.EquippedWeapon = ItemMain.knife[num1];
                        ItemMain.equipmentNow = ItemMain.knife[num1].Name;
                        GameObject.Find("EquipmentValue").GetComponent<TextMesh>().text = ItemMain.equipmentNow;

                    }
                    break;
                case "Sword":
                    if (num1 < ItemMain.sword.Count)
                    {
                        effect = "  佩戴武器已切换至: " + ItemMain.sword[num1].Name;
                        tittle = "武器切换成功";
                        GameRunningData.GetRunningData().player.EquippedWeapon = ItemMain.sword[num1];
                        ItemMain.equipmentNow = ItemMain.sword[num1].Name;
                        GameObject.Find("EquipmentValue").GetComponent<TextMesh>().text = ItemMain.equipmentNow;
                    }
                    break;
                case "Rod":
                    if (num1 < ItemMain.rod.Count)
                    {
                        effect = "  佩戴武器已切换至: " + ItemMain.rod[num1].Name;
                        tittle = "武器切换成功";
                        GameRunningData.GetRunningData().player.EquippedWeapon = ItemMain.rod[num1];
                        ItemMain.equipmentNow = ItemMain.rod[num1].Name;
                        GameObject.Find("EquipmentValue").GetComponent<TextMesh>().text = ItemMain.equipmentNow;
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
            yield return new WaitForSeconds(2);
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
