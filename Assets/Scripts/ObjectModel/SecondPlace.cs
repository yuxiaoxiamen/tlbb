using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlace : Place
{
    public FirstPlace PrePlace { get; set; }

    public override string GetPlaceString()
    {
        return PrePlace.Id + "-" + Id;
    }
}
