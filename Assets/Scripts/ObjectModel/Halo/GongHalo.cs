using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GongHalo
{
    public Person Owner { get; set; }
    public HashSet<Vector2Int> Range { get; set; }
    public List<Person> Persons { get; set; }

    public abstract void ActBuffOnPerson(Person person);

    public abstract void ResumeBuffOnPerson(Person person, bool isInLoop);

    public abstract void ResumeBuffAllPerson();

    public void EffectHalo()
    {
        foreach (Person person in Persons)
        {
            if (Range.Contains(person.RowCol))
            {
                ActBuffOnPerson(person);
            }
        }
    }
}
