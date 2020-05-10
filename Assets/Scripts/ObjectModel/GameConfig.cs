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
        MapEventProbability = 1;
        ConsultCost = new List<int>
        {
            10,50,100,1
        };
        RewardMaxIndex = 21;
    }
}
