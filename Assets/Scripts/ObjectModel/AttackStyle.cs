using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStyle
{
    public AttackStyleFixData FixData { get; set; }
    public int Rank { get; set; }
    public int Proficiency { get; set; }

    public AttackStyle()
    {
        Rank = 1;
        Proficiency = 0;
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
        if(Rank < 10)
        {
            Proficiency += e;
            if (Proficiency >= GetMaxProFiciency())
            {
                ++Rank;
                Proficiency = 0;
            }
        }
    }
}

