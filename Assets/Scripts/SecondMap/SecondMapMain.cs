using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondMapMain : MonoBehaviour
{
    public GameObject dialogueObject;

    // Start is called before the first frame update
    void Start()
    {
        HideDialogue();
        var key = GameRunningData.GetRunningData().currentPlace.GetPlaceString() + "/" 
            + GameRunningData.GetRunningData().date.GetDateString();
        if (GlobalData.MainConversations.ContainsKey(key))
        {
            StartAConversation(GlobalData.MainConversations[key]);
        }
    }

    void StartAConversation(List<Conversation> conversations)
    {
        ShowDialogue();
        ControlDialogue dialogueScript = dialogueObject.GetComponent<ControlDialogue>();
        dialogueScript.SetConversation(conversations[0]);
    }

    void HideDialogue()
    {
        dialogueObject.SetActive(false);
    }

    void ShowDialogue()
    {
        dialogueObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(1))
        //{
        //    SceneManager.LoadScene("FirstMap");
        //}
    }
}
