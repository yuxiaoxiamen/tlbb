using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueHalo : GongHalo
{ 
    public Dictionary<Person, List<int>> AmountOfChanges { get; set; }

    public ValueHalo()
    {
        AmountOfChanges = new Dictionary<Person, List<int>>();
    }

    public override void ActBuffOnPerson(Person person)
    {
        InnerGong gong = Owner.SelectedInnerGong;
        int changeValue = 0;
        List<int> amountOfChanges = new List<int>();
        switch (gong.FixData.Id)
        {
            case 0:
                if (gong.Rank >= 1)
                {
                    changeValue = 10;
                    person.Dodge += changeValue;
                    amountOfChanges.Add(changeValue);
                    AmountOfChanges.Add(person, amountOfChanges);
                }
                break;
            case 10:
                if (gong.Rank >= 6)
                {
                    changeValue = 10;
                    person.Crit += changeValue;
                    amountOfChanges.Add(changeValue);
                    if (gong.Rank >= 10)
                    {
                        changeValue = 10;
                        person.AttackPowerRate += changeValue;
                        amountOfChanges.Add(changeValue);
                    }
                    AmountOfChanges.Add(person, amountOfChanges);
                }
                break;
            case 13:
                if (gong.Rank >= 6)
                {
                    changeValue = -(int)(person.Defend * 0.1);
                    person.Defend += changeValue;
                    amountOfChanges.Add(changeValue);
                    AmountOfChanges.Add(person, amountOfChanges);
                }
                break;
        }
    }

    public override void ResumeBuffOnPerson(Person person, bool isInLoop)
    {
        InnerGong gong = Owner.SelectedInnerGong;
        List<int> amountOfChanges = AmountOfChanges[person];
        switch (gong.FixData.Id)
        {
            case 0:
                if (gong.Rank >= 1)
                {
                    person.Dodge -= amountOfChanges[0];
                }
                break;
            case 10:
                if (gong.Rank >= 6)
                {
                    person.Crit -= amountOfChanges[0];
                    if (gong.Rank >= 10)
                    {
                        person.AttackPowerRate -= amountOfChanges[1];
                    }
                }
                break;
            case 13:
                if (gong.Rank >= 6)
                {
                    person.Defend -= amountOfChanges[0];
                }
                break;
        }
        if (!isInLoop)
        {
            AmountOfChanges.Remove(person);
        }
    }

    public override void ResumeBuffAllPerson()
    {
        foreach (Person person in AmountOfChanges.Keys)
        {
            ResumeBuffOnPerson(person, true);
        }
        AmountOfChanges.Clear();
    }
}
