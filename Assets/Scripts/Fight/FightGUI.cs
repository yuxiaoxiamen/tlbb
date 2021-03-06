﻿using DG.Tweening;
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
    public static GameObject failPanel;
    public static GameObject settlementText;

    private static GameObject tabObject;
    public static bool isTabing = false;

    // Start is called before the first frame update
    void Start()
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
        failPanel = GameObject.Find("failPanel");
        failPanel.SetActive(false);
        settlementText = GameObject.Find("settlement");
        settlementText.SetActive(false);
        tabObject = GameObject.Find("Inventory");
        tabObject.SetActive(false);
        ControlDialogue.instance.HideDialogue();
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
        else if(nameSplits[0] == "buff")
        {
            AttackBuff buff = lookingPerson.AttackBuffs[int.Parse(nameSplits[1])];
            text = buff.StyleEffect.Name + Environment.NewLine +
                "剩余回合："+buff.Duration+ Environment.NewLine+
                buff.StyleEffect.Detail;
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
            iconObject.name = "buff_"+i;
            iconObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("buff/"+buff.StyleEffect.Id);
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
        Text nameText = infoPanel.transform.Find("nameBg").Find("nameText").GetComponent<Text>();

        head.GetComponent<Image>().sprite = Resources.Load<Sprite>("head/"+person.BaseData.HeadPortrait);
        nameText.text = person.BaseData.Name;

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
        styles.Sort(delegate (AttackStyle x, AttackStyle y)
        {
            return y.CompareTo(x);
        });
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
            SoundEffectControl.instance.PlaySoundEffect(9);
            AttackTool.instance.CountAttackDistance(FightPersonClick.currentPerson, FightMain.instance.friendQueue);
            AttackTool.instance.ShowAttackDistance();
        });
        restButton.GetComponent<Button>().onClick.AddListener(delegate {
            SoundEffectControl.instance.PlaySoundEffect(9);
            FightMain.OneRoundOver(FightPersonClick.currentPerson);
            FightMain.instance.PlayerFinished();
        });
        switchStyleButton.GetComponent<Button>().onClick.AddListener(SwitchStyleListener);
        switchInnerButton.GetComponent<Button>().onClick.AddListener(SwitchGongListener);
        treatButotn.GetComponent<Button>().onClick.AddListener(delegate {
            SoundEffectControl.instance.PlaySoundEffect(9);
            TreatTool.ShowTreatPersons(FightPersonClick.currentPerson);
        });
        itemButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundEffectControl.instance.PlaySoundEffect(9);
            isTabing = true;
            ItemUse.user = FightPersonClick.currentPerson;
            ItemMain.instance.SetWeapon();
            tabObject.SetActive(true);
            FightGridClick.ClearPathAndRange();
            HideBattlePane();
            FightMain.instance.HideAllHPSplider();
        });
    }

    public static void HideTab()
    {
        isTabing = false;
        tabObject.SetActive(false);
    }


    static GameObject FindButton(string name)
    {
        Transform content = battleControlObject.transform;
        return content.Find(name).gameObject;
    }

    public static void ShowBattlePane(Person person)
    {
        if(battleControlObject != null)
        {
            battleControlObject.SetActive(true);
        }  
    }

    public static void HideBattlePane()
    {
        battleControlObject.SetActive(false);
    }

    void AddButtonInScrollPane (GameObject buttonPrefab, string text, int i, bool isSelected)
    {
        GameObject button = Instantiate(buttonPrefab);
        RectTransform buttonTransform = button.GetComponent<RectTransform>();
        buttonTransform.SetParent(scrollContent.GetComponent<RectTransform>());
        buttonTransform.localPosition = Vector3.zero;
        buttonTransform.localRotation = Quaternion.identity;
        buttonTransform.localScale = Vector3.one;
        button.name = i + "";
        button.transform.Find("Text").GetComponent<Text>().text = text;
        if (isSelected)
        {
            button.GetComponent<Image>().sprite = Resources.Load<Sprite>("ui/selected");
        }
    }

    void FillAttackStyle()
    {
        List<AttackStyle> styles = FightPersonClick.currentPerson.BaseData.AttackStyles;
        for (int i = 0; i < styles.Count; ++i)
        {
            bool isSelected = false;
            if(styles[i] == FightPersonClick.currentPerson.SelectedAttackStyle)
            {
                isSelected = true;
            }
            if (styles[i].FixData.WeaponKind == AttackWeaponKind.Finger ||
                    styles[i].FixData.WeaponKind == AttackWeaponKind.Palm ||
                    styles[i].FixData.WeaponKind == AttackWeaponKind.Fist)
            {
                AddButtonInScrollPane(styleButtonPrefab, GetStyleText(styles[i]), i, isSelected);
                continue;
            }
            if (FightPersonClick.currentPerson.EquippedWeapon != null)
            {
                switch (FightPersonClick.currentPerson.EquippedWeapon.Type)
                {
                    case ItemKind.Sword:
                        if (styles[i].FixData.WeaponKind == AttackWeaponKind.Sword)
                        {
                            AddButtonInScrollPane(styleButtonPrefab, GetStyleText(styles[i]), i, isSelected);
                        }
                        break;
                    case ItemKind.Knife:
                        if (styles[i].FixData.WeaponKind == AttackWeaponKind.Knife)
                        {
                            AddButtonInScrollPane(styleButtonPrefab, GetStyleText(styles[i]), i, isSelected);
                        }
                        break;
                    case ItemKind.Rod:
                        if (styles[i].FixData.WeaponKind == AttackWeaponKind.Rod)
                        {
                            AddButtonInScrollPane(styleButtonPrefab, GetStyleText(styles[i]), i, isSelected);
                        }
                        break;
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
        string text = style.FixData.Name + System.Environment.NewLine + "攻击强度" + style.GetRealBasePower() +"  内力消耗"+ style.GetRealMPCost() + System.Environment.NewLine +
            effect + style.FixData.DetailInfo;
        return text;
    }

    void FillInnerGong()
    {
        List<InnerGong> gongs = FightPersonClick.currentPerson.BaseData.InnerGongs;
        for (int i = 0; i < gongs.Count; ++i)
        {
            bool isSelected = false;
            if (gongs[i] == FightPersonClick.currentPerson.SelectedInnerGong)
            {
                isSelected = true;
            }
            AddButtonInScrollPane(gongButtonPrefab, GetGongText(gongs[i]), i, isSelected);
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
        SoundEffectControl.instance.PlaySoundEffect(9);
        ClearScrollPane();
        FightGridClick.ClearPathAndRange();
        isSwitching = true;
        scrollPanel.SetActive(true);
        FillAttackStyle();
        HideBattlePane();
    }

    public void SwitchGongListener()
    {
        SoundEffectControl.instance.PlaySoundEffect(9);
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
        SoundEffectControl.instance.PlaySoundEffect(7);
        successPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        StartEndConversation(true);
    }

    public static IEnumerator ShowFailPanel()
    {
        SoundEffectControl.instance.PlaySoundEffect(8);
        failPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        StartEndConversation(false);
    }

    private static void StartEndConversation(bool isSuccess)
    {
        if (FightMain.source == FightSource.MainLine)
        {
            if (GameRunningData.GetRunningData().isFinal)
            {
                FinalMain.isSuccess = isSuccess;
                SceneManager.LoadScene("Final");
                return;
            }
            var key = GameRunningData.GetRunningData().GetPlaceDateKey();
            var conflict = GlobalData.MainLineConflicts[key];
            var conversations = new List<Conversation>();
            if (conflict.IsZ)
            {
                if (isSuccess)
                {
                    conversations = GetConversations(4);
                }
                else
                {
                    conversations = GetConversations(5);
                }

            }
            else
            {
                if (isSuccess)
                {
                    conversations = GetConversations(6);
                }
                else
                {
                    conversations = GetConversations(7);
                }
            }
            ControlDialogue.instance.StartConversation(conversations, () =>
            {
                EndFight();
            });
        }
        else if (FightMain.source == FightSource.Contest)
        {
            if (isSuccess)
            {
                List<Conversation> contestConversation = new List<Conversation>
            {
                new Conversation()
                {
                    People = FightMain.contestEnemy,
                    Content = "阁下武艺高强，在下佩服",
                    IsLeft = false
                }
            };
                ControlDialogue.instance.StartConversation(contestConversation, () =>
                {
                    FightMain.contestEnemy.ChangeLikability(20, true);
                    foreach(AttackStyle style in FightMain.contestEnemy.BaseData.AttackStyles)
                    {
                        style.Rank += 2;
                        style.Proficiency = 0;
                        if(style.Rank >= 10)
                        {
                            style.Rank = 10;
                        }
                    }
                    EndFight();
                });
            }
            else
            {
                EndFight();
            }
        }
        else
        {
            EndFight();
        }
    }

    private static void EndFight()
    {
        ItemMain.ClearItems();
        if(FightMain.source != FightSource.Encounter)
        {
            TimeGoSubject.GetTimeSubject().UpdateTime(1);
        }
        GameRunningData.GetRunningData().ReturnToMap();
    }

    private static List<Conversation> GetConversations(int type)
    {
        List<Conversation> conversations = new List<Conversation>();
        var key = GameRunningData.GetRunningData().GetPlaceDateKey();
        foreach (var mc in GlobalData.MainConversations[key])
        {
            if(mc.ContentType == type)
            {
                conversations.Add(mc);
            }
        }
        return conversations;
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
