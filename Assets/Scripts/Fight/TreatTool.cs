using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreatTool : MonoBehaviour
{
    public static void ShowTreatPersons(Person person)
    {
        FightGridClick.ClearPathAndRange();
        person.ControlState = BattleControlState.Treating;
        foreach (Person friend in FightMain.friendQueue)
        {
            GameObject gridObject = FightMain.gridDataToObject[friend.RowCol];
            FightGridClick.SwitchGridColor(gridObject, FightGridClick.treatColor);
        }
        FightGUI.HideBattlePane();
    }

    public static void ResumeGrid()
    {
        foreach (Person friend in FightMain.friendQueue)
        {
            GameObject gridObject = FightMain.gridDataToObject[friend.RowCol];
            FightGridClick.SwitchGridColor(gridObject, FightGridClick.defaultColor);
        }
    }
    
}
