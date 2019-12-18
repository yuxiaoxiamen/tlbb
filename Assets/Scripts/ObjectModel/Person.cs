using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person
{
    public AttackStyle SelectedAttackStyle { get; set; }
    public InnerGong SelectedInnerGong { get; set; }
    public Vector2Int RowCol { get; set; } //人物在战斗场景中的几行几列
    public GameObject PersonObject { get; set; } //在Scene中的GameObject
    public BattleControlState ControlState { get; set; }
    public int MoveRank { get; set; }
    public int CurrentHP { get; set; }
    public int CurrentMP { get; set; }
    public int CurrentEnergy { get; set; }
    public bool IsMoved { get; set; } 
    public PersonBaseData BaseData { get; set; }

    public Person()
    {
        IsMoved = false;
        ControlState = BattleControlState.Moving;
        MoveRank = 3;
        CurrentHP = 100;
        CurrentMP = 100;
        CurrentEnergy = 100;
    }
}
public enum BattleControlState { Moving, Moved, End, Attacking };
