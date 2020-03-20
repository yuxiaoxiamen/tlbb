using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public int Number { get; set; }
    public string RealTime { get; set; }

    public SaveData(int number)
    {
        Number = number;
        RealTime = DateTime.Now.ToString();
    }
}
