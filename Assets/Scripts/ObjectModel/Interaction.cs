using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interaction
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void InteractionListener(Person person)
    {
        InteractControl.instance.HideInterPanel();
        //ThridMapMain.ShowAllHeads();
        ThridMapMain.HidePeople();
        switch (Id)
        {
            case 0: //交谈
                ChatListener(person);
                break;
            case 1: //切磋
                FightMain.source = FightSource.Contest;
                FightMain.contestEnemy = person;
                SceneManager.LoadScene("Fight");
                break;
            case 2: //交易
                break;
            case 3: //锻造
                ManualMain.instance.SetManual(false);
                ThridMapMain.manualUI.SetActive(true);
                break; 
            case 4: //炼丹
                SceneManager.LoadScene("Alchemy");
                break;
            case 5: //治疗
                break;
            case 6: //休息
                break;
            case 7: //喝酒
                break;
            case 8: //学医
                break;
            case 9: //下厨
                ManualMain.instance.SetManual(true);
                ThridMapMain.manualUI.SetActive(true);
                break;
            case 10: //招募
                GameRunningData.GetRunningData().teammates.Add(person);
                ThridMapMain.ShowAllHeads();
                break;
            case 11: //离开
                ThridMapMain.ShowAllHeads();
                break;
        }
    }

    void ChatListener(Person person)
    {
        List<Conversation> conversations = new List<Conversation>();
        if(person.BaseData.ChatConversations != null)
        {
            foreach (var cc in person.BaseData.ChatConversations)
            {
                conversations.Add(cc);
            }
            ControlDialogue.instance.StartConversation(conversations, () =>
            {
                ThridMapMain.ShowAllHeads();
            });
        }
        else
        {
            ThridMapMain.ShowAllHeads();
        }
    }
}
