using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GongBuffTool
{
    public static HashSet<Person> FiveTenTriggers { get; set; }
    public static Dictionary<Person, List<int>> NightSixTriggers { get; set; }
    public static Dictionary<Person, int> FourteenSixTriggers { get; set; }
    public static Dictionary<Person, List<int>> FourteenTenTriggers { get; set; }
    public static bool EnemyHaveFullMapBuff { get; set; }
    public static bool FriendHaveFullMapBuff { get; set; }
    public static List<GongHalo> AllHalos { get; set; }

    static GongBuffTool()
    {
        FiveTenTriggers = new HashSet<Person>();
        NightSixTriggers = new Dictionary<Person, List<int>>();
        FourteenSixTriggers = new Dictionary<Person, int>();
        FourteenTenTriggers = new Dictionary<Person, List<int>>();
        AllHalos = new List<GongHalo>();
    }

    public static void CreateAllHalo(List<Person> friends, List<Person> enemys)
    {
        foreach(Person person in friends)
        {
            CreateHalo(person, friends, enemys);
        }
    }

    public static void CreateHalo(Person person, List<Person> friends, List<Person> enemys)
    {
        GongHalo halo = null;
        switch (person.SelectedInnerGong.FixData.Id)
        {
            case 0:
                if (person.SelectedInnerGong.Rank >= 1)
                {
                    halo = new ValueHalo()
                    {
                        Persons = friends
                    };
                }
                break;
            case 5:
                if (person.SelectedInnerGong.Rank >= 1)
                {
                    halo = new RecoverHalo()
                    {
                        Persons = friends
                    };
                }
                break;
            case 13:
                if (person.SelectedInnerGong.Rank >= 6)
                {
                    halo = new ValueHalo()
                    {
                        Persons = enemys
                    };
                }
                break;
            case 15:
                if (person.SelectedInnerGong.Rank >= 10)
                {
                    halo = new RecoverHalo()
                    {
                        Persons = friends
                    };
                }
                break;
        }
        if (halo != null)
        {
            halo.Owner = person;
            halo.Range = PersonMoveTool.CreateRange(person.RowCol, 1);
            halo.Range.Add(person.RowCol);
            halo.EffectHalo();
            AllHalos.Add(halo);
        }

        if (person.SelectedInnerGong.FixData.Id == 10)
        {
            if (person.SelectedInnerGong.Rank >= 6)
            {
                halo = new ValueHalo()
                {
                    Owner = person,
                    Range = PersonMoveTool.CreateRange(person.RowCol, 1),
                    Persons = friends
                };
                halo.Range.Add(person.RowCol);
                halo.EffectHalo();
                AllHalos.Add(halo);
            }
            if (person.SelectedInnerGong.Rank >= 10)
            {
                halo = new ValueHalo()
                {
                    Owner = person,
                    Range = PersonMoveTool.CreateRange(person.RowCol, 1),
                    Persons = friends
                };
                halo.Range.Add(person.RowCol);
                halo.EffectHalo();
                AllHalos.Add(halo);
            }
        }
    }

    public static void HaloPersonMoveListener(Person person, Vector2Int preRc, Vector2Int currentRc)
    {
        foreach(GongHalo halo in AllHalos)
        {
            if(person == halo.Owner)
            {
                halo.ResumeBuffAllPerson();
                halo.Range = PersonMoveTool.CreateRange(currentRc, 1);
                halo.Range.Add(currentRc);
                halo.EffectHalo();
            }
            else
            {
                if (halo.Range.Contains(preRc) && !halo.Range.Contains(currentRc))
                {
                    halo.ResumeBuffOnPerson(person, false);
                }
                else if (!halo.Range.Contains(preRc) && halo.Range.Contains(currentRc))
                {
                    halo.ActBuffOnPerson(person);
                }
            }
        }
    }

    public static void EffectRecoverHalo(Person person)
    {
        foreach(GongHalo halo in AllHalos)
        {
            if (halo is RecoverHalo recoverHalo)
            {
                recoverHalo.EffectBuff(person);
            }
        }
    }

    public static void HPBuffTrigger(Person person)
    {
        SetFiveTen(person);
        SixTen(person);
        NightSix(person);
        NightTen(person);
        FourteenSix(person);
        FourteenTen(person);
        TwentytwoSix(person);
    }

    public static void ResumeGongBuff(Person person)
    {
        ResumeValueBuff(person);
        ResumeNightSix(person);
        ResumeFourteenSix(person);
        ResumeFourteenTen(person);

        if(person.SelectedInnerGong.FixData.Id == 18 && person.SelectedInnerGong.Rank >= 10)
        {
            if (FightMain.instance.friendQueue.Contains(person))
            {
                ResumeEighteenTen(FightMain.instance.friendQueue, FightMain.instance.enemyQueue, false);
            }
            else
            {
                ResumeEighteenTen(FightMain.instance.enemyQueue, FightMain.instance.friendQueue, true);
            }
        }

        GongHalo removeHalo = null;
        foreach (GongHalo halo in AllHalos)
        {
            if (person == halo.Owner)
            {
                halo.ResumeBuffAllPerson();
                removeHalo = halo;
            }
        }
        if(removeHalo != null)
        {
            AllHalos.Remove(removeHalo);
        }
    }

    public static void EffectDefaultBuff(Person person)
    {
        InnerGongFixData gong = person.SelectedInnerGong.FixData;
        int changeHPValue = 0;
        int changeMPValue = 0;
        switch (person.SelectedInnerGong.GetGrade())
        {
            case 1:
                changeMPValue = (int)(person.BaseData.MP * 0.08);
                AttackTool.PersonChangeMP(person, changeMPValue, true);
                break;
            case 2:
                changeHPValue = (int)(person.BaseData.HP * 0.08);
                changeMPValue = (int)(person.BaseData.MP * 0.1);
                AttackTool.PersonChangeHP(person, changeHPValue, true);
                AttackTool.PersonChangeMP(person, changeMPValue, true);
                break;
            case 3:
                changeHPValue = (int)(person.BaseData.HP * 0.1);
                changeMPValue = (int)(person.BaseData.MP * 0.2);
                AttackTool.PersonChangeHP(person, changeHPValue, true);
                AttackTool.PersonChangeMP(person, changeMPValue, true);
                break;
        }
    }

    public static void GongBuffRevertHPMP(Person person)
    {
        InnerGongFixData gong = person.SelectedInnerGong.FixData;
        int changeHPValue = 0;
        int changeMPValue = 0;
        switch (person.SelectedInnerGong.FixData.Id)
        {
            case 1:
                changeMPValue = (int)(person.BaseData.MP * 0.2);
                AttackTool.PersonChangeMP(person, changeMPValue, true);
                break;
            case 15:
                if(person.SelectedInnerGong.Rank >= 6)
                {
                    changeHPValue = (int)(person.BaseData.HP * 0.05);
                    changeMPValue = (int)(person.BaseData.MP * 0.2);
                    AttackTool.PersonChangeHP(person, changeHPValue, true);
                    AttackTool.PersonChangeMP(person, changeMPValue, true);
                }
                break;
            case 17:
                changeMPValue = (int)(person.BaseData.MP * 0.1);
                AttackTool.PersonChangeMP(person, changeMPValue, true);
                break;
            case 23:
                if(person.SelectedInnerGong.Rank >= 6)
                {
                    changeMPValue = (int)(person.BaseData.MP * 0.2);
                    AttackTool.PersonChangeMP(person, changeMPValue, true);
                }
                break;
        }
    }

    public static void ResumeValueBuff(Person person)
    {
        InnerGong gong = person.GongBuff.Gong;
        GongBuff gongBuff = person.GongBuff;
        switch (gong.FixData.Id)
        {
            case 0:
                if (gong.Rank >= 6)
                {
                    person.Dodge -= gongBuff.AmountOfChanges[0];
                }
                if (gong.Rank >= 10)
                {
                    person.MoveRank -= gongBuff.AmountOfChanges[1];
                }
                break;
            case 2:
                if (gong.Rank >= 6)
                {
                    person.AttackPowerRate -= gongBuff.AmountOfChanges[0];
                }
                break;
            case 3:
                if (gong.Rank >= 1)
                {
                    person.Crit -= gongBuff.AmountOfChanges[0];
                    person.Dodge -= gongBuff.AmountOfChanges[1];
                    person.Counterattack -= gongBuff.AmountOfChanges[2];
                }
                if (gong.Rank >= 6)
                {
                    person.AttackPowerRate -= gongBuff.AmountOfChanges[3];
                }
                break;
            case 5:
                if (gong.Rank >= 6)
                {
                    person.Dodge -= gongBuff.AmountOfChanges[0];
                }
                break;
            case 7:
                if (gong.Rank >= 1)
                {
                    person.Defend -= gongBuff.AmountOfChanges[0];
                }
                if (gong.Rank >= 6)
                {
                    person.Crit -= gongBuff.AmountOfChanges[1];
                    person.Dodge -= gongBuff.AmountOfChanges[2];
                    person.Counterattack -= gongBuff.AmountOfChanges[3];
                }
                break;
            case 11:
                if (gong.Rank >= 6)
                {
                    person.Defend -= gongBuff.AmountOfChanges[0];
                    person.AttackPowerRate -= gongBuff.AmountOfChanges[1];
                    person.Dodge -= gongBuff.AmountOfChanges[2];
                }
                break;
            case 12:
                if (gong.Rank >= 10)
                {
                    person.AttackPowerRate -= gongBuff.AmountOfChanges[0];
                    person.Crit -= gongBuff.AmountOfChanges[1];
                }
                break;
            case 16:
                if (gong.Rank >= 6)
                {
                    person.Counterattack -= gongBuff.AmountOfChanges[0];
                }
                break;
            case 19:
                if (gong.Rank >= 6)
                {
                    person.MoveRank -= gongBuff.AmountOfChanges[0];
                }
                break;
            case 20:
                if (gong.Rank >= 6)
                {
                    person.AttackPowerRate -= gongBuff.AmountOfChanges[0];
                    person.Defend -= gongBuff.AmountOfChanges[1];
                }
                break;
            case 21:
                if (gong.Rank >= 6)
                {
                    person.Defend -= gongBuff.AmountOfChanges[0];
                    person.Dodge -= gongBuff.AmountOfChanges[1];
                }
                break;
        }
    }

    public static void EffectValueBuff(Person person)
    {
        InnerGong gong = person.SelectedInnerGong;
        GongBuff gongBuff = new GongBuff
        {
            Gong = gong
        };
        person.GongBuff = gongBuff;
        int changeValue = 0;
        switch (gong.FixData.Id)
        {
            case 0:
                if(gong.Rank >= 6)
                {
                    changeValue = 20;
                    person.Dodge += changeValue;
                    gongBuff.AmountOfChanges.Add(changeValue);
                }
                if(gong.Rank >= 10)
                {
                    changeValue = 2;
                    person.MoveRank += changeValue;
                    gongBuff.AmountOfChanges.Add(changeValue);
                }
                break;
            case 2:
                if (gong.Rank >= 6)
                {
                    changeValue = 15;
                    person.AttackPowerRate += changeValue;
                    gongBuff.AmountOfChanges.Add(changeValue);
                }
                break;
            case 3:
                if (gong.Rank >= 1)
                {
                    changeValue = 10;
                    person.Crit += changeValue;
                    gongBuff.AmountOfChanges.Add(changeValue);

                    changeValue = 10;
                    person.Dodge += changeValue;
                    gongBuff.AmountOfChanges.Add(changeValue);

                    changeValue = 10;
                    person.Counterattack += changeValue;
                    gongBuff.AmountOfChanges.Add(changeValue);
                }
                if (gong.Rank >= 6)
                {
                    changeValue = 10;
                    person.AttackPowerRate += changeValue;
                    gongBuff.AmountOfChanges.Add(changeValue);
                }
                break;
            case 5:
                if (gong.Rank >= 6)
                {
                    changeValue = 20;
                    person.Dodge += changeValue;
                    gongBuff.AmountOfChanges.Add(changeValue);
                }
                break;
            case 7:
                if (gong.Rank >= 1)
                {
                    changeValue = (int)(person.Defend * 0.2);
                    person.Defend += changeValue;
                    gongBuff.AmountOfChanges.Add(changeValue);
                }
                if (gong.Rank >= 6)
                {
                    changeValue = 20;
                    person.Crit += changeValue;
                    gongBuff.AmountOfChanges.Add(changeValue);

                    changeValue = 20;
                    person.Dodge += changeValue;
                    gongBuff.AmountOfChanges.Add(changeValue);

                    changeValue = 20;
                    person.Counterattack += changeValue;
                    gongBuff.AmountOfChanges.Add(changeValue);
                }
                break;
            case 11:
                if(gong.Rank >= 6)
                {
                    changeValue = (int)(person.Defend * 0.2);
                    person.Defend += changeValue;
                    gongBuff.AmountOfChanges.Add(changeValue);

                    changeValue = 10;
                    person.AttackPowerRate += changeValue;
                    gongBuff.AmountOfChanges.Add(changeValue);

                    changeValue = 20;
                    person.Dodge += changeValue;
                    gongBuff.AmountOfChanges.Add(changeValue);
                }
                break;
            case 12:
                if (gong.Rank >= 10)
                {
                    changeValue = 30;
                    person.AttackPowerRate += changeValue;
                    gongBuff.AmountOfChanges.Add(changeValue);

                    changeValue = 30;
                    person.Crit += changeValue;
                    gongBuff.AmountOfChanges.Add(changeValue);
                }
                break;
            case 16:
                if (gong.Rank >= 6)
                {
                    changeValue = 30;
                    person.Counterattack += changeValue;
                    gongBuff.AmountOfChanges.Add(changeValue);
                }
                break;
            case 19:
                if (gong.Rank >= 6)
                {
                    changeValue = 1;
                    person.MoveRank += changeValue;
                    gongBuff.AmountOfChanges.Add(changeValue);
                }
                break;
            case 20:
                if (gong.Rank >= 6)
                {
                    changeValue = 20;
                    person.AttackPowerRate += changeValue;
                    gongBuff.AmountOfChanges.Add(changeValue);

                    changeValue = (int)(person.Defend * 0.1);
                    person.Defend += changeValue;
                    gongBuff.AmountOfChanges.Add(changeValue);
                }
                break;
            case 21:
                if (gong.Rank >= 6)
                {
                    changeValue = (int)(person.Defend * 0.1);
                    person.Defend += changeValue;
                    gongBuff.AmountOfChanges.Add(changeValue);

                    changeValue = 10;
                    person.Dodge += changeValue;
                    gongBuff.AmountOfChanges.Add(changeValue);
                }
                break;
        }
    }

    public static int ThreeGradeReduceInjury(Person person, int value)
    {
        if(person.SelectedInnerGong.GetGrade() == 3)
        {
            value = value - (int)(value * 0.2);
        }
        return value;
    }

    public static void ZeroSix(Person person)
    {
        if (person.SelectedInnerGong.FixData.Id == 0 && person.SelectedInnerGong.Rank >= 6)
        {
            AttackBuffTool.AddBuff(person, GlobalData.StyleEffects[18]);
        }
    }

    public static int OneSix(Person person, int value)
    {
        int v = 0;
        if(person.SelectedInnerGong.FixData.Id == 1 && person.SelectedInnerGong.Rank >= 6)
        {
            v = (int)(value * 0.5);
            if (v > person.CurrentMP)
            {
                v = person.CurrentMP;
            }
            AttackTool.PersonChangeMP(person, v, false);
        }
        return value - v;
    }

    public static void OneTen(Person attacker, Person enemy)
    {
        if (attacker.SelectedInnerGong.FixData.Id == 1 && attacker.SelectedInnerGong.Rank >= 10)
        {
            int v = (int)(enemy.CurrentMP * 0.3);
            AttackTool.PersonChangeMP(enemy, v, false);
            AttackTool.PersonChangeMP(attacker, v, true);
        }
    }

    public static int TwoOne(Person person, int value)
    {
        if (person.SelectedInnerGong.FixData.Id == 2 && person.SelectedInnerGong.Rank >= 1)
        {
            value = value - (int)(value * 0.3);
        }
        return value;
    }

    public static void TwoTen(Person person)
    {
        if (person.SelectedInnerGong.FixData.Id == 2 && person.SelectedInnerGong.Rank >= 10)
        {
            int v = (int)(person.BaseData.HP * 0.1);
            AttackTool.PersonChangeHP(person, v, true);
            v = (int)(person.BaseData.MP * 0.1);
            AttackTool.PersonChangeMP(person, v, true);
        }
    }

    public static bool FourOne(Person person)
    {
        if(person.SelectedInnerGong.FixData.Id == 4 && person.SelectedInnerGong.Rank >= 1)
        {
            return true;
        }
        return false;
    }

    public static bool FourSix(Person person)
    {
        if (person.SelectedInnerGong.FixData.Id == 4 && person.SelectedInnerGong.Rank >= 6)
        {
            return true;
        }
        return false;
    }

    public static void FiveSix(Person person)
    {
        if (person.SelectedInnerGong.FixData.Id == 5 && person.SelectedInnerGong.Rank >= 6)
        {
            AttackBuffTool.AddBuff(person, GlobalData.StyleEffects[20]);
        }
    }

    public static void SetFiveTen(Person person)
    {
        if (person.SelectedInnerGong.FixData.Id == 5 && person.SelectedInnerGong.Rank >= 10)
        {
            if(person.CurrentHP * 1.0 / person.BaseData.HP <= 0.6)
            {
                FiveTenTriggers.Add(person);
            }
            else
            {
                FiveTenTriggers.Remove(person);
            }
        }
    }

    public static int TriggerFiveTen(Person person, int value)
    {
        if (FiveTenTriggers.Contains(person))
        {
            value = value - (int)(value * 0.1);
        }
        return value;
    }

    public static void SixOne(Person attacker, Person enemy)
    {
        if (attacker.SelectedInnerGong.FixData.Id == 6 && attacker.SelectedInnerGong.Rank >= 1)
        {
            AttackBuffTool.AddBuff(enemy, GlobalData.StyleEffects[4]);
        }
    }

    public static void SixSix(Person person)
    {
        if (person.SelectedInnerGong.FixData.Id == 6 && person.SelectedInnerGong.Rank >= 6)
        {
            AttackBuffTool.AddBuff(person, GlobalData.StyleEffects[11]);
        }
    }

    public static void SixTen(Person person)
    {
        if (person.SelectedInnerGong.FixData.Id == 6 && person.SelectedInnerGong.Rank >= 10)
        {
            if (person.CurrentHP * 1.0 / person.BaseData.HP <= 0.4)
            {
                AttackBuffTool.AddBuff(person, GlobalData.StyleEffects[14]);
            }
        }
    }

    public static void SevenTen(Person person)
    {
        if (person.SelectedInnerGong.FixData.Id == 7 && person.SelectedInnerGong.Rank >= 10)
        {
            if (person.CurrentMP * 1.0 / person.BaseData.MP <= 0.4)
            {
                AttackBuffTool.AddBuff(person, GlobalData.StyleEffects[22]);
            }
        }
    }

    public static void EightSix(Person attacker, Person enemy)
    {
        if (attacker.SelectedInnerGong.FixData.Id == 8 && attacker.SelectedInnerGong.Rank >= 6)
        {
            int v = (int)(enemy.BaseData.MP * 0.2);
            AttackTool.PersonChangeMP(enemy, v, false);
        }
    }

    public static void EightTen(Person attacker, Person enemy, int value)
    {
        if (enemy.SelectedInnerGong.FixData.Id == 8 && enemy.SelectedInnerGong.Rank >= 10)
        {
            int v = (int)(value * 0.15);
            AttackTool.PersonChangeMP(attacker, v, false);
        }
    }

    public static void NightSix(Person person)
    {
        if (person.SelectedInnerGong.FixData.Id == 9 && person.SelectedInnerGong.Rank >= 6)
        {
            if (!NightSixTriggers.ContainsKey(person))
            {
                if (person.CurrentHP * 1.0 / person.BaseData.HP >= 0.5)
                {
                    List<int> amountOfChanges = new List<int>();
                    int changeValue = 10;
                    person.AttackPowerRate += changeValue;
                    amountOfChanges.Add(changeValue);

                    changeValue = (int)(person.Defend * 0.1);
                    person.Defend += changeValue;
                    amountOfChanges.Add(changeValue);
                    NightSixTriggers.Add(person, amountOfChanges);
                }
                else
                {
                    ResumeNightSix(person);
                }
            }
        }
    }

    public static void ResumeNightSix(Person person)
    {
        if (NightSixTriggers.ContainsKey(person))
        {
            List<int> amountOfChanges = NightSixTriggers[person];
            person.AttackPowerRate -= amountOfChanges[0];
            person.Defend -= amountOfChanges[1];
            NightSixTriggers.Remove(person);
        }
    }

    public static void NightTen(Person person)
    {
        if (person.SelectedInnerGong.FixData.Id == 9 && person.SelectedInnerGong.Rank >= 10)
        {
            if (person.CurrentHP * 1.0 / person.BaseData.HP <= 0.5)
            {
                AttackBuffTool.AddBuff(person, GlobalData.StyleEffects[19]);
            }
        }
    }

    public static void ElevenTen(Person person)
    {
        if (person.SelectedInnerGong.FixData.Id == 11 && person.SelectedInnerGong.Rank >= 10)
        {
            AttackBuffTool.AddBuff(person, GlobalData.StyleEffects[11]);
        }
    }

    public static void TwelveSix(Person attacker, Person enemy)
    {
        if (attacker.SelectedInnerGong.FixData.Id == 12 && attacker.SelectedInnerGong.Rank >= 6)
        {
            AttackBuffTool.AddBuff(enemy, GlobalData.StyleEffects[7]);
        }
    }

    public static bool ThirteenOne(Person person)
    {
        if (person.SelectedInnerGong.FixData.Id == 13 && person.SelectedInnerGong.Rank >= 1)
        {
            return true;
        }
        return false;
    }

    public static void FourteenSix(Person person)
    {
        if (person.SelectedInnerGong.FixData.Id == 14 && person.SelectedInnerGong.Rank >= 6)
        {
            if (!FourteenSixTriggers.ContainsKey(person))
            {
                if (person.CurrentHP * 1.0 / person.BaseData.HP <= 0.7)
                {
                    int changeValue = 10;
                    person.Crit += changeValue;
                    FourteenSixTriggers.Add(person, changeValue);
                }
                else
                {
                    ResumeFourteenSix(person);
                }
            }
        }
    }

    public static void ResumeFourteenSix(Person person)
    {
        if (FourteenSixTriggers.ContainsKey(person))
        {
            person.Crit -= FourteenSixTriggers[person];
            FourteenSixTriggers.Remove(person);
        }
    }

    public static void FourteenTen(Person person)
    {
        if (person.SelectedInnerGong.FixData.Id == 14 && person.SelectedInnerGong.Rank >= 10)
        {
            if (!FourteenTenTriggers.ContainsKey(person))
            {
                if (person.CurrentHP * 1.0 / person.BaseData.HP <= 0.5)
                {
                    List<int> amountOfChanges = new List<int>();
                    int changeValue = 20;
                    person.Counterattack += changeValue;
                    amountOfChanges.Add(changeValue);

                    changeValue = (int)(person.Defend * 0.1);
                    person.Defend += changeValue;
                    amountOfChanges.Add(changeValue);
                    FourteenTenTriggers.Add(person, amountOfChanges);
                }
                else
                {
                    ResumeFourteenTen(person);
                }
            }
        }
    }

    public static void ResumeFourteenTen(Person person)
    {
        if (FourteenTenTriggers.ContainsKey(person))
        {
            List<int> amountOfChanges = FourteenTenTriggers[person];
            person.Counterattack -= amountOfChanges[0];
            person.Defend -= amountOfChanges[1];
            FourteenTenTriggers.Remove(person);
        }
    }

    public static void SixteenTen(Person attacker)
    {
        if (attacker.SelectedInnerGong.FixData.Id == 16 && attacker.SelectedInnerGong.Rank >= 10)
        {
            AttackBuffTool.AddBuff(attacker, GlobalData.StyleEffects[14]);
        }
    }

    public static void TwentytwoSix(Person person)
    {
        if (person.SelectedInnerGong.FixData.Id == 22 && person.SelectedInnerGong.Rank >= 6)
        {
            if (person.CurrentHP * 1.0 / person.BaseData.HP <= 0.5)
            {
                AttackBuffTool.AddBuff(person, GlobalData.StyleEffects[21]);
            }
        }
    }

    public static void TwentyfourSix(Person person)
    {
        if (person.SelectedInnerGong.FixData.Id == 24 && person.SelectedInnerGong.Rank >= 6)
        {
            AttackBuffTool.AddBuff(person, GlobalData.StyleEffects[20]);
        }
    }

    public static void TwentyfiveSix(Person person)
    {
        if (person.SelectedInnerGong.FixData.Id == 25 && person.SelectedInnerGong.Rank >= 6)
        {
            AttackBuffTool.AddBuff(person, GlobalData.StyleEffects[11]);
        }
    }

    public static void EightennTen(List<Person> friends, List<Person> enemys, bool isEnemy)
    {
        if (isEnemy)
        {
            EnemyHaveFullMapBuff = false;
        }
        else
        {
            FriendHaveFullMapBuff = false;
        }
        foreach(Person friend in friends)
        {
            if(friend.SelectedInnerGong.FixData.Id == 18 && friend.SelectedInnerGong.Rank >= 10)
            {
                if (isEnemy)
                {
                    EnemyHaveFullMapBuff = true;
                }
                else
                {
                    FriendHaveFullMapBuff = true;
                }
                break;
            }
        }
        if ((isEnemy && EnemyHaveFullMapBuff) || (!isEnemy && FriendHaveFullMapBuff))
        {
            foreach (Person friend in friends)
            {
                friend.AttackPowerRate += 30;
            }
            foreach (Person enemy in enemys)
            {
                enemy.AttackPowerRate -= 30;
            }
        }
    }

    public static void ResumeEighteenTen(List<Person> friends, List<Person> enemys, bool isEnemy)
    {
        if ((isEnemy && EnemyHaveFullMapBuff) || (!isEnemy && FriendHaveFullMapBuff))
        {
            foreach (Person friend in friends)
            {
                friend.AttackPowerRate -= 30;
            }
            foreach (Person enemy in enemys)
            {
                enemy.AttackPowerRate += 30;
            }
        }
    }
}
