using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig
{
    public static int StandardMP { get; set; }
    public static float MPEffectRate { get; set; }
    public static int MaxDefend { get; set; }
    public static int PerMedicalSkillResume { get; set; }
    public static int MaxMedicalSkill { get; set; }
    public static int PerStyleExperience { get; set; }
    public static int PerGongExperience { get; set; }
    public static int WuXingAddition { get; set; }
    public static int MaxRank { get; set; }
    public static List<Conversation> MapEventConversation { get; set; }
    public static List<Conversation> JiuEventConversation { get; set; }
    public static List<Conversation> YiEventConversation { get; set; }
    public static List<Conversation> DanEventConversation { get; set; }
    public static List<Conversation> QieEventConversation { get; set; }
    public static List<Conversation> DuanEventConversation { get; set; }
    public static int MapEventProbability { get; set; }
    public static List<int> ConsultCost { get; set; }
    public static int RewardMaxIndex { get; set; }

    static GameConfig()
    {
        StandardMP = 100;
        MPEffectRate = 0.001f;
        MaxDefend = 100;
        PerMedicalSkillResume = 10;
        MaxMedicalSkill = 100;
        PerStyleExperience = 20;
        PerGongExperience = 5;
        WuXingAddition = 2;
        MaxRank = 10;
        MapEventConversation = new List<Conversation>
        {
            new Conversation()
            {
                People = GameRunningData.GetRunningData().player,
                Content = "路遇匪徒，进行反击",
                IsLeft = true
            }
        };
        JiuEventConversation = new List<Conversation>
        {
            new Conversation()
            {
                People = GlobalData.Persons[69],
                Content = "小兄弟，口渴了吧，来陪我喝壶酒吧！",
                IsLeft = false
            },
            new Conversation()
            {
                People = GameRunningData.GetRunningData().player,
                Content = "恭敬不如从命，小弟多谢了。",
                IsLeft = true
            }
        };
        YiEventConversation = new List<Conversation>
        {
            new Conversation()
            {
                People = GlobalData.Persons[73],
                Content = "在下赤脚医，孤身游四海。你我相逢便是缘，我来传授你些医术，能否掌握就看你啦！",
                IsLeft = false
            },
            new Conversation()
            {
                People = GameRunningData.GetRunningData().player,
                Content = "让我试试吧！",
                IsLeft = true
            }
        };
        DanEventConversation = new List<Conversation>
        {
            new Conversation()
            {
                People = GlobalData.Persons[73],
                Content = "小兄弟，你听说过炼丹师么！算你走运，今日本炼丹师就指导你一下，看看你能练出什么丹药。",
                IsLeft = false
            },
            new Conversation()
            {
                People = GameRunningData.GetRunningData().player,
                Content = "额......那我来试试吧！",
                IsLeft = true
            }
        };
        QieEventConversation = new List<Conversation>
        {
            new Conversation()
            {
                People = GlobalData.Persons[81],
                Content = "小兄弟，帮个忙好么，我需要处理一些食材。",
                IsLeft = false
            },
            new Conversation()
            {
                People = GameRunningData.GetRunningData().player,
                Content = "乐意效劳！",
                IsLeft = true
            }
        };
        DuanEventConversation = new List<Conversation>
        {
            new Conversation()
            {
                People = GlobalData.Persons[77],
                Content = "嘿，伙计有看上的兵器么？我可以教你如何打造。",
                IsLeft = false
            },
            new Conversation()
            {
                People = GameRunningData.GetRunningData().player,
                Content = "让我来选一下吧！",
                IsLeft = true
            }
        };
        MapEventProbability = 2;
        ConsultCost = new List<int>
        {
            10,50,100,1
        };
        RewardMaxIndex = 21;
    }
}
