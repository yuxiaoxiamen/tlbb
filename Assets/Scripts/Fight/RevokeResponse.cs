using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevokeResponse : MonoBehaviour
{
    public static GameObject preMoveGridObject;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(FightPersonClick.currentPerson != null && FightPersonClick.currentPerson.ControlState == BattleControlState.Attacking)
            {
                if (FightPersonClick.currentPerson.IsMoved)
                {
                    FightPersonClick.currentPerson.ControlState = BattleControlState.Moved;
                }
                else
                {
                    FightPersonClick.currentPerson.ControlState = BattleControlState.Moving;
                }
                
                AttackTool.ClearAttackRange();
                AttackTool.ClearAttackDistance();
                FightPersonClick.SelectAPerson(FightPersonClick.currentPerson);
                FightGUI.ShowBattlePane(FightPersonClick.currentPerson);
            }
            else if(FightPersonClick.currentPerson != null && FightPersonClick.currentPerson.ControlState == BattleControlState.Treating)
            {
                if (FightPersonClick.currentPerson.IsMoved)
                {
                    FightPersonClick.currentPerson.ControlState = BattleControlState.Moved;
                }
                else
                {
                    FightPersonClick.currentPerson.ControlState = BattleControlState.Moving;
                }

                TreatTool.ResumeGrid();
                FightPersonClick.SelectAPerson(FightPersonClick.currentPerson);
                FightGUI.ShowBattlePane(FightPersonClick.currentPerson);
            }
            else if (FightGUI.isSwitching)
            {
                FightGUI.HideScrollPane();
                FightGUI.ShowBattlePane(FightPersonClick.currentPerson);
            }
            else if (FightPersonClick.currentPerson != null && FightPersonClick.currentPerson.ControlState == BattleControlState.Moved) 
            {
                FightGridClick.SwitchGridColor(FightMain.gridDataToObject[FightGridClick.movePreRC], FightGridClick.selectColor);
                FightMain.positionToPerson.Remove(FightPersonClick.currentPerson.RowCol);
                FightMain.positionToPerson.Add(FightGridClick.movePreRC, FightPersonClick.currentPerson);
                FightPersonClick.currentPerson.PersonObject.transform.DOMove(FightMain.gridDataToObject[FightGridClick.movePreRC].transform.position, 0.1f);
                FightPersonClick.currentPerson.ControlState = BattleControlState.Moving;
                FightPersonClick.currentPerson.RowCol = FightGridClick.movePreRC;
                FightPersonClick.currentPerson.PersonObject.transform.rotation = FightGridClick.movePreQuaternion;
                FightPersonClick.currentPerson.IsMoved = true;
                FightGridClick.moveRange = PersonMoveTool.GenerateMoveRange(FightPersonClick.currentPerson.RowCol, FightPersonClick.currentPerson.MoveRank);
                FightGridClick.ChangeMoveRangeColor(FightPersonClick.currentPerson, FightGridClick.rangeColor);
            }
        }
    }
}
