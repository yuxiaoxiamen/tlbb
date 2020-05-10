using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGoSubject
{
    public List<Person> persons;
    private static TimeGoSubject instance;
    private List<GameDate> mainLineTime;
    private int preUpdateIndex;

    private TimeGoSubject()
    {
        persons = new List<Person>();
        mainLineTime = new List<GameDate>();
        GetMainLineTimeQueue();
    }

    public static TimeGoSubject GetTimeSubject()
    {
        if(instance == null)
        {
            instance = new TimeGoSubject();
        }
        return instance;
    }

    public void Attach(Person person)
    {
        persons.Add(person);
    }

    public void UpdateTime(int space)
    {
        if (GameRunningData.GetRunningData().isFinal)
        {
            return;
        }
        GameRunningData.GetRunningData().date.GoByTime(space);
        if (GameRunningData.GetRunningData().date.CompareTo(mainLineTime[mainLineTime.Count - 1]) >= 0)
        {
            GameRunningData.GetRunningData().date = mainLineTime[mainLineTime.Count - 1];
            GameRunningData.GetRunningData().isFinal = true;
        }
        foreach(Person person in persons)
        {
            person.PromoteGong(person.SelectedInnerGong);
            if(person == GameRunningData.GetRunningData().player || 
                GameRunningData.GetRunningData().teammates.Contains(person))
            {
                for(int i = 1; i <= space; ++i)
                {
                    person.HpMpEnergyChange();
                }
            }
            else
            {
                person.PromoteAttackStyle();
            }
        }
        UpdatePersonPlace();
    }

    void UpdatePersonPlace()
    {
        for(int i = preUpdateIndex + 1; i < mainLineTime.Count; ++i)
        {
            var time = mainLineTime[i];
            if (GameRunningData.GetRunningData().date.CompareTo(time) < 0)
            {
                break;
            }
            else
            {
                foreach (var key in GlobalData.MainLineConflicts.Keys)
                {
                    var dateString = key.Split('/')[1];
                    HashSet<Person> set = new HashSet<Person>();
                    if (time.GetDateString().Equals(dateString))
                    {
                        var placeString = key.Split('/')[0];
                        MainLineConflict conflict = GlobalData.MainLineConflicts[key];
                        foreach (var p in conflict.ZFriends)
                        {
                            set.Add(p);
                        }
                        foreach (var p in conflict.FFriends)
                        {
                            set.Add(p);
                        }
                        foreach (var p in conflict.ZEnemys)
                        {
                            set.Add(p);
                        }
                        foreach (var p in conflict.FEnemys)
                        {
                            set.Add(p);
                        }
                        foreach(var p in set)
                        {
                            p.UpdatePlace(placeString);
                        }
                    }
                }
                preUpdateIndex = i;
            }
        }
    }

    void GetMainLineTimeQueue()
    {
        mainLineTime = SortMainLine();
        preUpdateIndex = -1;
    }

    public List<GameDate> SortMainLine()
    {
        var time = new List<GameDate>();
        foreach (var key in GlobalData.MainLineConflicts.Keys)
        {
            var dateString = key.Split('/')[1];
            string[] dateStrings = dateString.Split('-');
            time.Add(new GameDate(int.Parse(dateStrings[0]), int.Parse(dateStrings[1]),
                int.Parse(dateStrings[2]), int.Parse(dateStrings[3])));
        }
        time.Sort(delegate (GameDate x, GameDate y)
        {
            return x.CompareTo(y);
        });
        return time;
    }
    
}
