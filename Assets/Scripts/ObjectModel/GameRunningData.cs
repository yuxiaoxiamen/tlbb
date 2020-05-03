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

    private GameRunningData()
    {
        date = new GameDate(1, 1, 1, 1);
        player = GlobalData.Persons[0];
        player.RowCol = new Vector2Int(33, 43);
        playerPreRc = new Vector2Int(30, 39);
        currentPlace = null;
        money = 500;   //测试数据
        teammates = new List<Person>();
        belongings = new List<Good>();

        //测试数据
        belongings.Add(GlobalData.Items[0]);
        belongings.Add(GlobalData.Items[3]);
        belongings.Add(GlobalData.Items[4]);
        belongings.Add(GlobalData.Items[6]);
        belongings.Add(GlobalData.Items[13]);
        belongings.Add(GlobalData.Items[14]);
        belongings.Add(GlobalData.Items[15]);
        belongings[0].Number = 3;
        belongings[1].Number = 4;
        belongings[2].Number = 6;
        belongings[3].Number = 1;
        belongings[4].Number = 2;
        belongings[5].Number = 1;
        belongings[6].Number = 3;

        belongings.Add(GlobalData.Items[23]);
        belongings.Add(GlobalData.Items[24]);
        belongings.Add(GlobalData.Items[25]);
        belongings.Add(GlobalData.Items[38]);
        belongings.Add(GlobalData.Items[39]);
        belongings.Add(GlobalData.Items[40]);
        belongings.Add(GlobalData.Items[55]);
        belongings.Add(GlobalData.Items[56]);
        belongings.Add(GlobalData.Items[57]);
        belongings[7].Number = 1;
        belongings[8].Number = 1;
        belongings[9].Number = 1;
        belongings[10].Number = 1;
        belongings[11].Number = 1;
        belongings[12].Number = 1;
        belongings[13].Number = 1;
        belongings[14].Number = 1;
        belongings[15].Number = 1;

        belongings.Add(GlobalData.Items[64]);
        belongings.Add(GlobalData.Items[65]);
        belongings.Add(GlobalData.Items[66]);
        belongings[16].Number = 1;
        belongings[17].Number = 2;
        belongings[18].Number = 2;

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
