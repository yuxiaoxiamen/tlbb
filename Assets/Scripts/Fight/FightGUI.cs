using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FightGUI : MonoBehaviour
{
    private static GameObject attackButton;
    private static GameObject treatButotn;
    private static GameObject itemButton;
    private static GameObject switchStyleButton;
    private static GameObject switchInnerButton;
    private static GameObject restButton;
    public static GameObject battleControlObject;

    private static GameObject scrollPanel;
    public GameObject styleButtonPrefab;
    public GameObject gongButtonPrefab;
    public GameObject scrollContent;
    public static bool isSwitching = false;

    private static GameObject infoPanel;
    private static GameObject detailPanel;
    public static Person lookingPerson;
    public static bool isLookingInfo = false;
    public static GameObject buffIconPrefab;
    public GameObject iconPrefab;
    public static GameObject buffPanel;

    public static GameObject successPanel;
    public static GameObject settlementText;

    // Start is called before the first frame update
    void Awake()
    {
        scrollPanel = GameObject.Find("scrollPanel");
        scrollPanel.SetActive(false);
        infoPanel = GameObject.Find("infoPanel");
        buffPanel = infoPanel.transform.Find("buffPanel").gameObject;
        infoPanel.SetActive(false);
        detailPanel = GameObject.Find("detailPanel");
        detailPanel.SetActive(false);
        successPanel = GameObject.Find("successPanel");
        successPanel.SetActive(false);
        settlementText = GameObject.Find("settlement");
        settlementText.SetActive(false);
        SetBattleControlPanel();
        SetButtonListener();
        buffIconPrefab = iconPrefab;
    }

    public static void SetDetailPanel(string name)
    {
        detailPanel.SetActive(true);
        Text detailText = detailPanel.transform.Find("text").GetComponent<Text>();
        string text = "";
        string[] nameSplits = name.Split('_');
        if(nameSplits[0] == "style")
        {
            text = GetStyleText(lookingPerson.BaseData.AttackStyles[int.Parse(nameSplits[1])]);
        }
        else
        {
            text = GetGongText(lookingPerson.SelectedInnerGong);
        }
        detailText.text = text;
    }

    public static void HideDetailPanel()
    {
        detailPanel.SetActive(false);
    }

    public static void AddBuffIcon(Person person)
    {
        for (int i = 0; i < person.AttackBuffs.Count; ++i)
        {
            AttackBuff buff = person.AttackBuffs[i];
            GameObject iconObject = Instantiate(buffIconPrefab);
            RectTransform rectTransform = iconObject.GetComponent<RectTransform>();
            rectTransform.SetParent(buffPanel.GetComponent<RectTransform>());
            rectTransform.localPosition = Vector3.zero;
            rectTransform.localRotation = Quaternion.identity;
            rectTransform.localScale = Vector3.one;
            iconObject.name = i+"";
        }
    }

    private static void DestoryBuffIcon()
    {
        for (int i = 0; i < buffPanel.transform.childCount; ++i)
        {
            Transform child = buffPanel.transform.GetChild(i);
            if(child.tag == "Buff")
            {
                Destroy(child.gameObject);
            }
        }
    }

    public static void SetInfoPanel(Person person)
    {
        infoPanel.SetActive(true);
        DestoryBuffIcon();
        AddBuffIcon(person);
        isLookingInfo = true;
        lookingPerson = person;
        Transform head = infoPanel.transform.Find("head");
        Transform attributeTexts = infoPanel.transform.Find("sx");
        Transform gongText = infoPanel.transform.Find("ng").GetChild(0);
        Transform styleTexts = infoPanel.transform.Find("zs");
        Button exitButton = infoPanel.transform.Find("exitButton").GetComponent<Button>();

        exitButton.onClick.AddListener(() =>
        {
            HideInfoPanel();
        });

        head.GetComponent<Image>().sprite = Resources.Load<Sprite>("head/"+person.BaseData.HeadPortrait);

        Transform hpTransform = attributeTexts.Find("hp");
        Transform mpTransform = attributeTexts.Find("mp");
        Transform bjTransform = attributeTexts.Find("bj");
        Transform fjTransform = attributeTexts.Find("fj");
        Transform sbTransform = attributeTexts.Find("sb");
        Transform fyTransform = attributeTexts.Find("fy");
        Transform mzTransform = attributeTexts.Find("mz");
        Transform ydTransform = attributeTexts.Find("yd");

        hpTransform.Find("value").GetComponent<Text>().text = person.CurrentHP + "/" + person.BaseData.HP;
        mpTransform.Find("value").GetComponent<Text>().text = person.CurrentMP + "/" + person.BaseData.MP;
        bjTransform.Find("value").GetComponent<Text>().text = person.Crit + "";
        fjTransform.Find("value").GetComponent<Text>().text = person.Counterattack + "";
        sbTransform.Find("value").GetComponent<Text>().text = person.Dodge + "";
        fyTransform.Find("value").GetComponent<Text>().text = person.Defend + "";
        mzTransform.Find("value").GetComponent<Text>().text = person.Accuracy + "";
        ydTransform.Find("value").GetComponent<Text>().text = person.MoveRank + "";

        gongText.GetComponent<Text>().text = person.SelectedInnerGong.FixData.Name;

        List<AttackStyle> styles = person.BaseData.AttackStyles;
        int max = 4;
        int count = Mathf.Min(max, styles.Count);
        for(int i = 0; i < count; ++i)
        {
            GameObject styleTextObject = styleTexts.GetChild(i).gameObject;
            styleTextObject.SetActive(true);
            styleTextObject.GetComponent<Text>().text = styles[i].FixData.Name;
            styleTextObject.name = "style_" + i + "";
        }
        if(count < max)
        {
            for(int i = count; i < max; ++i)
            {
                styleTexts.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public static void HideInfoPanel()
    {
        infoPanel.SetActive(false);
    }

    private void SetBattleControlPanel()
    {
        battleControlObject = GameObject.Find("battleControlPanel");
        battleControlObject.SetActive(false);
        attackButton = FindButton("attackButton");
        treatButotn = FindButton("treatButton");
        itemButton = FindButton("itemButton");
        switchStyleButton = FindButton("switchStyleButton");
        switchInnerButton = FindButton("switchInnerButton");
        restButton = FindButton("restButton");
    }

    public void SetButtonListener()
    {
        attackButton.GetComponent<Button>().onClick.AddListener(delegate {
            AttackTool.CountAttackDistance(FightPersonClick.currentPerson, FightMain.friendQueue);
            AttackTool.ShowAttackDistance();
        });
        restButton.GetComponent<Button>().onClick.AddListener(delegate {
            FightMain.OneRoundOver(FightPersonClick.currentPerson);
            FightMain.PlayerFinished();
        });
        switchStyleButton.GetComponent<Button>().onClick.AddListener(SwitchStyleListener);
        switchInnerButton.GetComponent<Button>().onClick.AddListener(SwitchGongListener);
        treatButotn.GetComponent<Button>().onClick.AddListener(delegate {
            TreatTool.ShowTreatPersons(FightPersonClick.currentPerson);
        });
    }


    static GameObject FindButton(string name)
    {
        Transform content = battleControlObject.transform;
        return content.Find(name).gameObject;
    }

    public static void ShowBattlePane(Person person)
    {
        battleControlObject.SetActive(true);

        switch (person.ControlState)
        {
            case BattleControlState.Moving:
            case BattleControlState.Moved:
                attackButton.SetActive(true);
                itemButton.SetActive(true);
                switchStyleButton.SetActive(true);
                restButton.SetActive(true);
                break;
            case BattleControlState.End:
                attackButton.SetActive(false);
                itemButton.SetActive(false);
                switchStyleButton.SetActive(false);
                restButton.SetActive(true);
                break;
        }
    }

    public static void HideBattlePane()
    {
        battleControlObject.SetActive(false);
    }

    void AddButtonInScrollPane (GameObject buttonPrefab, string text, int i)
    {
        GameObject button = Instantiate(buttonPrefab);
        RectTransform buttonTransform = button.GetComponent<RectTransform>();
        buttonTransform.SetParent(scrollContent.GetComponent<RectTransform>());
        buttonTransform.localPosition = Vector3.zero;
        buttonTransform.localRotation = Quaternion.identity;
        buttonTransform.localScale = Vector3.one;
        button.name = i + "";
        button.transform.Find("Text").GetComponent<Text>().text = text;
    }

    void FillAttackStyle()
    {
        List<AttackStyle> styles = FightPersonClick.currentPerson.BaseData.AttackStyles;
        for (int i = 0; i < styles.Count; ++i)
        {
            if(FightPersonClick.currentPerson.EquippedWeapon != null)
            {
                switch (FightPersonClick.currentPerson.EquippedWeapon.Type)
                {
                    case ItemKind.Sword:
                        if (styles[i].FixData.WeaponKind == AttackWeaponKind.Sword)
                        {
                            AddButtonInScrollPane(styleButtonPrefab, GetStyleText(styles[i]), i);
                        }
                        break;
                    case ItemKind.Knife:
                        if (styles[i].FixData.WeaponKind == AttackWeaponKind.Knife)
                        {
                            AddButtonInScrollPane(styleButtonPrefab, GetStyleText(styles[i]), i);
                        }
                        break;
                    case ItemKind.Rod:
                        if (styles[i].FixData.WeaponKind == AttackWeaponKind.Rod)
                        {
                            AddButtonInScrollPane(styleButtonPrefab, GetStyleText(styles[i]), i);
                        }
                        break;
                }
            }
            else
            {
                if (styles[i].FixData.WeaponKind == AttackWeaponKind.Finger ||
                    styles[i].FixData.WeaponKind == AttackWeaponKind.Palm ||
                    styles[i].FixData.WeaponKind == AttackWeaponKind.Fist)
                {
                    AddButtonInScrollPane(styleButtonPrefab, GetStyleText(styles[i]), i);
                }
            }
        }
    }

    static string GetStyleText(AttackStyle style)
    {
        string effect = "";
        foreach (var e in style.FixData.Effects)
        {
            effect += e.Name + "：" + e.Detail + System.Environment.NewLine;
        }
        string text = style.FixData.Name + System.Environment.NewLine + "攻击强度" + style.GetRealBasePower() + System.Environment.NewLine +
            effect + style.FixData.DetailInfo;
        return text;
    }

    void FillInnerGong()
    {
        List<InnerGong> gongs = FightPersonClick.currentPerson.BaseData.InnerGongs;
        for (int i = 0; i < gongs.Count; ++i)
        {
            AddButtonInScrollPane(gongButtonPrefab, GetGongText(gongs[i]), i);
        }
    }

    static string GetGongText(InnerGong gong)
    {
        InnerGongFixData gongFixData = gong.FixData;
        string effect = "";
        if (!gongFixData.FirstEffect.Equals("无") && gong.Rank >= 1)
        {
            effect += gongFixData.FirstEffect + System.Environment.NewLine;
        }
        if (!gongFixData.SixthEffect.Equals("无") && gong.Rank >= 6)
        {
            effect += gongFixData.SixthEffect + System.Environment.NewLine;
        }
        if (!gongFixData.TenthEffect.Equals("无") && gong.Rank >= 10)
        {
            effect += gongFixData.TenthEffect + System.Environment.NewLine;
        }
        string text = gongFixData.Name + System.Environment.NewLine + gongFixData.DefaultEffect + System.Environment.NewLine + effect;
        return text;
    }

    private void ClearScrollPane()
    {
        for (int i = 0; i < scrollContent.transform.childCount; i++)
        {
            Destroy(scrollContent.transform.GetChild(i).gameObject);
        }
    }

    public void SwitchStyleListener()
    {
        ClearScrollPane();
        FightGridClick.ClearPathAndRange();
        isSwitching = true;
        scrollPanel.SetActive(true);
        FillAttackStyle();
        HideBattlePane();
    }

    public void SwitchGongListener()
    {
        ClearScrollPane();
        FightGridClick.ClearPathAndRange();
        isSwitching = true;
        scrollPanel.SetActive(true);
        FillInnerGong();
        HideBattlePane();
    }

    public static void HideScrollPane()
    {
        scrollPanel.SetActive(false);
        isSwitching = false;
    }

    public static IEnumerator ShowSuccessPanel()
    {
        successPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        GameRunningData.GetRunningData().date.GoByTime(100);
        GameRunningData.GetRunningData().ReturnToMap();
    }

    public static void SetSettlement(List<Good> rewards, int experience, int money)
    {
        settlementText.SetActive(true);
        string text = "江湖阅历增长 " + experience + Environment.NewLine;
        text += "获得金钱 " + money + Environment.NewLine;
        foreach (Good good in rewards)
        {
            text += "获得物品  " + good.Name + Environment.NewLine;
        }
        settlementText.GetComponent<Text>().text = text;
    }
}
