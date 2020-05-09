using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackStyle
{
    [System.NonSerialized]
    private AttackStyleFixData fixData;
    public AttackStyleFixData FixData { get { return fixData; } set { fixData = value; } }
    public int Id { get; set; }
    public int Rank { get; set; }
    public int Proficiency { get; set; }

    public AttackStyle()
    {
        Rank = 1;
        Proficiency = 0;
    }

    public int GetGrade()
    {
        return FixData.Effects.Count;
    }

    public int GetRealMPCost()
    {
        return (int)(FixData.MPCost * Mathf.Pow(1 + FixData.CostIncrease / 100f, Rank - 1));
    }

    public int GetMaxProFiciency()
    {
        return (int)(FixData.FirstMaxProficiency * Mathf.Pow(1 + FixData.NextMaxRatio / 100f, Rank - 1));
    }

    public int GetRealBasePower()
    {
        return (int)(FixData.BasePower * Mathf.Pow(1 + FixData.PowerIncrease / 100f, Rank - 1));
    }

    public void AddExperience(int e)
    {
        if (Rank < GameConfig.MaxRank)
        {
            Proficiency += e;
            if (Proficiency >= GetMaxProFiciency())
            {
                ++Rank;
                Proficiency = 0;
            }
        }
    }

    public int CompareTo(AttackStyle style)
    {
        if (style.GetRealBasePower() > GetRealBasePower())
        {
            return -1;
        }
        else if (style.GetRealBasePower() < GetRealBasePower())
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}

