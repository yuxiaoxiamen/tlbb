using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManualMain : MonoBehaviour
{
    public static bool isFood;
    public RectTransform scrollContent;
    public GameObject manualItemPrefab;
    public static ManualMain instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void SetManual(bool isfood)
    {
        isFood = isfood;
        SetTitle();
        SetTip();
        SetReturnButton();
        for(int j = 0; j < scrollContent.childCount; ++j)
        {
            Destroy(scrollContent.GetChild(j).gameObject);
        }
        int i = 0;
        if (isFood)
        {
            foreach (FoodManual foodManual in GlobalData.FoodManuals)
            {
                SetManualItem(foodManual, i);
                ++i;
            }
        }
        else
        {
            foreach (WeaponManual weaponManual in GlobalData.WeaponManuals)
            {
                SetManualItem(weaponManual, i);
                ++i;
            }
        }
    }

    void SetManualItem(Manual manual, int index)
    {
        RectTransform manualItemTransform = Instantiate(manualItemPrefab).GetComponent<RectTransform>();
        manualItemTransform.SetParent(scrollContent);
        manualItemTransform.localPosition = Vector3.zero;
        manualItemTransform.localRotation = Quaternion.identity;
        manualItemTransform.localScale = Vector3.one;
        manualItemTransform.name = index + "";
        SetText(manualItemTransform, manual);
    }

    void SetText(RectTransform manualItemTransform, Manual manual)
    {
        Text nameText = manualItemTransform.Find("name").GetComponent<Text>();
        Text numberText1 = manualItemTransform.Find("number1").GetComponent<Text>();
        Text numberText2 = manualItemTransform.Find("number2").GetComponent<Text>();
        Text numberText3 = manualItemTransform.Find("number3").GetComponent<Text>();
        Text numberText4 = manualItemTransform.Find("number4").GetComponent<Text>();
        Text numberText5 = manualItemTransform.Find("number5").GetComponent<Text>();
        nameText.text = manual.Item.Name;
        numberText5.text = manual.Price + "钱";
        
        if (isFood)
        {
            FoodManual foodManual = (FoodManual)manual;
            numberText1.text = foodManual.FruitNum + "";
            numberText2.text = foodManual.VegetableNum + "";
            numberText3.text = foodManual.MeatNum + "";
            numberText4.text = "";
        }
        else
        {
            WeaponManual WeaponManual = (WeaponManual)manual;
            numberText1.text = WeaponManual.CopperNumber + "";
            numberText2.text = WeaponManual.IronNumber + "";
            numberText3.text = WeaponManual.SilverNumber + "";
            numberText4.text = WeaponManual.GoldNumber + "";
        }
    }

    void SetTitle()
    {
        Transform titlePanel = transform.Find("titlePanel");
        Text titleText1 = titlePanel.Find("title1").GetComponent<Text>();
        Text titleText2 = titlePanel.Find("title2").GetComponent<Text>();
        Text titleText3 = titlePanel.Find("title3").GetComponent<Text>();
        Text titleText4 = titlePanel.Find("title4").GetComponent<Text>();
        if (isFood)
        {
            titleText1.text = "水果";
            titleText2.text = "蔬菜";
            titleText3.text = "肉类";
            titleText4.text = "";
        }
        else
        {
            titleText1.text = "铜";
            titleText2.text = "铁";
            titleText3.text = "银";
            titleText4.text = "金";
        }
    }

    void SetTip()
    {
        Text tip = transform.Find("tip").GetComponent<Text>();
        if (isFood)
        {
            tip.text = "选择要烹饪的食物，不同的食物需要的食材不同";
        }
        else
        {
            tip.text = "选择需要打造的武器，不同的武器需要的矿石不同";
        }
    }

    void SetReturnButton()
    {
        Button button = transform.Find("returnButton").GetComponent<Button>();
        button.onClick.AddListener(()=>
        {
            ControlBottomPanel.IsBanPane = false;
            gameObject.SetActive(false);
            ThridMapMain.ShowAllHeads();
        });
    }
}
