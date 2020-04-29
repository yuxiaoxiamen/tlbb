using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBuffTool
{
    public static void EnemyGetDeBuff(Person attacker, Person enemy)
    {
        AttackStyleFixData attackStyleData = attacker.SelectedAttackStyle.FixData;
        foreach (AttackStyleEffect effect in attackStyleData.Effects)
        {
            if (effect.Type == EffectType.DeBuff)
            {
                TriggerValueDeBuff(enemy, AddBuff(enemy, effect));
            }
        }
    }

    public static void PersonGetBuff(Person attacker)
    {
        AttackStyleFixData attackStyleData = attacker.SelectedAttackStyle.FixData;
        foreach (AttackStyleEffect effect in attackStyleData.Effects)
        {
            if (effect.Type == EffectType.Buff)
            {
                TriggerValueBuff(attacker, AddBuff(attacker, effect));
            }
        }
    }

    public static AttackBuff AddBuff(Person person, AttackStyleEffect effect)
    {
        var buff = new AttackBuff()
        {
            StyleEffect = effect,
            Duration = effect.TimeValue
        };
        bool flag = false;
        if (effect.Id == 2 || effect.Id == 3 || effect.Id == 4 || effect.Id == 19 || effect.Id == 20)
        {
            foreach(AttackBuff bf in person.AttackBuffs)
            {
                if(bf.StyleEffect.Id == effect.Id)
                {
                    bf.Duration += effect.TimeValue;
                    flag = true;
                    break;
                }
            }
        }
        if (!flag)
        {
            person.AttackBuffs.Add(buff);
        }
        
        return buff;
    }

    public static void ReduceBuffDuration(Person person)
    {
        List<AttackBuff> buffs = new List<AttackBuff>();
        foreach (AttackBuff buff in person.AttackBuffs)
        {
            --buff.Duration;
            if (buff.Duration != 0)
            {
                buffs.Add(buff);
            }
            else
            {
                ResumeValueBuff(person, buff);
            }
        }
        person.AttackBuffs.Clear();
        person.AttackBuffs = buffs;
    }

    public static int IsPersonHasSeriousInjuryBuff(Person person)
    {
        foreach (AttackBuff buff in person.AttackBuffs)
        {
            if (buff.StyleEffect.Id == 9)
            {
                return buff.StyleEffect.Value;
            }
        }
        return 0;
    }

    public static bool IsPersonHasSkipBuff(Person person)
    {
        foreach (AttackBuff buff in person.AttackBuffs)
        {
            if (buff.StyleEffect.Id == 0)
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsPersonHasIgnoreDefendBuff(Person person)
    {
        foreach (AttackBuff buff in person.AttackBuffs)
        {
            if (buff.StyleEffect.Id == 12)
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsPersonHasInvincibleBuff(Person person)
    {
        foreach (AttackBuff buff in person.AttackBuffs)
        {
            if (buff.StyleEffect.Id == 11)
            {
                return true;
            }
        }
        return false;
    }

    private static void ResumeValueBuff(Person person, AttackBuff buff)
    {
        AttackStyleEffect effect = buff.StyleEffect;
        switch (effect.Id)
        {
            case 1:
                person.MoveRank -= buff.AmountOfChange;
                break;
            case 5:
                person.Accuracy -= buff.AmountOfChange;
                break;
            case 6:
                person.Defend -= buff.AmountOfChange;
                break;
            case 7:
                person.AttackPowerRate -= buff.AmountOfChange;
                break;
            case 8:
                person.Dodge -= buff.AmountOfChange;
                break;
            case 13:
                person.MoveRank -= buff.AmountOfChange;
                break;
            case 15:
                person.Dodge -= buff.AmountOfChange;
                break;
            case 16:
                person.Crit -= buff.AmountOfChange;
                break;
            case 17:
                person.Counterattack -= buff.AmountOfChange;
                break;
            case 18:
                person.Defend -= buff.AmountOfChange;
                break;
            case 21:
                person.AttackPowerRate -= buff.AmountOfChange;
                break;
        }
    }

    public static void TriggerValueBuff(Person person, AttackBuff buff)
    {
        AttackStyleEffect effect = buff.StyleEffect;
        int changeValue = 0;
        switch (effect.Id)
        {
            case 13:
                buff.AmountOfChange = effect.Value;
                person.MoveRank += effect.Value;
                break;
            case 15:
                changeValue = effect.Value;
                person.Dodge += changeValue;
                buff.AmountOfChange = changeValue;
                break;
            case 16:
                changeValue = effect.Value;
                person.Crit += changeValue;
                buff.AmountOfChange = changeValue;
                break;
            case 17:
                changeValue = effect.Value;
                person.Counterattack += changeValue;
                buff.AmountOfChange = changeValue;
                break;
            case 18:
                changeValue = (int)(person.Defend * effect.Value * 1.0 / 100);
                person.Defend += changeValue;
                buff.AmountOfChange = changeValue;
                break;
            case 21:
                changeValue = effect.Value;
                person.AttackPowerRate += changeValue;
                buff.AmountOfChange = changeValue;
                break;
            case 22:
                List<AttackBuff> buffs = new List<AttackBuff>();
                foreach (AttackBuff bf in person.AttackBuffs)
                {
                    if (bf.StyleEffect.Type == EffectType.Buff)
                    {
                        buffs.Add(bf);
                    }
                    else
                    {
                        ResumeValueBuff(person, bf);
                    }
                }
                person.AttackBuffs = buffs;
                break;
        }
    }

    public static void TriggerValueDeBuff(Person person, AttackBuff buff)
    {
        AttackStyleEffect effect = buff.StyleEffect;
        int changeValue = 0;
        switch (effect.Id)
        {
            case 1:
                changeValue = -effect.Value;
                person.MoveRank += changeValue;
                buff.AmountOfChange = changeValue;
                break;
            case 5:
                changeValue = -effect.Value;
                person.Accuracy += changeValue;
                buff.AmountOfChange = changeValue;
                break;
            case 6:
                changeValue = -(int)(person.Defend * effect.Value * 1.0 / 100);
                person.Defend += changeValue;
                buff.AmountOfChange = changeValue;
                break;
            case 7:
                changeValue = -effect.Value;
                person.AttackPowerRate += changeValue;
                buff.AmountOfChange = changeValue;
                break;
            case 8:
                changeValue = -effect.Value;
                person.Dodge += changeValue;
                buff.AmountOfChange = changeValue;
                break;
            case 10:
                List<AttackBuff> debuffs = new List<AttackBuff>();
                foreach (AttackBuff bf in person.AttackBuffs)
                {
                    if (bf.StyleEffect.Type == EffectType.DeBuff)
                    {
                        debuffs.Add(bf);
                    }
                    else
                    {
                        ResumeValueBuff(person, bf);
                    }
                }
                person.AttackBuffs = debuffs;
                break;
        }
    }

    public static void ReduceHPMP(Person person)
    {
        foreach (AttackBuff buff in person.AttackBuffs)
        {
            AttackStyleEffect effect = buff.StyleEffect;
            if (effect.Id == 2 || effect.Id == 4)
            {
                int changeValue = (int)(person.CurrentHP * (effect.Value * 1.0 / 100));
                AttackTool.PersonChangeHP(person, changeValue, false);
            }
            if (effect.Id == 3)
            {
                int changeValue = (int)(person.CurrentMP * (effect.Value * 1.0 / 100));
                AttackTool.PersonChangeMP(person, changeValue, false);
            }
        }
    }

    public static void TriggerDubleHitBuff(Person attacker, HashSet<Person> enemys)
    {
        foreach (AttackBuff buff in attacker.AttackBuffs)
        {
            if (buff.StyleEffect.Id == 23)
            {
                foreach (Person enemy in enemys)
                {
                    AttackTool.PersonChangeHP(enemy, AttackTool.CountHPLoseValue(attacker, enemy), false);
                }
                break;
            }
        }
    }

    public static void TriggerReboundBuff(Person attacker, Person enemy, int value)
    {
        foreach (AttackBuff buff in enemy.AttackBuffs)
        {
            if (buff.StyleEffect.Id == 14)
            {
                AttackTool.PersonChangeHP(attacker, value, false);
                break;
            }
        }
    }

    public static void TriggerAbsorbBuff(Person attacker, Person enemy)
    {
        foreach (AttackBuff buff in attacker.AttackBuffs)
        {
            AttackStyleEffect effect = buff.StyleEffect;
            if (effect.Id == 19)
            {
                int value = (int)(enemy.CurrentHP * (effect.Value * 1.0 / 100));
                AttackTool.PersonChangeHP(attacker, value, true);
                AttackTool.PersonChangeHP(enemy, value, false);
            }
            if(effect.Id == 20)
            {
                int value = (int)(enemy.CurrentMP * (effect.Value * 1.0 / 100));
                AttackTool.PersonChangeMP(attacker, value, true);
                AttackTool.PersonChangeMP(enemy, value, false);
            }
        }
    }
}
