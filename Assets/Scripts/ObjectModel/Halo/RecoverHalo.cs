using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverHalo : GongHalo
{
    public HashSet<Person> FiveOne { get; set; }
    public HashSet<Person> FifteenTen { get; set; }

    public RecoverHalo()
    {
        FiveOne = new HashSet<Person>();
        FifteenTen = new HashSet<Person>();
    }

    public override void ActBuffOnPerson(Person person)
    {
        InnerGong gong = Owner.SelectedInnerGong;
        switch (gong.FixData.Id)
        {
            case 5:
                if (gong.Rank >= 1)
                {
                    FiveOne.Add(person);
                }
                break;
            case 15:
                if (gong.Rank >= 10)
                {
                    FifteenTen.Add(person);
                }
                break;
        }
    }

    public override void ResumeBuffOnPerson(Person person, bool isInLoop)
    {
        InnerGong gong = Owner.SelectedInnerGong;
        switch (gong.FixData.Id)
        {
            case 5:
                if (gong.Rank >= 1)
                {
                    FiveOne.Remove(person);
                }
                break;
            case 15:
                if (gong.Rank >= 10)
                {
                    FifteenTen.Remove(person);
                }
                break;
        }
    }

    public override void ResumeBuffAllPerson()
    {
        if (Owner.SelectedInnerGong.FixData.Id == 5)
        {
            FiveOne.Clear();
        }
        else if (Owner.SelectedInnerGong.FixData.Id == 15)
        {
            FifteenTen.Clear();
        }
    }

    public void EffectBuff(Person person)
    {
        if(FiveOne.Contains(person))
        {
            int changeHPValue = (int)(person.BaseData.HP * 0.1);
            int changeMPValue = (int)(person.BaseData.MP * 0.1);
            AttackTool.PersonChangeHP(person, changeHPValue, true);
            AttackTool.PersonChangeMP(person, changeMPValue, true);
        }
        if(FifteenTen.Contains(person))
        {
            int changeHPValue = (int)(person.BaseData.HP * 0.2);
            AttackTool.PersonChangeHP(person, changeHPValue, true);
        }
    }
}
