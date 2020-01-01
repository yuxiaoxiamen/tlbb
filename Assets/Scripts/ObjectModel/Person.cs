using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person
{
    public AttackStyle SelectedAttackStyle { get; set; }
    public InnerGong SelectedInnerGong { get; set; }
    public Vector2Int RowCol { get; set; }
    public GameObject PersonObject { get; set; }
    public BattleControlState ControlState { get; set; }
    public int CurrentHP { get; set; }
    public int CurrentMP { get; set; }
    public int CurrentEnergy { get; set; }
    public float Crit { get; set; }
    public float Counterattack { get; set; }
    public float Dodge { get; set; }
    public int Defend { get; set; }
    public int MoveRank { get; set; }
    public string CurrentPlaceString { get; set; }
    public bool IsMoved { get; set; } 
    public PersonBaseData BaseData { get; set; }

    public Person()
    {
        IsMoved = false;
        MoveRank = 3;
        ControlState = BattleControlState.Moving;
    }

    public void InitAttribute()
    {
        Crit = 10;
        Counterattack = 20;
        Dodge = 30;
        Defend = 30;
        MoveRank = 3;
    }

    public void UpdatePlace()
    {

    }
}
public enum BattleControlState { Moving, Moved, End, Attacking };
