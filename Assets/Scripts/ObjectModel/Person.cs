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
    public int MoveRank { get; set; }
    public int CurrentHP { get; set; }
    public int CurrentMP { get; set; }
    public int CurrentEnergy { get; set; }
    public string CurrentPlaceString { get; set; }
    public bool IsMoved { get; set; } 
    public PersonBaseData BaseData { get; set; }

    public Person()
    {
        IsMoved = false;
        ControlState = BattleControlState.Moving;
    }

    public void UpdatePlace()
    {

    }
}
public enum BattleControlState { Moving, Moved, End, Attacking };
