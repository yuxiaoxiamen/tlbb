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

    // Start is called before the first frame update
    void Start()
    {
        scrollPanel = GameObject.Find("Scroll View");
        scrollPanel.SetActive(false);
        SetButtonListener();
    }

    

    public static void SetBattleControlPanel()
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
        Transform content = battleControlObject.gameObject.transform;
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
            var style = styles[i];
            string effect = "";
            foreach (var e in style.FixData.Effects)
            {
                effect += e.Name + "：" + e.Detail + System.Environment.NewLine;
            }
            string text = style.FixData.Name + System.Environment.NewLine + "攻击强度" + style.GetRealBasePower() + System.Environment.NewLine +
                effect + style.FixData.DetailInfo;
            AddButtonInScrollPane(styleButtonPrefab, text, i);
        }
    }

    void FillInnerGong()
    {
        List<InnerGong> gongs = FightPersonClick.currentPerson.BaseData.InnerGongs;
        for (int i = 0; i < gongs.Count; ++i)
        {
            var gong = gongs[i];
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
            AddButtonInScrollPane(gongButtonPrefab, text, i);
        }
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
