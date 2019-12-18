using DG.Tweening;
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
    private int conversationIndex = 0;
    private bool isClickToDisplayText = false;
    private bool isConversationOver = false;
    public List<Conversation> Conversations { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        rightHead = transform.Find("rightHead").GetComponent<Image>();
        rightName = transform.Find("rightName").GetComponent<Text>();
        leftHead = transform.Find("leftHead").GetComponent<Image>();
        leftName = transform.Find("leftName").GetComponent<Text>();
        content = transform.Find("content").GetComponent<Text>();
    }

    public void SetConversation(Conversation conversation)
    {
        ShowOneSide(conversation.IsLeft);
        HideOneSide(conversation.IsLeft);
        if (conversation.IsLeft)
        {
            leftHead.sprite = Resources.Load<Sprite>("head/"+conversation.People.BaseData.HeadPortrait);
            leftName.text = conversation.People.BaseData.Name;
        }
        else
        {
            rightHead.sprite = Resources.Load<Sprite>("head/"+conversation.People.BaseData.HeadPortrait);
            rightName.text = conversation.People.BaseData.Name;
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
        isConversationOver = false;
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
        isConversationOver = true;
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
        if (Input.GetMouseButtonDown(0))
        {
            if (!isConversationOver)
            {
                isClickToDisplayText = true;
            }
            else
            {
                ++conversationIndex;
                if (conversationIndex < Conversations.Count)
                {
                    SetConversation(Conversations[conversationIndex]);
                }
                else
                {
                    HideDialogue();
                }
            }
        }
    }
}
