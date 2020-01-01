using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private static Person lookingPerson;
    public static bool isLookingInfo = false;

    // Start is called before the first frame update
    void Awake()
    {
        scrollPanel = GameObject.Find("scrollPanel");
        scrollPanel.SetActive(false);
        infoPanel = GameObject.Find("infoPanel");
        infoPanel.SetActive(false);
        detailPanel = GameObject.Find("detailPanel");
        detailPanel.SetActive(false);
        SetBattleControlPanel();
        SetButtonListener();
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

    public static void SetInfoPanel(Person person)
    {
        infoPanel.SetActive(true);
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
        Transform ydTransform = attributeTexts.Find("yd");

        hpTransform.Find("value").GetComponent<Text>().text = person.CurrentHP + "/" + person.BaseData.HP;
        mpTransform.Find("value").GetComponent<Text>().text = person.CurrentMP + "/" + person.BaseData.MP;
        bjTransform.Find("value").GetComponent<Text>().text = person.Crit + "";
        fjTransform.Find("value").GetComponent<Text>().text = person.Counterattack + "";
        sbTransform.Find("value").GetComponent<Text>().text = person.Dodge + "";
        fyTransform.Find("value").GetComponent<Text>().text = person.Defend + "";
        ydTransform.Find("value").GetComponent<Text>().text = person.MoveRank + "";

        gongText.GetComponent<Text>().text = person.SelectedInnerGong.Name;

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
        restButton.GetComponent<Button>().onClick.AddListener(FightMain.PlayerFinished);
        switchStyleButton.GetComponent<Button>().onClick.AddListener(SwitchStyleListener);
        switchInnerButton.GetComponent<Button>().onClick.AddListener(SwitchGongListener);
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
            AddButtonInScrollPane(styleButtonPrefab, GetStyleText(styles[i]), i);
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
        string effect = "";
        if (!gong.FirstEffect.Equals("无"))
        {
            effect += gong.FirstEffect + System.Environment.NewLine;
        }
        if (!gong.SixthEffect.Equals("无"))
        {
            effect += gong.SixthEffect + System.Environment.NewLine;
        }
        if (!gong.TenthEffect.Equals("无"))
        {
            effect += gong.TenthEffect + System.Environment.NewLine;
        }
        string text = gong.Name + System.Environment.NewLine + gong.DefaultEffect + System.Environment.NewLine + effect;
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
}
