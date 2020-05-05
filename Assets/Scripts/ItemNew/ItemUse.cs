using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUse : MonoBehaviour
{
    private Button button;
    private static GameObject dialog;
    private static bool isFirst = true;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        if (isFirst)
        {
            dialog = GameObject.Find("Dialog").gameObject;
            isFirst = false;
        }
        button.onClick.AddListener(() =>
        {
            StopAllCoroutines();
            dialog.GetComponent<RectTransform>().localPosition = new Vector3(30, 20);
            StartCoroutine(Count());

            string ButtonName = button.name;
            int num1 = 0;
            int.TryParse(ButtonName, out num1);

            string type = ItemMain.itemtype;                 //获取物品栏类型
            string effect = "";                               //物品效果
            string tittle = "物品使用成功";

            switch (type)
            {
                case "Alcohol":
                    if (num1 < ItemMain.alcohol.Count)
                    {
                        UseItem(num1, ItemMain.alcohol, out effect);
                    }
                    break;
                case "Food":
                    if (num1 < ItemMain.food.Count)
                    {
                        UseItem(num1, ItemMain.food, out effect);
                    }
                    break;
                case "Pellet":
                    if (num1 < ItemMain.pellet.Count)
                    {

                        UseItem(num1, ItemMain.pellet, out effect);
                    }
                    break;
                case "Knife":
                    if (num1 < ItemMain.knife.Count)
                    {
                        SwapWeapon(ItemMain.knife[num1], out effect, out tittle);
                    }
                    break;
                case "Sword":
                    if (num1 < ItemMain.sword.Count)
                    {
                        SwapWeapon(ItemMain.sword[num1], out effect, out tittle);
                    }
                    break;
                case "Rod":
                    if (num1 < ItemMain.rod.Count)
                    {
                        SwapWeapon(ItemMain.rod[num1], out effect, out tittle);
                    }
                    break;
                default:
                    break;
            }

            GameObject.Find("DialogText").GetComponent<Text>().text = effect;
            GameObject.Find("DialogTittle").GetComponent<Text>().text = tittle;
        });
    }

    void UseItem(int num1, List<Good> items, out string effect)
    {
        effect = items[num1].Effect;
        items[num1].Number--;
        RealUse(items[num1]);
        if (items[num1].Number > 0)
        {
            GameObject.Find("num" + num1.ToString()).gameObject.transform.Find("Text").GetComponent<Text>().text = ItemMain.pellet[num1].Number.ToString();
        }
        else
        {
            foreach (var belonging in GameRunningData.GetRunningData().belongings)
            {
                if (items[num1].Name.Equals(belonging.Name))
                {
                    GameRunningData.GetRunningData().belongings.Remove(belonging);
                    break;
                }
            }
            items.Remove(items[num1]);
            //清空物品栏

            ItemSwitch.ClearGrid(transform.parent);
            for (int n = 0; n < items.Count; n++)
            {
                ItemMain.SetItem(n, transform.parent.Find(n.ToString()).gameObject, items);
            }
        }
    }

    void RealUse(Good item)
    {
        var player = GameRunningData.GetRunningData().player;
        int value = 0;
        switch (item.Type)
        {
            case ItemKind.Alcohol:
                value = (int)(player.BaseData.MP * (item.ResumeValue * 1.0f / 100));
                player.ChangeMP(value, true);
                switch (item.Id)
                {
                    case 0:
                        player.AttackPowerRate += item.AttrValue;
                        break;
                    case 1:
                        value = (int)(player.Defend * (item.AttrValue * 1.0f / 100));
                        player.Defend += value;
                        break;
                    case 2:
                        player.Crit += item.AttrValue;
                        break;
                    case 3:
                        player.Dodge += item.AttrValue;
                        break;
                    case 4:
                        player.Counterattack += item.AttrValue;
                        break;
                }
                break;
            case ItemKind.Food:
                value = (int)(player.BaseData.Energy * (item.ResumeValue * 1.0f / 100));
                player.ChangeEnergy(value, true);
                break;
            case ItemKind.Pellet:
                switch (item.Id)
                {
                    case 63:
                        player.BaseData.Bi += 1;
                        break;
                    case 64:
                        player.BaseData.Gen += 1;
                        break;
                    case 65:
                        player.BaseData.Wu += 1;
                        break;
                    case 66:
                        player.BaseData.Shen += 1;
                        break;
                    case 67:
                        player.BaseData.Jin += 1;
                        break;
                }
                break;
        }
    }

    void SwapWeapon(Good weapon, out string effect, out string tittle)
    {
        if (weapon.Name.Equals(ItemMain.equipmentNow))
        {
            effect = "  佩戴武器已取下，切换至空手状态 ";
            tittle = "武器已取下";
            GameRunningData.GetRunningData().player.EquippedWeapon = null;
            ItemMain.equipmentNow = "无";
            transform.parent.Find("EquipmentValue").GetComponent<Text>().text = ItemMain.equipmentNow;
        }
        else
        {
            effect = "  佩戴武器已切换至: " + weapon.Name;
            tittle = "武器切换成功";
            GameRunningData.GetRunningData().player.EquippedWeapon = weapon;
            ItemMain.equipmentNow = weapon.Name;
            transform.parent.Find("EquipmentValue").GetComponent<Text>().text = ItemMain.equipmentNow;
        }
    }

    IEnumerator Count()
    {
        yield return new WaitForSeconds(1);
        dialog.GetComponent<RectTransform>().localPosition = new Vector3(5200, 20);
    }
}
