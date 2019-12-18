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
        content.text = conversation.Content;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
