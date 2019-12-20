using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Place
{
    public int Id { get; set; }
    public string Name { get; set; }

    public abstract string GetPlaceString();
}
