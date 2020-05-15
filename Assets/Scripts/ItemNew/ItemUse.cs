using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUse : MonoBehaviour
{
    private Button button;
    private GameObject dialog;
    public static Person user;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        dialog = GameObject.Find("Dialog").gameObject;
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
                        SoundEffectControl.instance.PlaySoundEffect(1);
                        UseItem(num1, ItemMain.alcohol, out effect);
                    }
                    break;
                case "Food":
                    if (num1 < ItemMain.food.Count)
                    {
                        SoundEffectControl.instance.PlaySoundEffect(0);
                        UseItem(num1, ItemMain.food, out effect);
                    }
                    break;
                case "Pellet":
                    if (num1 < ItemMain.pellet.Count)
                    {
                        SoundEffectControl.instance.PlaySoundEffect(2);
                        UseItem(num1, ItemMain.pellet, out effect);
                    }
                    break;
                case "Knife":
                    if (num1 < ItemMain.knife.Count)
                    {
                        UseWeapon(num1, ItemMain.knife, out effect, out tittle);
                    }
                    break;
                case "Sword":
                    if (num1 < ItemMain.sword.Count)
                    {
                        UseWeapon(num1, ItemMain.sword, out effect, out tittle);
                    }
                    break;
                case "Rod":
                    if (num1 < ItemMain.rod.Count)
                    {
                        UseWeapon(num1, ItemMain.rod, out effect, out tittle);
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
            transform.Find("num" + num1.ToString()).gameObject.transform.Find("Text")
            .GetComponent<Text>().text = items[num1].Number.ToString();
        }
        else
        {
            //foreach (var belonging in GameRunningData.GetRunningData().belongings)
            //{
            //    if (items[num1].Name.Equals(belonging.Name))
            //    {
            //        GameRunningData.GetRunningData().belongings.Remove(belonging);
            //        break;
            //    }
            //}
            GameRunningData.GetRunningData().belongings.Remove(items[num1]);
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
        int value = 0;
        switch (item.Type)
        {
            case ItemKind.Alcohol:
                value = (int)(user.BaseData.MP * (item.ResumeValue * 1.0f / 100));
                value = (int)(value * (1 + user.BaseData.LiquorSkill * 0.01f));
                user.ChangeMP(value, true);
                switch (item.Id)
                {
                    case 0:
                        user.AttackPowerRate += item.AttrValue;
                        break;
                    case 1:
                        value = (int)(user.Defend * (item.AttrValue * 1.0f / 100));
                        user.Defend += value;
                        break;
                    case 2:
                        user.Crit += item.AttrValue;
                        break;
                    case 3:
                        user.Dodge += item.AttrValue;
                        break;
                    case 4:
                        user.Counterattack += item.AttrValue;
                        break;
                }
                break;
            case ItemKind.Food:
                value = (int)(user.BaseData.Energy * (item.ResumeValue * 1.0f / 100));
                value = (int)(value * (1 + user.BaseData.CookingSkill * 0.01f));
                user.ChangeEnergy(value, true);
                break;
            case ItemKind.Pellet:
                switch (item.Id)
                {
                    case 63:
                        user.BaseData.Bi += 1;
                        break;
                    case 64:
                        user.BaseData.Gen += 1;
                        break;
                    case 65:
                        user.BaseData.Wu += 1;
                        break;
                    case 66:
                        user.BaseData.Shen += 1;
                        break;
                    case 67:
                        user.BaseData.Jin += 1;
                        break;
                }
                break;
        }
    }

    void UseWeapon(int num1, List<Good> items, out string effect, out string tittle)
    {
        SoundEffectControl.instance.PlaySoundEffect(3);
        if(user.EquippedWeapon == null)
        {
            if (user == GameRunningData.GetRunningData().player)
            {
                SwapWeapon(num1, items, out effect, out tittle);
            }
            else
            {
                effect = "  人物不可以佩戴该类型的武器";
                tittle = "武器切换失败";
            }
        }
        else
        {
            if (user.EquippedWeapon.Type == items[num1].Type)
            {
                SwapWeapon(num1, items, out effect, out tittle);
            }
            else
            {
                if (user == GameRunningData.GetRunningData().player)
                {
                    SwapWeapon(num1, items, out effect, out tittle);
                }
                else
                {
                    effect = "  人物不可以佩戴该类型的武器";
                    tittle = "武器切换失败";
                }
            }
        }
        ItemSwitch.ClearGrid(transform.parent);
        for (int n = 0; n < items.Count; n++)
        {
            ItemMain.SetItem(n, transform.parent.Find(n.ToString()).gameObject, items);
        }
    }

    void SwapWeapon(int num1, List<Good> items, out string effect, out string tittle)
    {
        effect = "  佩戴武器已切换至: " + items[num1].Name;
        tittle = "武器切换成功";
        transform.parent.Find("EquipmentValue").GetComponent<Text>().text = items[num1].Name;
        items[num1].Number--;
        if (user.EquippedWeapon != null)
        {
            GameRunningData.GetRunningData().AddItem(user.EquippedWeapon);
            switch (user.EquippedWeapon.Type)
            {
                case ItemKind.Knife:
                    if (!ItemMain.knife.Contains(user.EquippedWeapon))
                    {
                        ItemMain.knife.Add(user.EquippedWeapon);
                    }
                    break;
                case ItemKind.Sword:
                    if (!ItemMain.sword.Contains(user.EquippedWeapon))
                    {
                        ItemMain.sword.Add(user.EquippedWeapon);
                    }
                    break;
                case ItemKind.Rod:
                    if (!ItemMain.rod.Contains(user.EquippedWeapon))
                    {
                        ItemMain.rod.Add(user.EquippedWeapon);
                    }
                    break;
            }
        }
        user.EquippedWeapon = items[num1];
        if (items[num1].Number > 0)
        {
            transform.Find("num" + num1.ToString()).gameObject.transform.Find("Text")
            .GetComponent<Text>().text = items[num1].Number.ToString();
        }
        else
        {
            GameRunningData.GetRunningData().belongings.Remove(items[num1]);
            items.Remove(items[num1]);
        }
    }

    IEnumerator Count()
    {
        yield return new WaitForSeconds(1);
        dialog.GetComponent<RectTransform>().localPosition = new Vector3(5200, 20);
    }
}
