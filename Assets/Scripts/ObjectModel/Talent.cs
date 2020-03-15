using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talent
{
    public TalentName Name { get; set; }
    public int Number { get; set; }
}

public enum TalentName { Bi, Gen, Jing, Wu, Shen}