using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPersonClick : MonoBehaviour
{
    public static Person prePerson;
    public static Person currentPerson;
    private bool isMouseInPerson;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (isMouseInPerson &&Input.GetMouseButtonDown(1))
        {
            Person p = GlobalData.Persons[int.Parse(gameObject.name)];
            FightGUI.SetInfoPanel(p);
        }

        if(FightGUI.isLookingInfo && Input.GetMouseButtonDown(0))
        {
            FightGUI.HideInfoPanel();
            FightGUI.HideDetailPanel();
        }
    }

    private void OnMouseDown()
    {
        if (!GUIMouseHandle.isMouseOver)
        {
            Person p = GlobalData.Persons[int.Parse(gameObject.name)];
            if (currentPerson != null && currentPerson.ControlState == BattleControlState.Attacking
                && AttackTool.attackRange.Count > 0)
            {
                if (AttackTool.AttackEnemys(currentPerson, FightMain.enemyQueue))
                {
                    FightMain.RotatePerson(currentPerson,
                        PersonMoveTool.GetAngle(
                            currentPerson.PersonObject.transform.position, transform.position));
                    FightMain.instance.PlayerFinished();
                }
            }
            else if (currentPerson != null && currentPerson.ControlState == BattleControlState.Treating)
            {
                int value = currentPerson.MedicalSkillResumeHP();
                AttackTool.PersonChangeHP(p, value, true);
                TreatTool.ResumeGrid();
                FightMain.OneRoundOver(currentPerson);
                FightMain.instance.PlayerFinished();
            }
            else
            {
                SelectAPerson(p);
            }
        }
    }

    private void OnMouseEnter()
    {
        isMouseInPerson = true;
        GameObject gridObject = FightMain.gridDataToObject[GlobalData.Persons[int.Parse(gameObject.name)].RowCol];
        if (currentPerson != null && currentPerson.ControlState == BattleControlState.Attacking &&
            AttackTool.attackDistance.Contains(gridObject))
        {
            AttackTool.CountAttackRange(gridObject, currentPerson, FightMain.friendQueue);
            AttackTool.ShowAttackRange();
        }
    }

    private void OnMouseExit()
    {
        isMouseInPerson = false;
        GameObject gridObject = FightMain.gridDataToObject[GlobalData.Persons[int.Parse(gameObject.name)].RowCol];
        if (currentPerson != null && currentPerson.ControlState == BattleControlState.Attacking &&
            AttackTool.attackDistance.Contains(gridObject))
        {
            AttackTool.ClearAttackRange();
        }
    }

    public static void SelectAPerson(Person p)
    {
        if (FightMain.friendQueue.Contains(p))
        {
            if (p.ControlState == BattleControlState.Moving)
            {
                prePerson = currentPerson;
                currentPerson = p;
                FightGUI.ShowBattlePane(currentPerson);

                if (prePerson != null)
                {
                    FightGridClick.SwitchGridColor(FightMain.gridDataToObject[prePerson.RowCol], FightGridClick.defaultColor);
                    FightGridClick.ChangeMoveRangeColor(prePerson, FightGridClick.defaultColor);
                    if (prePerson.ControlState == BattleControlState.Moved)
                    {
                        prePerson.ControlState = BattleControlState.End;
                        FightMain.CountPlayerOver();
                    }
                }
                FightGridClick.moveRange = PersonMoveTool.GenerateMoveRange(currentPerson.RowCol, currentPerson.MoveRank);
                FightGridClick.ChangeMoveRangeColor(currentPerson, FightGridClick.rangeColor);
                CameraFollow.cameraFollowInstance.SetCameraFollowTarget(p);
            }
            else if (p.ControlState == BattleControlState.Moved)
            {
                FightGUI.ShowBattlePane(p);
            }
            
            if(p.ControlState != BattleControlState.End)
            {
                FightGridClick.SwitchGridColor(FightMain.gridDataToObject[currentPerson.RowCol], FightGridClick.selectColor);
            }
        }
    }
}
