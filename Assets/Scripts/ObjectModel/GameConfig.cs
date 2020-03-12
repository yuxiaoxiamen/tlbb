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

    static GameConfig()
    {
        StandardMP = 100;
        MPEffectRate = 0.001f;
        MaxDefend = 100;
        PerMedicalSkillResume = 10;
        MaxMedicalSkill = 100;
        PerStyleExperience = 100;
        PerGongExperience = 100;
        WuXingAddition = 2;
        MaxRank = 10;
    }
}
