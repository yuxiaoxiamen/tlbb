using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTool
{
    public static HashSet<GameObject> attackDistance = new HashSet<GameObject>();
    public static HashSet<GameObject> attackRange = new HashSet<GameObject>();

    public static void CountAttackDistance(Person person, List<Person> friends)
    {
        FightGridClick.ClearPathAndRange();
        HashSet<Vector2Int> distanceRange = new HashSet<Vector2Int>();
        switch (person.SelectedAttackStyle.FixData.Kind)
        {
            case AttackStyleKind.Line:
                distanceRange = PersonMoveTool.CreateRange(person.RowCol, person.SelectedAttackStyle.FixData.AwayFromPerson);
                attackDistance = RangeRemoveFriend(distanceRange, friends);
                person.ControlState = BattleControlState.Attacking;
                FightGUI.HideBattlePane();
                break;
            case AttackStyleKind.Range:
                distanceRange = PersonMoveTool.CreateRange(person.RowCol, person.SelectedAttackStyle.FixData.AttackRank);
                attackRange = RangeRemoveFriend(distanceRange, friends);
                HashSet<Person> canAttackEnemys = new HashSet<Person>();
                foreach (Person enemy in FightMain.enemyQueue)
                {
                    GameObject gridObject = FightMain.gridDataToObject[enemy.RowCol];
                    if (attackRange.Contains(gridObject))
                    {
                        canAttackEnemys.Add(enemy);
                    }
                }
                if (canAttackEnemys.Count > 0)
                {
                    foreach (Person enemy in canAttackEnemys)
                    {
                        Debug.Log(enemy.BaseData.Id);
                        FightMain.PlayerFinished();
                    }
                    FightGUI.HideBattlePane();
                    person.ControlState = BattleControlState.Attacking;
                }
                break;
            case AttackStyleKind.Remote:
                distanceRange = PersonMoveTool.CreateRange(person.RowCol, person.SelectedAttackStyle.FixData.AwayFromPerson);
                attackDistance = RangeRemoveFriend(distanceRange, friends);
                person.ControlState = BattleControlState.Attacking;
                FightGUI.HideBattlePane();
                break;
            case AttackStyleKind.Sector:
                distanceRange = PersonMoveTool.CreateRange(person.RowCol, person.SelectedAttackStyle.FixData.AwayFromPerson);
                attackDistance = RangeRemoveFriend(distanceRange, friends);
                person.ControlState = BattleControlState.Attacking;
                FightGUI.HideBattlePane();
                break;
        }
    }

    public static bool AttackEnemys(List<Person> enemys)
    {
        HashSet<Person> canAttackEnemys = new HashSet<Person>();
        foreach (Person enemy in enemys)
        {
            GameObject gridObject = FightMain.gridDataToObject[enemy.RowCol];
            if (attackRange.Contains(gridObject))
            {
                canAttackEnemys.Add(enemy);
            }
        }
        if (canAttackEnemys.Count > 0)
        {
            foreach (Person enemy in canAttackEnemys)
            {
                enemy.CurrentHP -= 1;
                FightMain.SetPersonHPSplider(enemy);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void ShowAttackRange()
    {
        foreach (var grid in attackRange)
        {
            FightGridClick.SwitchGridColor(grid, FightGridClick.attackRangeColor);
        }
    }

    public static void CountAttackRange(GameObject gridObject, Person person, List<Person> friends)
    {
        if (person.SelectedAttackStyle.FixData.Kind == AttackStyleKind.Line)
        {
            var lineRange = CreateLineRange(person.RowCol, FightMain.gridObjectToData[gridObject]);
            attackRange = RangeRemoveFriend(lineRange, friends);
            var realRange = new HashSet<GameObject>();
            foreach (var rc in attackRange)
            {
                if (attackDistance.Contains(rc))
                {
                    realRange.Add(rc);
                }
            }
            attackRange = realRange;
            realRange = null;
        }
        else if (person.SelectedAttackStyle.FixData.Kind == AttackStyleKind.Remote)
        {
            HashSet<Vector2Int> remoteRange =
                PersonMoveTool.CreateRange(FightMain.gridObjectToData[gridObject], person.SelectedAttackStyle.FixData.AttackRank);
            attackRange = RangeRemoveFriend(remoteRange, friends);
            attackRange.Add(gridObject);
        }
        else
        {
            HashSet<Vector2Int> range1 =
                PersonMoveTool.CreateRange(FightMain.gridObjectToData[gridObject], 1);
            HashSet<Vector2Int> range2 = PersonMoveTool.CreateRange(person.RowCol, 1);
            HashSet<Vector2Int> range = new HashSet<Vector2Int>();
            foreach (var rc in range1)
            {
                if (range2.Contains(rc))
                {
                    range.Add(rc);
                }
            }
            attackRange = RangeRemoveFriend(range, friends);
            attackRange.Add(gridObject);
        }
    }

    public static void ClearAttackDistance()
    {
        if(attackDistance != null)
        {
            foreach (var grid in attackDistance)
            {
                FightGridClick.SwitchGridColor(grid, FightGridClick.defaultColor);
            }
            attackDistance.Clear();
        }
    }

    public static void ClearAttackRange()
    {
        if(attackRange != null)
        {
            foreach (var grid in attackRange)
            {
                if (attackDistance.Contains(grid))
                {
                    FightGridClick.SwitchGridColor(grid, FightGridClick.attackDistanceColor);
                }
                else
                {
                    FightGridClick.SwitchGridColor(grid, FightGridClick.defaultColor);
                }

            }
            attackRange.Clear();
        }
    }

    static HashSet<GameObject> RangeRemoveFriend(HashSet<Vector2Int> gridRange, List<Person> friends)
    {
        HashSet<Vector2Int> obstacles = new HashSet<Vector2Int>();
        var range = new HashSet<GameObject>();
        foreach (var friend in friends)
        {
            obstacles.Add(friend.RowCol);
        }
        foreach (var rc in gridRange)
        {
            if (!obstacles.Contains(rc))
            {
                range.Add(FightMain.gridDataToObject[rc]);
            }
        }
        return range;
    }

    public static void ShowAttackDistance()
    {
        foreach (var grid in attackDistance)
        {
            FightGridClick.SwitchGridColor(grid, FightGridClick.attackDistanceColor); 
        }
    }

    static HashSet<Vector2Int> CreateLineRange(Vector2Int startRc, Vector2Int endRc)
    {
        HashSet<Vector2Int> gridRanges = new HashSet<Vector2Int>();
        if (startRc.x % 2 == 0)//偶
        {
            if (startRc.x > endRc.x)//上
            {
                if (startRc.y <= endRc.y)//右
                {
                    Vector2Int current = startRc;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth)
                    {
                        gridRanges.Add(current);
                        current.x = current.x - 2;
                        current.y = current.y + 1;         
                    }
                    current.x = startRc.x-1;
                    current.y = startRc.y;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth)
                    {
                        gridRanges.Add(current);
                        current.x = current.x - 2;
                        current.y = current.y +1;
                    }
                }
                else//左
                {
                    Vector2Int current = startRc;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth)
                    {
                        gridRanges.Add(current);
                        current.x = current.x - 2;
                        current.y = current.y - 1;
                    }
                    current.x = startRc.x-1;
                    current.y = startRc.y - 1;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth)
                    {
                        gridRanges.Add(current);
                        current.x = current.x - 2;
                        current.y = current.y - 1;
                    }
                }
            }
            else if (startRc.x < endRc.x) // 下
            {
                if (startRc.y <= endRc.y)//右
                {
                    Vector2Int current = startRc;
   
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth)
                    {
                        gridRanges.Add(current);
                        current.x = current.x + 2 ;
                        current.y = current.y + 1;
                    }
                    current.x = startRc.x + 1;
                    current.y = startRc.y;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth)
                    {
                        gridRanges.Add(current);
                        current.x = current.x +2;
                        current.y = current.y+1;
                    }
                }
                else//左
                {
                    Vector2Int current = startRc;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth)
                    {
                        gridRanges.Add(current);
                        current.x = current.x + 2;
                        current.y = current.y - 1;
                        
                    }
                    current.x = startRc.x + 1;
                    current.y = startRc.y - 1;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth)
                    {
                        gridRanges.Add(current);
                        current.x = current.x +2;
                        current.y = current.y-1;
                    }
                }
            }
            else //中
            {
                if (startRc.y > endRc.y)
                {
                    for (int i = 0; i < startRc.y; ++i)
                    {
                        gridRanges.Add(new Vector2Int(startRc.x, i));
                    }
                }
                else
                {
                    for (int i = startRc.y + 1; i < FightMain.mapWidth; ++i)
                    {
                        gridRanges.Add(new Vector2Int(startRc.x, i));
                    }
                }
            }
        }
        else
        {
            if (startRc.x > endRc.x)//上
            {
                if (startRc.y < endRc.y)//右
                {
                    Vector2Int current = startRc;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth - 1)
                    {
                        gridRanges.Add(current);
                        current.x = current.x - 2;
                        current.y = current.y + 1;
                    }
                    current.x = startRc.x - 1;
                    current.y = startRc.y + 1;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth - 1)
                    {
                        gridRanges.Add(current);
                        current.x = current.x - 2;
                        current.y = current.y + 1;
                    }
                }
                else//左
                {
                    Vector2Int current = startRc;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth - 1)
                    {
                        gridRanges.Add(current);
                        current.x = current.x - 2;
                        current.y = current.y - 1;
                    }
                    current.x = startRc.x - 1;
                    current.y = startRc.y;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth - 1)
                    {
                        gridRanges.Add(current);
                        current.x = current.x - 2;
                        current.y = current.y-1;
                    }
                }
            }
            else if (startRc.x < endRc.x) // 下
            {
                if (startRc.y < endRc.y)//右
                {
                    Vector2Int current = startRc;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth - 1)
                    {
                        gridRanges.Add(current);
                        current.x = current.x + 2;
                        current.y = current.y + 1;
                    }
                    current.x = startRc.x + 1;
                    current.y = startRc.y + 1;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth - 1)
                    {
                        gridRanges.Add(current);
                        current.x = current.x +2;
                        current.y = current.y+1;
                    }
                }
                else//左
                {
                    Vector2Int current = startRc;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth - 1)
                    {
                        gridRanges.Add(current);
                        current.x = current.x + 2;
                        current.y = current.y - 1;
                        
                    }
                    current.x = startRc.x + 1;
                    current.y = startRc.y;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth - 1)
                    {
                        gridRanges.Add(current);
                        current.x = current.x +2;
                        current.y = current.y-1;  
                    }
                }
            }
            else //中
            {
                if (startRc.y > endRc.y)
                {
                    for (int i = 0; i < startRc.y; ++i)
                    {
                        gridRanges.Add(new Vector2Int(startRc.x, i));
                    }
                }
                else
                {
                    for (int i = startRc.y + 1; i < FightMain.mapWidth - 1; ++i)
                    {
                        gridRanges.Add(new Vector2Int(startRc.x, i));
                    }
                }
            }
        }
        return gridRanges;
    }
}
