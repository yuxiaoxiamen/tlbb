using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class FightGridClick : MonoBehaviour
{
    public static Color defaultColor;
    private static Color pathColor;
    public static Color rangeColor;
    public static Color selectColor;
    public static Color attackDistanceColor;
    public static Color attackRangeColor;
    public static Color treatColor;
    public static float defaultA = 1.0f;
    private static List<Vector2Int> movePath = new List<Vector2Int>();
    public static HashSet<Vector2Int> moveRange = new HashSet<Vector2Int>();
    public static Vector2Int movePreRC;
    public static Quaternion movePreQuaternion;

    // Start is called before the first frame update
    void Awake()
    {
        defaultColor = gameObject.GetComponent<Renderer>().material.color;
        SetColor();
    }

    private void SetColor()
    {
        selectColor = new Color(0, 1, 0, defaultA);
        pathColor = new Color(1, 0, 1, defaultA);
        rangeColor = new Color(0, 1, 1, defaultA);
        attackDistanceColor = new Color(147 / 255f, 112 / 255f, 219 / 255f, defaultA);
        attackRangeColor = new Color(1, 0, 0, defaultA);
        treatColor = new Color(1, 165 / 255f, 0, defaultA);
    }

    private void OnMouseDown()
    {
        if (!GUIMouseHandle.isMouseOver)
        {
            if (FightPersonClick.currentPerson != null && FightPersonClick.currentPerson.ControlState == BattleControlState.Attacking
            && AttackTool.attackRange.Count > 0)
            {
                if (AttackTool.AttackEnemys(FightPersonClick.currentPerson, FightMain.enemyQueue))
                {
                    FightMain.RotatePerson(FightPersonClick.currentPerson,
                        PersonMoveTool.GetAngle(
                            FightPersonClick.currentPerson.PersonObject.transform.position, transform.position));
                    FightMain.instance.PlayerFinished();
                }
            }
            else if (movePath.Count != 0) //当前人物可移动的格子
            {
                SwitchGridColor(FightMain.gridDataToObject[FightPersonClick.currentPerson.RowCol], defaultColor);

                ChangeMoveRangeColor(FightPersonClick.currentPerson, defaultColor);
                FightPersonClick.currentPerson.ControlState = BattleControlState.Moved;
                FightPersonClick.currentPerson.IsMoved = true;

                FightMain.positionToPerson.Remove(FightPersonClick.currentPerson.RowCol);
                FightMain.positionToPerson.Add(FightMain.gridObjectToData[gameObject], FightPersonClick.currentPerson);
                movePreRC = FightPersonClick.currentPerson.RowCol;
                movePreQuaternion = FightPersonClick.currentPerson.PersonObject.transform.rotation;
                PersonMoveTool.MovePerson(movePath, FightPersonClick.currentPerson, FightMain.speed, FightGUI.ShowBattlePane);
                SwitchGridColor(gameObject, selectColor);
                ChangePathColor(defaultColor);
                movePath.Clear();
                FightGUI.HideBattlePane();
                SwitchGridColor(gameObject, selectColor);
            }
        }
    }

    private void OnMouseEnter()
    {
        if (FightPersonClick.currentPerson != null && FightPersonClick.currentPerson.ControlState == BattleControlState.Attacking &&
            AttackTool.attackDistance.Contains(gameObject))
        {
            AttackTool.CountAttackRange(gameObject, FightPersonClick.currentPerson, FightMain.friendQueue);
            AttackTool.ShowAttackRange();
        }
        if (FightPersonClick.currentPerson != null && FightPersonClick.currentPerson.ControlState == BattleControlState.Moving &&
            moveRange.Contains(FightMain.gridObjectToData[gameObject]))
        {
            movePath = PersonMoveTool.FindPath(FightPersonClick.currentPerson.RowCol, 
                FightMain.gridObjectToData[gameObject], FightMain.GetGrids(), PersonMoveTool.GetObstacles(), true);
            ChangePathColor(pathColor);
        }
    }

    private void OnMouseExit()
    {
        if (FightPersonClick.currentPerson != null && FightPersonClick.currentPerson.ControlState == BattleControlState.Attacking &&
            AttackTool.attackDistance.Contains(gameObject))
        {
            AttackTool.ClearAttackRange();
        }
        if (FightPersonClick.currentPerson != null && FightPersonClick.currentPerson.ControlState == BattleControlState.Moving)
        {
            ChangePathColor(rangeColor);
            movePath.Clear();
        }
    }

    public static void ClearPathAndRange()
    {
        ChangePathColor(defaultColor);
        ChangeMoveRangeColor(FightPersonClick.currentPerson, defaultColor);
        movePath.Clear();
        moveRange.Clear();
    }

    public static void ChangeMoveRangeColor(Person person, Color color)
    {
        foreach (var point in moveRange)
        {
            if (point.Equals(person.RowCol))
            {
                continue;
            }
            var gridObject = FightMain.gridDataToObject[point];
            SwitchGridColor(gridObject, color);
        }
    }

    public static void ChangePathColor(Color color)
    {
        foreach (var point in movePath)
        {
            var gridObject = FightMain.gridDataToObject[point];
            SwitchGridColor(gridObject, color);
        }
    }

    public static void SwitchGridColor(GameObject gridObject, Color color)
    {
        gridObject.GetComponent<Renderer>().material.color = color;
    }
}
