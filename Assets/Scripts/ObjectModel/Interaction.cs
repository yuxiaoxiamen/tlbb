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
        Random.InitState((int)System.DateTime.Now.Ticks);
        int x = Random.Range(1, 101);
        if (x <= 100)
        {
            List<GameDate> times = TimeGoSubject.GetTimeSubject().SortMainLine();
            int count = 0;
            List<string> titles = new List<string>();
            foreach (var time in times)
            {
                if (GameRunningData.GetRunningData().date.CompareTo(time) <= 0)
                {
                    foreach (var key in GlobalData.MainLineConflicts.Keys)
                    {
                        var dateString = key.Split('/')[1];
                        if (time.GetDateString().Equals(dateString))
                        {
                            titles.Add(GlobalData.MainLineConflicts[key].Title);
                        }
                    }
                    ++count;
                    if(count == 3)
                    {
                        break;
                    }
                }
            }
            int i = Random.Range(0, titles.Count);
            if (!HearsayMain.says.Contains(titles[i]))
            {
                HearsayMain.says.Add(titles[i]);
            }
            conversations.Add(new Conversation()
            {
                People = person,
                Content = titles[i],
                IsLeft = false
            });
        }
        else
        {
            foreach (var cc in person.BaseData.ChatConversations)
            {
                conversations.Add(cc);
            }
        }
        ControlDialogue.instance.StartConversation(conversations, () =>
        {
            ThridMapMain.ShowAllHeads();
        });
    }
}
