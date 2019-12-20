using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRunningData
{
    private static GameRunningData instance;
    public Person player;
    public GameDate date;
    public Place currentPlace;

    private GameRunningData()
    {
        date = new GameDate(1, 1, 1, 1);
        player = GlobalData.Persons[0];
        player.RowCol = new Vector2Int(30, 39);
        currentPlace = GlobalData.FirstPlaces[3];
    }

    public static GameRunningData GetRunningData()
    {
        if(instance == null)
        {
            instance = new GameRunningData();
        }
        return instance;
    }
}
