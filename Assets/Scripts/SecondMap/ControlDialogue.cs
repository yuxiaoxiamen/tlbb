using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlDialogue : MonoBehaviour
{
    private Image rightHead;
    private Image leftHead;
    private Text rightName;
    private Text leftName;
    private Text content;
    private RectTransform optionPanel;
    private int conversationIndex = 0;
    private bool isClickToDisplayText = false;
    private bool isOneConversationOver = false;
    private List<Conversation> conversations;
    private Action finishAction;
    private List<Conversation> Asides = new List<Conversation>();
    private string[] options;
    private List<Conversation> Justices = new List<Conversation>();
    private List<Conversation> Evils = new List<Conversation>();
    private bool isInChooice = false;
    private string key;
    private RectTransform bgTransform;

    public GameObject optionButtonPrefab;

    public static ControlDialogue instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        bgTransform = transform.Find("bg").GetComponent<RectTransform>();
        rightHead = transform.Find("rightHead").GetComponent<Image>();
        rightName = transform.Find("rightName").GetComponent<Text>();
        leftHead = transform.Find("leftHead").GetComponent<Image>();
        leftName = transform.Find("leftName").GetComponent<Text>();
        content = transform.Find("content").GetComponent<Text>();
        optionPanel = transform.Find("optionPanel").GetComponent<RectTransform>();
        HideOptionPanel();
    }

    public bool CheckMainConversation(Action action)
    {
        key = GameRunningData.GetRunningData().GetPlaceDateKey();
        if (GlobalData.MainConversations.ContainsKey(key))
        {
            var mainConversations = GlobalData.MainConversations[key];
            
            foreach(var mc in GlobalData.MainConversations[key])
            {
                switch (mc.ContentType)
                {
                    case 0:
                        Asides.Add(mc);
                        break;
                    case 1:
                        options = mc.Content.Split(',');
                        break;
                    case 2:
                        Justices.Add(mc);
                        break;
                    case 3:
                        Evils.Add(mc);
                        break;
                }
            }
            if(options != null)
            {
                StartConversation(Asides, () =>
                {
                    ShowOption(action);
                });
            }
            else
            {
                StartConversation(Asides, action);
            }
            return true;
        }
        else
        {
            HideDialogue();
            return false;
        }
    }

    void ShowOption(Action finishA)
    {
        ShowDialogue();
        ShowOptionPanel();
        for(int i = 0; i < options.Length; ++i)
        {
            string option = options[i];
            GameObject optionObject = Instantiate(optionButtonPrefab);
            optionObject.GetComponent<RectTransform>().SetParent(optionPanel);
            optionObject.transform.localScale = Vector3.one;
            optionObject.transform.Find("Text").GetComponent<Text>().text = option;
            optionObject.name = i + "";
            var conflict = GlobalData.MainLineConflicts[key];
            optionObject.GetComponent<Button>().onClick.AddListener(() =>
            {
                int type = int.Parse(optionObject.name);
                if (type == 0)
                {
                    conflict.IsZ = true;
                    StartConversation(Justices, finishA);
                }
                else if(type == 1)
                {
                    conflict.IsZ = false;
                    StartConversation(Evils, finishA);
                }
                HideOptionPanel();
            });
        }
    }

    void HideOptionPanel()
    {
        isInChooice = false;
        optionPanel.gameObject.SetActive(false);
    }

    void ShowOptionPanel()
    {
        isInChooice = true;
        optionPanel.gameObject.SetActive(true);
    }

    public void StartConversation(List<Conversation> cs, Action action)
    {
        finishAction = action;
        ControlBottomPanel.IsBanPane = true;
        conversations = cs;
        ShowDialogue();
        SetOneConversation(conversations[0]);
    }

    public void SetOneConversation(Conversation conversation)
    {
        ShowOneSide(conversation.IsLeft);
        HideOneSide(conversation.IsLeft);
        if(conversation.People != null)
        {
            if (conversation.IsLeft)
            {
                leftHead.sprite = Resources.Load<Sprite>("head/" + conversation.People.BaseData.HeadPortrait);
                leftName.text = conversation.People.BaseData.Name;
                bgTransform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                rightHead.sprite = Resources.Load<Sprite>("head/" + conversation.People.BaseData.HeadPortrait);
                rightName.text = conversation.People.BaseData.Name;
                bgTransform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        StartCoroutine(SetContentText(conversation.Content));
    }

    void ShowOneSide(bool conversationIsLeft)
    {
        if (!conversationIsLeft)
        {
            rightHead.gameObject.SetActive(true);
            rightName.gameObject.SetActive(true);
        }
        else
        {
            leftHead.gameObject.SetActive(true);
            leftName.gameObject.SetActive(true);
        }
    }

    void HideOneSide(bool conversationIsLeft)
    {
        if (conversationIsLeft)
        {
            rightHead.gameObject.SetActive(false);
            rightName.gameObject.SetActive(false);
        }
        else
        {
            leftHead.gameObject.SetActive(false);
            leftName.gameObject.SetActive(false);
        }
    }

    IEnumerator SetContentText(string text)
    {
        text.Replace("{}", GameRunningData.GetRunningData().player.BaseData.Name);
        isOneConversationOver = false;
        for (int i = 1; i < text.Length + 1; ++i)
        {
            if (isClickToDisplayText)
            {
                content.text = text;
                isClickToDisplayText = false;
                break;
            }
           content.text = text.Substring(0, i);
           yield return new WaitForSeconds(0.1f);
        }
        isOneConversationOver = true;
    }

    public void HideDialogue()
    {
        gameObject.SetActive(false);
    }

    public void ShowDialogue()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInChooice)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!isOneConversationOver)
                {
                    isClickToDisplayText = true;
                }
                else
                {
                    ++conversationIndex;
                    if (conversationIndex < conversations.Count)
                    {
                        SetOneConversation(conversations[conversationIndex]);
                    }
                    else
                    {
                        HideDialogue();
                        conversationIndex = 0;
                        finishAction();
                        ControlBottomPanel.IsBanPane = false;
                    }
                }
            }
        }
    }
}
