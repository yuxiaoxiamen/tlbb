using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FightAI
{
    public static List<Person> Enemys { get; set; }
    public static bool AIEnd;
    public static void NPCAI(Person person, List<Person> enemys)
    {
        if (!AttackBuffTool.IsPersonHasSkipBuff(person))
        {
            Person closestEnemy = new Person();
            Enemys = enemys;
            float minDistance = float.MaxValue;
            foreach (Person enemy in enemys)
            {
                float d = PathFinding.GetDistanceSix(person.RowCol, enemy.RowCol);
                if (d < minDistance)
                {
                    minDistance = d;
                    closestEnemy = enemy;
                }
            }

            var obstacles = PersonMoveTool.GetObstacles();
            obstacles.Remove(closestEnemy.RowCol);
            HashSet<Vector2Int> moveRangeGrids = PersonMoveTool.GenerateMoveRange(person.RowCol, person.MoveRank);
            List<Vector2Int> realPath = new List<Vector2Int>();
            if (CanFight(person))
            {
                List<Vector2Int> path = PersonMoveTool.FindPath(person.RowCol, closestEnemy.RowCol, FightMain.GetGrids(), obstacles, true);
                foreach (Vector2Int rc in path)
                {
                    if (moveRangeGrids.Contains(rc))
                    {
                        realPath.Add(rc);
                    }
                }
                PersonMoveTool.MovePerson(realPath, person, FightMain.speed, NPCFight);
            }
            else
            {
                float maxDistance = 0;
                Vector2Int furthestRc = person.RowCol;
                foreach (Vector2Int rc in moveRangeGrids)
                {
                    float d = PathFinding.GetDistanceSix(rc, closestEnemy.RowCol);
                    if (d > maxDistance)
                    {
                        maxDistance = d;
                        furthestRc = rc;
                    }
                }
                realPath = PersonMoveTool.FindPath(person.RowCol, furthestRc, FightMain.GetGrids(), obstacles, true);
                PersonMoveTool.MovePerson(realPath, person, FightMain.speed, NPCRest);
            }
        }
        else
        {
            AIEnd = true;
        }
    }

    static bool CanFight(Person person)
    {
        if(person.CurrentHP * 1.0f / person.BaseData.HP < 0.2)
        {
            return false;
        }
        int maxAttackPower = 0;
        foreach(AttackStyle style in person.BaseData.AttackStyles)
        {
            if(style.GetRealMPCost() <= person.CurrentMP)
            {
                if (style.GetRealBasePower() > maxAttackPower)
                {
                    maxAttackPower = style.GetRealBasePower();
                    person.SelectedAttackStyle = style;
                }
            }
        }
        return maxAttackPower != 0;
    }

    static void NPCFight(Person person)
    {
        AttackTool.CountAttackDistance(person, FightMain.enemyQueue);

        Dictionary<GameObject, int> canAttackCounts = new Dictionary<GameObject, int>();

        foreach (GameObject gridObject in AttackTool.attackDistance)
        {
            AttackTool.CountAttackRange(gridObject, person, FightMain.enemyQueue);
            foreach (GameObject rangeGridObject in AttackTool.attackRange)
            {
                if (FightMain.positionToPerson.ContainsKey(FightMain.gridObjectToData[rangeGridObject]) &&
                    Enemys.Contains(FightMain.positionToPerson[FightMain.gridObjectToData[rangeGridObject]]))
                {
                    if (canAttackCounts.ContainsKey(gridObject))
                    {
                        canAttackCounts[gridObject]++;
                    }
                    else
                    {
                        canAttackCounts.Add(gridObject, 1);
                    }
                }
            }
        }
        var list = canAttackCounts.OrderByDescending(o => o.Value).ToList();
        if(list.Count != 0)
        {
            AttackTool.CountAttackRange(list[0].Key, person, FightMain.enemyQueue);
            AttackTool.ShowAttackRange();
            AttackTool.AttackEnemys(person, Enemys);
            foreach (var gridObject in AttackTool.attackRange)
            {
                gridObject.GetComponent<Renderer>().material.DOColor(FightGridClick.defaultColor, 0.5f).OnComplete(()=>
                {
                    AIEnd = true;
                });
            }
        }
        else
        {
            AIEnd = true;
        }
    }

    static void NPCRest(Person person)
    {
        FightMain.OneRoundOver(person);
        AIEnd = true;
    }
}
