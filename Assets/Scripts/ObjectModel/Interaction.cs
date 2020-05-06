﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Interaction
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void InteractionListener(Person person)
    {
        InteractControl.instance.HideInterPanel();
        ThridMapMain.HidePeople();
        int money = 0;
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
                ControlBottomPanel.IsBanPane = true;
                switch (person.BaseData.Name)
                {
                    case "店小二":
                        GoodDisplay.storeType = "Food";
                        break;
                    case "酒保":
                        GoodDisplay.storeType = "Alcohol";
                        break;
                    case "铁匠":
                        GoodDisplay.storeType = "Blacksmith";
                        break;
                }
                GoodDisplay.instance.SetItemList();
                ConfirmOperation.instance.ChangeImage();
                ThridMapMain.storeUI.SetActive(true);
                break;
            case 3: //锻造
                ControlBottomPanel.IsBanPane = true;
                ManualMain.instance.SetManual(false);
                ThridMapMain.manualUI.SetActive(true);
                break; 
            case 4: //炼丹
                TimeGoSubject.GetTimeSubject().UpdateTime(5);
                SceneManager.LoadScene("Alchemy");
                break;
            case 5: //治疗
                var player = GameRunningData.GetRunningData().player;
                money = (int)(player.CurrentHP * 1.0f / player.BaseData.HP * 100 * 10);
                if(GameRunningData.GetRunningData().money >= money)
                {
                    player.ChangeHP(player.BaseData.HP, true);
                    GameRunningData.GetRunningData().money -= money;
                    TipControl.instance.SetTip("花费了" + money + "钱" + "，回复全部生命值");
                }
                else
                {
                    TipControl.instance.SetTip( "金钱不足");
                }
                ThridMapMain.ShowAllHeads();
                break;
            case 6: //休息
                money = 50;
                if (GameRunningData.GetRunningData().money >= money)
                {
                    GameRunningData.GetRunningData().money -= money;
                    TimeGoSubject.GetTimeSubject().UpdateTime(1);
                    ControlTopPanel.instance.UpdateTimeText();
                    TipControl.instance.SetTip("花费了" + money + "钱" + "，休息了一会儿");
                }
                else
                {
                    TipControl.instance.SetTip("金钱不足");
                }
                ThridMapMain.ShowAllHeads();
                break;
            case 7: //喝酒
                TimeGoSubject.GetTimeSubject().UpdateTime(2);
                SceneManager.LoadScene("LiquorPower");
                break;
            case 8: //学医
                break;
            case 9: //下厨
                ControlBottomPanel.IsBanPane = true;
                ManualMain.instance.SetManual(true);
                ThridMapMain.manualUI.SetActive(true);
                break;
            case 10: //招募
                GameRunningData.GetRunningData().teammates.Add(person);
                ThridMapMain.instance.persons.Remove(person);
                ThridMapMain.instance.ReAddHead();
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
