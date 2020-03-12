using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStyleFixData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public AttackWeaponKind WeaponKind { get; set; }
    public AttackStyleKind AttackKind { get; set; }
    public int AwayFromPerson { get; set; }
    public int AttackRank { get; set; }
    public string DetailInfo { get; set; }
    public int FirstMaxProficiency { get; set; }
    public int NextMaxRatio { get; set; }
    public int BasePower { get; set; }
    public int PowerIncrease { get; set; }
    public int MPCost { get; set; }
    public int CostIncrease { get; set; }
    public List<AttackStyleEffect> Effects { get; set; }
}

public enum AttackWeaponKind { Fist, Palm, Finger, Knife, Sword, Rod}

public enum AttackStyleKind { Remote, Range, Line, Sector }