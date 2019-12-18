using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRunningData
{
    private static GameRunningData instance;
    public Person player;
    public GameDate date;
    public string placeNumber;
    public Place currentPlace;

    private GameRunningData()
    {

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
