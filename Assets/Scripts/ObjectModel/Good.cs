﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Good
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Information { get; set; }
    public ItemKind Type { get; set; }
    public string Effect { get; set; }
    public int ResumeValue { get; set; }
    public int AttrValue { get; set; }
    public int BuyingPrice { get; set; }
    public int SellingPrice { get; set; }
}

public enum ItemKind { Alcohol, Food, Sword, Knife, Rod }
