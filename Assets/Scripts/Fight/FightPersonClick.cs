using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPersonClick : MonoBehaviour
{
    public static Person prePerson;
    public static Person currentPerson;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        Person p = GlobalData.Persons[int.Parse(gameObject.name)];
        SelectAPerson(p);
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
            }
            else if (p.ControlState == BattleControlState.Moved)
            {
                FightGUI.ShowBattlePane(p);
            }

            FightGridClick.SwitchGridColor(FightMain.gridDataToObject[currentPerson.RowCol], FightGridClick.selectColor);
            CameraFollow.cameraFollowInstance.SetCameraFollowTarget(currentPerson);
        }
    }
}
