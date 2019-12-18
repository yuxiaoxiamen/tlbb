using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDate
{
    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
    public TimeSlot Slot { get; set; }

    public GameDate(int year, int month, int day, int slot)
    {
        Year = year;
        Month = month;
        Day = day;
        Slot = (TimeSlot)Enum.ToObject(typeof(TimeSlot), slot);
    }

    public string GetDateString()
    {
        return Year + "-" + Month + "-" + Day + "-" + ((int)Slot);
    }
}

public enum TimeSlot { Morning , Noon, Night}
