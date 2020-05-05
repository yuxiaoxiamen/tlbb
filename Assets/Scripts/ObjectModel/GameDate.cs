using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDate
{
    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
    public int Slot { get; set; }

    public GameDate(int year, int month, int day, int slot)
    {
        Year = year;
        Month = month;
        Day = day;
        Slot = slot;
    }

    public string GetDateString()
    {
        return Year + "-" + Month + "-" + Day + "-" + Slot;
    }

    public string GetDateText()
    {
        return "第" + Year + "年" + Month + "月" + Day + "日" + GetSoltText(Slot);
    }

    private string GetSoltText(int solt)
    {
        switch (solt)
        {
            case 0:
                return "早晨";
            case 1:
                return "中午";
            case 2:
                return "晚上";
        }
        return "";
    }

    public void GoByTime(int space)
    {
        if ((Slot + space) / 3 == 0)
        {
            Slot = Slot + space;
        }
        else
        {
            int addDay = (Slot + space) / 3;
            Slot = (Slot + space) % 3;
            if(addDay + Day / 30 == 0)
            {
                Day = addDay + Day;
            }
            else
            {
                int addMonth = (addDay + Day) / 30;
                Day = (addDay + Day) % 30;
                if(addMonth + Month / 12 == 0)
                {
                    Month = addMonth + Month;
                }
                else
                {
                    Year = Year + (addMonth + Month) / 12;

                }
            }
        }
    }

    public int CompareTo(GameDate date)
    {
        if (Year > date.Year)
        {
            return 1;
        }
        else if (Year < date.Year)
        {
            return -1;
        }
        else
        {
            if (Month > date.Month)
            {
                return 1;
            }
            else if (Month < date.Month)
            {
                return -1;
            }
            else
            {
                if (Day > date.Day)
                {
                    return 1;
                }
                else if (Day < date.Day)
                {
                    return -1;
                }
                else
                {
                    if (Slot > date.Slot)
                    {
                        return 1;
                    }
                    else if (Slot < date.Slot)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
    }
}
