using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlace : Place
{
    public Vector2Int Entry { get; set; }
    public List<Vector2Int> Hold { get; set; }
    public List<SecondPlace> Sites { get; set; }

    public override string GetPlaceString()
    {
        return Id + "";
    }

    public bool IsGridInPlace(Vector2Int grid)
    {
        if (Hold.Contains(grid) || Entry == grid)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
