using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonMoveTool
{

    public static HashSet<Vector2Int> CreateRange(Vector2Int startPosition, int rank)
    {
        HashSet<Vector2Int> rangeGrids = new HashSet<Vector2Int>();
        var preRange = new Vector2Int();

        for (int i = 1; i <= rank; ++i)
        {
            var rc = new Vector2Int(startPosition.x, Mathf.Min(startPosition.y + i,
                startPosition.x % 2 == 0 ? FightMain.instance.mapWidth - 1 : FightMain.instance.mapWidth - 2));
            rangeGrids.Add(rc);
            preRange.y = startPosition.y + i;
        }
        for (int i = 1; i <= rank; ++i)
        {
            var rc = new Vector2Int(startPosition.x, Mathf.Max(startPosition.y - i, 0));
            rangeGrids.Add(rc);
            preRange.x = startPosition.y - i;
        }
        for (int i = 1; i <= rank; ++i)
        {
            var currentRange = new Vector2Int();
            if (startPosition.x % 2 != 0)
            {
                if (i % 2 != 0)
                {
                    currentRange.x = preRange.x + 1;
                    currentRange.y = preRange.y;
                }
                else
                {
                    currentRange.x = preRange.x;
                    currentRange.y = preRange.y - 1;
                }
            }
            else
            {
                if (i % 2 != 0)
                {
                    currentRange.x = preRange.x;
                    currentRange.y = preRange.y - 1;
                }
                else
                {
                    currentRange.x = preRange.x + 1;
                    currentRange.y = preRange.y;
                }
            }
            preRange = currentRange;
            for (int j = currentRange.x; j <= currentRange.y; ++j)
            {
                if (startPosition.x + i <= FightMain.instance.mapHeight - 1)
                {
                    int maxWidth = (startPosition.x + i) % 2 == 0 ? FightMain.instance.mapWidth - 1 : FightMain.instance.mapWidth - 2;
                    if (j >= 0 && j <= maxWidth)
                    {
                        var rc = new Vector2Int(startPosition.x + i, j);
                        rangeGrids.Add(rc);
                    }
                }
                if (startPosition.x - i >= 0)
                {
                    int maxWidth = (startPosition.x - i) % 2 == 0 ? FightMain.instance.mapWidth - 1 : FightMain.instance.mapWidth - 2;
                    if (j >= 0 && j <= maxWidth)
                    {
                        var rc = new Vector2Int(startPosition.x - i, j);
                        rangeGrids.Add(rc);
                    }
                }
            }
        }
        return rangeGrids;
    }

    static public HashSet<Vector2Int> GenerateMoveRange(Vector2Int startPosition, int rank)
    {
        HashSet<Vector2Int> rangeGrids = CreateRange(startPosition, rank);
        HashSet<Vector2Int> moveRangeGrids = new HashSet<Vector2Int>();
        HashSet<Vector2Int> obstacles = GetObstacles();
        foreach(var rc in rangeGrids)
        {
            if (!obstacles.Contains(rc) && FindPath(startPosition, rc, FightMain.instance.GetGrids(), obstacles, true).Count <= rank)
            {
                moveRangeGrids.Add(rc);
            }
        }
        return moveRangeGrids;
    }

    static public List<Vector2Int> FindPath(Vector2Int startPosition, Vector2Int endPosition, HashSet<Vector2Int> grids, HashSet<Vector2Int> obstacles, bool isSix)
    {
        var path = PathFinding.Astar(startPosition, endPosition, grids, obstacles);
        path.Remove(startPosition);
        return path;
    }

    static public void MovePerson(List<Vector2Int> movePath, Person person, float speed, Action<Person> finishAction)
    {
        FightMain.instance.positionToPerson.Remove(person.RowCol);
        
        List<Vector3> realPath = new List<Vector3>();
        Vector2Int preRc = person.RowCol;
        foreach (var point in movePath)
        {
            var gridObject = FightMain.instance.gridDataToObject[point];
            realPath.Add(gridObject.transform.position);
        }
        person.PersonObject.GetComponent<PersonAnimationControl>().Move();
        SoundEffectControl.instance.PlaySoundEffect(10);
        CameraFollow.cameraFollowInstance.isMove = true;
        Move(person, realPath, speed, finishAction);
        if(movePath.Count > 0)
        {
            FightMain.instance.positionToPerson.Add(movePath[movePath.Count - 1], person);
            person.RowCol = movePath[movePath.Count - 1];
            GongBuffTool.instance.HaloPersonMoveListener(person, preRc, person.RowCol);
        }
    }

    public static float GetAngle(Vector3 current, Vector3 next)
    {
        if(current.z == next.z)
        {
            if(current.x > next.x)
            {
                return -90;
            }
            else
            {
                return 90;
            }
        }
        else if(current.z > next.z)
        {
            if (current.x > next.x)
            {
                return -150;
            }
            else
            {
                return 150;
            }
        }
        else
        {
            if (current.x > next.x)
            {
                return -30;
            }
            else
            {
                return 30;
            }
        }
    }

    static int pathIndex = 0;
    static private void Move(Person person, List<Vector3> path, float speed, Action<Person> finishAction)
    {
        if (pathIndex < path.Count)
        {
            FightMain.RotatePerson(person, GetAngle(person.PersonObject.transform.position, path[pathIndex]));
            person.PersonObject.transform.DOMove(path[pathIndex], speed).OnComplete(() =>
            {
                ++pathIndex;
                CameraFollow.cameraFollowInstance.SetCameraFollowTarget(person);
                Move(person, path, speed, finishAction);
            });
        }
        else
        {
            pathIndex = 0;
            person.PersonObject.GetComponent<PersonAnimationControl>().Stand();
            CameraFollow.cameraFollowInstance.isMove = false;
            finishAction(person);
        }
    }

    public static HashSet<Vector2Int> GetObstacles()
    {
        HashSet<Vector2Int> obstacles = new HashSet<Vector2Int>();
        foreach (Person person in FightMain.instance.friendQueue)
        {
            obstacles.Add(person.RowCol);
        }
        foreach (Person person in FightMain.instance.enemyQueue)
        {
            obstacles.Add(person.RowCol);
        }
        return obstacles;
    }
}
