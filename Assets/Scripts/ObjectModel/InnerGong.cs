using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InnerGong
{
    [System.NonSerialized]
    private InnerGongFixData fixData;
    public InnerGongFixData FixData { get { return fixData; } set { fixData = value; } }
    public int Id { get; set; }
    public int Rank { get; set; }
    public int Proficiency { get; set; }

    public int GetGrade()
    {
        if (FixData.Id == 0 || FixData.Id == 1 || FixData.Id == 2 ||
            FixData.Id == 3 || FixData.Id == 4 || FixData.Id == 5 ||
            FixData.Id == 6 || FixData.Id == 7)
        {
            return 3;
        }else if(FixData.Id == 8 || FixData.Id == 9 || FixData.Id == 10 ||
            FixData.Id == 11 || FixData.Id == 12 || FixData.Id == 13 ||
            FixData.Id == 14 || FixData.Id == 15 || FixData.Id == 16)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    public InnerGong()
    {
        Rank = 1;
        Proficiency = 0;
    }

    public int GetMaxProFiciency()
    {
        return (int)(FixData.FirstMaxProficiency * Mathf.Pow(1 + FixData.NextMaxRatio / 100f, Rank - 1));
    }

    public bool AddExperience(int e)
    {
        if (Rank < GameConfig.MaxRank)
        {
            Proficiency += e;
            if (Proficiency >= GetMaxProFiciency())
            {
                ++Rank;
                Proficiency = 0;
                return true;
            }
        }
        return false;
    }
}
