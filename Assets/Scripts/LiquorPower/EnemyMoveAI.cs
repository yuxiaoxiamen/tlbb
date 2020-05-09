using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveAI : MonoBehaviour
{
    private bool[,] visited;
    private bool isFind = false;
    private bool isOnce = true;

    private void Start()
    {
        visited = new bool[LiquorPowerMain.instance.maxRow, LiquorPowerMain.instance.maxCol];
    }

    private void Update()
    {
        if (isOnce && LiquorPowerMain.instance.isGameStart)
        {
            Move(LiquorPowerMain.instance.enemy.RowCol);
            isOnce = false;
        }
    }

    public void Move(Vector2Int current)
    {
        if(current == LiquorPowerMain.instance.startRc)
        {
            isFind = true;
        }
        Vector2Int next = new Vector2Int(current.x - 1, current.y);
        RealMove(current, next);

        next = new Vector2Int(current.x, current.y - 1);
        RealMove(current, next);

        next = new Vector2Int(current.x + 1, current.y);
        RealMove(current, next);

        next = new Vector2Int(current.x, current.y + 1);
        RealMove(current, next);

    }

    void RealMove(Vector2Int current, Vector2Int next)
    {
        if (IsRoad(next) && !IsVisited(next) && !isFind)
        {
            LiquorPowerMain.instance.MoveEnemy(next);
            SetVisited(next, true);
            Move(next);
            if (!isFind)
            {
                SetVisited(next, false);
                LiquorPowerMain.instance.MoveEnemy(current);
            }
        }
    }

    bool IsRoad(Vector2Int rc)
    {
        return LiquorPowerMain.instance.grids[rc.x, rc.y];
    }

    bool IsVisited(Vector2Int rc)
    {
        return visited[rc.x, rc.y];
    }

    void SetVisited(Vector2Int rc, bool tof)
    {
        visited[rc.x, rc.y] = tof;
    }
}
