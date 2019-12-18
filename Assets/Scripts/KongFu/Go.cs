using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Go 
{
    private string nm;
    private string skill;

    public Go(string Nm, string Skill)
    {
        this.nm = Nm;
        this.skill = Skill;

    }
    public string Nm
    {
        get { return nm; }
    }
    public string Skill
    {
        get { return skill; }
    }

}
