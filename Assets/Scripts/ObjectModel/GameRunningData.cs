﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRunningData
{
    private static GameRunningData instance;
    public Person player;
    public GameDate date;
    public Place currentPlace;
    public List<Person> teammates;
    public List<Good> belongings;
    public Vector2Int playerPreRc;
    public int money;

    private GameRunningData()
    {
        date = new GameDate(1, 1, 1, 1);
        player = GlobalData.Persons[0];
        player.RowCol = new Vector2Int(33, 43);
        playerPreRc = new Vector2Int(30, 39);
        currentPlace = null;
        money = 0;
        teammates = new List<Person>();
        belongings = new List<Good>();
    }

    public static GameRunningData GetRunningData()
    {
        if(instance == null)
        {
            instance = new GameRunningData();
        }
        return instance;
    }

    public string GetPlaceDateKey()
    {
        return currentPlace.GetPlaceString() + "/" + date.GetDateString();
    }

    public void ReturnToMap()
    {
        if(currentPlace != null)
        {
            if (currentPlace is SecondPlace)
            {
                SceneManager.LoadScene("ThridMap");
            }
            else
            {
                FirstPlace place = (FirstPlace)currentPlace;
                if(place.Sites != null)
                {
                    SceneManager.LoadScene("SecondMap");
                }
                else
                {
                    SceneManager.LoadScene("ThridMap");
                }
            }
        }
        else
        {
            player.RowCol = playerPreRc;
            SceneManager.LoadScene("FirstMap");
        }
    }

    public void AddItem(Good item)
    {
        if (belongings.Contains(item))
        {
            ++item.Number;
        }
        else
        {
            belongings.Add(item);
        }
    }
}
