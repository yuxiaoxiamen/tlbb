using System.Collections;
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
    public int experspance;
    public bool isFinal;

    private GameRunningData()
    {
        date = new GameDate(1, 1, 1, 0);
        player = GlobalData.Persons[0];
        player.RowCol = new Vector2Int(46, 50);
        currentPlace = null;
        isFinal = false;
        money = 50000;   //测试数据
        experspance = 1000;
        teammates = new List<Person>();
        belongings = new List<Good>();
        teammates.Add(GlobalData.Persons[2]);
    }

    public static GameRunningData GetRunningData()
    {
        if(instance == null)
        {
            instance = new GameRunningData();
        }
        return instance;
    }

    public void SavePlayerMapRc()
    {
        playerPreRc = player.RowCol;
    }

    public string GetPlaceDateKey()
    {
        return currentPlace.GetPlaceString() + "/" + date.GetDateString();
    }

    public void ReturnToMap()
    {
        ControlBottomPanel.IsBanPane = false;
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
            item.Number = 1;
        }
    }
}
