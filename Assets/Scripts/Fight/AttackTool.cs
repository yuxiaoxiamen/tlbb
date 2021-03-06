﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTool : MonoBehaviour
{
    public HashSet<GameObject> attackDistance = new HashSet<GameObject>();
    public HashSet<GameObject> attackRange = new HashSet<GameObject>();
    public static AttackTool instance;

    private void Start()
    {
        instance = this;
        attackRange = new HashSet<GameObject>();
        attackRange = new HashSet<GameObject>();
    }

    public void CountAttackDistance(Person person, List<Person> friends)
    {
        FightGridClick.ClearPathAndRange();
        HashSet<Vector2Int> distanceRange = new HashSet<Vector2Int>();
        switch (person.SelectedAttackStyle.FixData.AttackKind)
        {
            case AttackStyleKind.Line:
                distanceRange = PersonMoveTool.CreateRange(person.RowCol, person.SelectedAttackStyle.FixData.AwayFromPerson);
                attackDistance = RangeRemoveFriend(distanceRange, friends);
                person.ControlState = BattleControlState.Attacking;
                FightGUI.HideBattlePane();
                break;
            case AttackStyleKind.Range:
                if(RangeAttack(person, friends, FightMain.instance.enemyQueue))
                {
                    FightMain.instance.PlayerFinished();
                    FightGUI.HideBattlePane();
                }
                else
                {
                    TipControl.instance.SetTip("攻击范围内没有敌人");
                }
                break;
            case AttackStyleKind.Remote:
                distanceRange = PersonMoveTool.CreateRange(person.RowCol, person.SelectedAttackStyle.FixData.AwayFromPerson);
                attackDistance = RangeRemoveFriend(distanceRange, friends);
                person.ControlState = BattleControlState.Attacking;
                FightGUI.HideBattlePane();
                break;
            case AttackStyleKind.Sector:
                distanceRange = PersonMoveTool.CreateRange(person.RowCol, person.SelectedAttackStyle.FixData.AwayFromPerson);
                attackDistance = RangeRemoveFriend(distanceRange, friends);
                person.ControlState = BattleControlState.Attacking;
                FightGUI.HideBattlePane();
                break;
        }
    }

    public bool RangeAttack(Person person, List<Person> friends, List<Person> enemys)
    {
        var distanceRange = PersonMoveTool.CreateRange(person.RowCol, person.SelectedAttackStyle.FixData.AttackRank);
        attackRange = RangeRemoveFriend(distanceRange, friends);
        attackDistance.Clear();
        HashSet<Person> canAttackEnemys = new HashSet<Person>();
        foreach (Person enemy in enemys)
        {
            GameObject gridObject = FightMain.instance.gridDataToObject[enemy.RowCol];
            if (attackRange.Contains(gridObject))
            {
                canAttackEnemys.Add(enemy);
            }
        }
        if (canAttackEnemys.Count > 0)
        {
            AttackAction(person, canAttackEnemys);
            return true;
        }
        return false;
    }

    public void PlayAttackMusic(Person person)
    {
        switch (person.SelectedAttackStyle.FixData.WeaponKind)
        {
            case AttackWeaponKind.Knife:
                SoundEffectControl.instance.PlaySoundEffect(11);
                break;
            case AttackWeaponKind.Sword:
                SoundEffectControl.instance.PlaySoundEffect(12);
                break;
            case AttackWeaponKind.Rod:
                SoundEffectControl.instance.PlaySoundEffect(13);
                break;
            case AttackWeaponKind.Fist:
                SoundEffectControl.instance.PlaySoundEffect(4);
                break;
            case AttackWeaponKind.Palm:
                SoundEffectControl.instance.PlaySoundEffect(5);
                break;
            case AttackWeaponKind.Finger:
                SoundEffectControl.instance.PlaySoundEffect(6);
                break;
        }
    }

    public bool AttackEnemys(Person attacker, List<Person> enemys)
    {
        HashSet<Person> canAttackEnemys = new HashSet<Person>();
        foreach (Person enemy in enemys)
        {
            GameObject gridObject = FightMain.instance.gridDataToObject[enemy.RowCol];
            if (attackRange.Contains(gridObject))
            {
                canAttackEnemys.Add(enemy);
            }
        }
        return AttackAction(attacker, canAttackEnemys);
    }

    private bool AttackAction(Person attacker, HashSet<Person> canAttackEnemys)
    {
        if (canAttackEnemys.Count > 0)
        {
            AttackStyle attackStyle = attacker.SelectedAttackStyle;
            int mpCost = attackStyle.GetRealMPCost();
            if (mpCost > attacker.CurrentMP)
            {
                TipControl.instance.SetTip("内力不足");
                return false;
            }
            PlayAttackMusic(attacker);
            StartCoroutine(attacker.PersonObject.GetComponent<PersonAnimationControl>().Action());
            PersonChangeMP(attacker, mpCost, false);

            PromoteStyleProficiency(attacker);
            AttackBuffTool.PersonGetBuff(attacker);

            foreach (Person enemy in canAttackEnemys)
            {
                AttackBuffTool.EnemyGetDeBuff(attacker, enemy);
                GongBuffTool.instance.SixOne(attacker, enemy);
                GongBuffTool.instance.EightSix(attacker, enemy);
                int value = CountHPLoseValue(attacker, enemy);
                value = GongBuffTool.instance.ThreeGradeReduceInjury(enemy, value);
                value = GongBuffTool.instance.OneSix(enemy, value);
                value = GongBuffTool.instance.TwoOne(enemy, value);
                value = GongBuffTool.instance.TriggerFiveTen(enemy, value);
                GongBuffTool.instance.OneTen(attacker, enemy);
                GongBuffTool.instance.ThreeTen(attacker, enemy);
                GongBuffTool.instance.EightTen(attacker, enemy, value);
                if (PersonChangeHP(enemy, value, false))
                {
                    GongBuffTool.instance.TwoTen(attacker);
                }
                AttackBuffTool.TriggerReboundBuff(attacker, enemy, value);
                AttackBuffTool.TriggerAbsorbBuff(attacker, enemy);

                float angle = PersonMoveTool.GetAngle(enemy.PersonObject.transform.position,
                    attacker.PersonObject.transform.position);
                FightMain.RotatePerson(enemy, angle);
            }

            AttackBuffTool.TriggerDubleHitBuff(attacker, canAttackEnemys);
            FightMain.OneRoundOver(attacker);
            return true;
        }
        else
        {
            return false;
        }
    }

    private static void PromoteStyleProficiency(Person person)
    {
        person.SelectedAttackStyle.AddExperience(person.CountStyleExperience());
    }

    private static bool ComputeProbability(float probability)
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        int x = Random.Range(1, 101);
        if(x <= probability)
        {
            return true;
        }
        return false;
    }

    public static int CountHPLoseValue(Person attacker, Person enemy)
    {
        int value = 0;
        float rate = 1 - CountAngle(attacker, enemy);
        
        if (!AttackBuffTool.IsPersonHasInvincibleBuff(enemy))
        {
            if (!GongBuffTool.instance.ThirteenOne(attacker) && 
                !GongBuffTool.instance.FourOne(attacker) && ComputeProbability((int)(enemy.Dodge * rate) + (100 - attacker.Accuracy)))
            {
                GongBuffTool.instance.ZeroSix(enemy);
                GongBuffTool.instance.FiveSix(enemy);
                GongBuffTool.instance.SixSix(enemy);
                GongBuffTool.instance.TwentyfourSix(enemy);
                SpecialEffectTool.instance.RateEffect(enemy, "闪避");
            }
            else
            {
                int attackPower = attacker.SelectedAttackStyle.GetRealBasePower();
                int defend = AttackBuffTool.IsPersonHasIgnoreDefendBuff(attacker) ? 0 : enemy.Defend;
                int mp = attacker.CurrentMP;
                int energy = attacker.CurrentEnergy;
                int attackPowerRate = attacker.AttackPowerRate;
                value = (int)(
                    attackPower *
                    (1 + attackPowerRate * 1.0 / 100) *
                    (1 - defend * 0.5 / GameConfig.MaxDefend) *
                    (1 + (mp - GameConfig.StandardMP) * GameConfig.MPEffectRate) *  
                    (1 - (attacker.BaseData.Energy - energy) * 1.0 / 100));
                if (ComputeProbability(attacker.Crit))
                {
                    value *= 2;
                    GongBuffTool.instance.ElevenTen(attacker);
                    GongBuffTool.instance.TwelveSix(attacker, enemy);
                    SpecialEffectTool.instance.RateEffect(attacker, "暴击");
                }

                if (GongBuffTool.instance.FourTen(enemy) || (!GongBuffTool.instance.FourSix(attacker) && ComputeProbability((int)(enemy.Counterattack * rate))))
                {
                    GongBuffTool.instance.SixteenTen(enemy);
                    GongBuffTool.instance.TwentyfiveSix(enemy);
                    PersonChangeHP(attacker, enemy.SelectedAttackStyle.GetRealBasePower(), false);
                    SpecialEffectTool.instance.RateEffect(enemy, "反击");
                }
            }
        }
        return value;
    }

    static float CountAngle(Person attacker, Person enemy)
    {
        Vector3 a = attacker.PersonObject.transform.position - enemy.PersonObject.transform.position;
        Vector3 b = enemy.PersonObject.transform.forward;
        float angle = Vector3.Angle(a, b);
        if (angle >= 0 && angle < 30)
        {
            return 0.2f;
        }
        else if(angle >= 30 && angle < 90)
        {
            return 0.3f;
        }
        else if (angle >= 90 && angle < 150)
        {
            return 0.4f;
        }
        else
        {
            return 0.5f;
        }
    }

    public static bool PersonChangeHP(Person person, int value, bool isAdd)
    {
        int realValue = value;
        if (isAdd)
        {
            int rate = AttackBuffTool.IsPersonHasSeriousInjuryBuff(person);
            realValue = (int)(value * (1 - rate * 1.0 / 100));
            person.ChangeHP(realValue, isAdd);
        }
        else
        {
            person.CurrentHP -= realValue;
            if (person.CurrentHP <= 0)
            {
                person.CurrentHP = 0;
                PersonDead(person);
                return true;
            }
        }
        SpecialEffectTool.instance.HPEffect(person, realValue, isAdd);
        FightMain.SetPersonHPSplider(person);
        GongBuffTool.instance.HPBuffTrigger(person);
        return false;
    }

    public static void PersonChangeMP(Person person, int value, bool isAdd)
    {
        person.ChangeMP(value, isAdd);
        GongBuffTool.instance.SevenTen(person);
    }

    private static void PersonDead(Person person)
    {
        person.PersonObject.SetActive(false);
        FightMain.instance.DestoryHPSplider(person);
        FightMain.instance.positionToPerson.Remove(person.RowCol);
        person.ChangeHP((int)(0.2f * person.BaseData.HP), true);
        person.ChangeMP((int)(0.2f * person.BaseData.MP), true);
        if (FightMain.instance.friendQueue.Contains(person))
        {
            FightMain.instance.friendQueue.Remove(person);
            if(FightMain.instance.friendQueue.Count == 0)
            {
                FightMain.instance.isFail = true;
            }
        }
        if (FightMain.instance.enemyQueue.Contains(person))
        {
            FightMain.instance.enemyQueue.Remove(person);
            if (FightMain.instance.enemyQueue.Count == 0)
            {
                FightMain.instance.isSuccess = true;
            }
        }
    }

    public void ShowAttackRange()
    {
        foreach (var grid in attackRange)
        {
            FightGridClick.SwitchGridColor(grid, FightGridClick.attackRangeColor);
        }
    }

    public void CountAttackRange(GameObject gridObject, Person person, List<Person> friends)
    {
        if (person.SelectedAttackStyle.FixData.AttackKind == AttackStyleKind.Line)
        {
            var lineRange = CreateLineRange(person.RowCol, FightMain.instance.gridObjectToData[gridObject]);
            attackRange = RangeRemoveFriend(lineRange, friends);
            var realRange = new HashSet<GameObject>();
            foreach (var rc in attackRange)
            {
                if (attackDistance.Contains(rc))
                {
                    realRange.Add(rc);
                }
            }
            attackRange = realRange;
            realRange = null;
        }
        else if (person.SelectedAttackStyle.FixData.AttackKind == AttackStyleKind.Remote)
        {
            HashSet<Vector2Int> remoteRange =
                PersonMoveTool.CreateRange(FightMain.instance.gridObjectToData[gridObject], 
                person.SelectedAttackStyle.FixData.AttackRank);
            attackRange = RangeRemoveFriend(remoteRange, friends);
            attackRange.Add(gridObject);
        }
        else
        {
            HashSet<Vector2Int> range1 =
                PersonMoveTool.CreateRange(FightMain.instance.gridObjectToData[gridObject], 1);
            HashSet<Vector2Int> range2 = PersonMoveTool.CreateRange(person.RowCol, 1);
            HashSet<Vector2Int> range = new HashSet<Vector2Int>();
            foreach (var rc in range1)
            {
                if (range2.Contains(rc))
                {
                    range.Add(rc);
                }
            }
            attackRange = RangeRemoveFriend(range, friends);
            attackRange.Add(gridObject);
        }
    }

    public void ClearAttackDistance()
    {
        if(attackDistance != null)
        {
            foreach (var grid in attackDistance)
            {
                FightGridClick.SwitchGridColor(grid, FightGridClick.defaultColor);
            }
            attackDistance.Clear();
        }
    }

    public void ClearAttackRange()
    {
        if(attackRange != null)
        {
            foreach (var grid in attackRange)
            {
                if (attackDistance.Contains(grid))
                {
                    FightGridClick.SwitchGridColor(grid, FightGridClick.attackDistanceColor);
                }
                else
                {
                    FightGridClick.SwitchGridColor(grid, FightGridClick.defaultColor);
                }

            }
            attackRange.Clear();
        }
    }

    HashSet<GameObject> RangeRemoveFriend(HashSet<Vector2Int> gridRange, List<Person> friends)
    {
        HashSet<Vector2Int> obstacles = new HashSet<Vector2Int>();
        var range = new HashSet<GameObject>();
        foreach (var friend in friends)
        {
            obstacles.Add(friend.RowCol);
        }
        foreach (var rc in gridRange)
        {
            if (!obstacles.Contains(rc))
            {
                range.Add(FightMain.instance.gridDataToObject[rc]);
            }
        }
        return range;
    }

    public void ShowAttackDistance()
    {
        foreach (var grid in attackDistance)
        {
            FightGridClick.SwitchGridColor(grid, FightGridClick.attackDistanceColor); 
        }
    }

    HashSet<Vector2Int> CreateLineRange(Vector2Int startRc, Vector2Int endRc)
    {
        HashSet<Vector2Int> gridRanges = new HashSet<Vector2Int>();
        if (startRc.x % 2 == 0)//偶
        {
            if (startRc.x > endRc.x)//上
            {
                if (startRc.y <= endRc.y)//右
                {
                    Vector2Int current = startRc;
                    while (current.x >= 0 && current.x < FightMain.instance.mapHeight && current.y >= 0 && current.y < FightMain.instance.mapWidth)
                    {
                        gridRanges.Add(current);
                        current.x = current.x - 2;
                        current.y = current.y + 1;         
                    }
                    current.x = startRc.x-1;
                    current.y = startRc.y;
                    while (current.x >= 0 && current.x < FightMain.instance.mapHeight && current.y >= 0 && current.y < FightMain.instance.mapWidth)
                    {
                        gridRanges.Add(current);
                        current.x = current.x - 2;
                        current.y = current.y +1;
                    }
                }
                else//左
                {
                    Vector2Int current = startRc;
                    while (current.x >= 0 && current.x < FightMain.instance.mapHeight && current.y >= 0 && current.y < FightMain.instance.mapWidth)
                    {
                        gridRanges.Add(current);
                        current.x = current.x - 2;
                        current.y = current.y - 1;
                    }
                    current.x = startRc.x-1;
                    current.y = startRc.y - 1;
                    while (current.x >= 0 && current.x < FightMain.instance.mapHeight && current.y >= 0 && current.y < FightMain.instance.mapWidth)
                    {
                        gridRanges.Add(current);
                        current.x = current.x - 2;
                        current.y = current.y - 1;
                    }
                }
            }
            else if (startRc.x < endRc.x) // 下
            {
                if (startRc.y <= endRc.y)//右
                {
                    Vector2Int current = startRc;
   
                    while (current.x >= 0 && current.x < FightMain.instance.mapHeight && current.y >= 0 && current.y < FightMain.instance.mapWidth)
                    {
                        gridRanges.Add(current);
                        current.x = current.x + 2 ;
                        current.y = current.y + 1;
                    }
                    current.x = startRc.x + 1;
                    current.y = startRc.y;
                    while (current.x >= 0 && current.x < FightMain.instance.mapHeight && current.y >= 0 && current.y < FightMain.instance.mapWidth)
                    {
                        gridRanges.Add(current);
                        current.x = current.x +2;
                        current.y = current.y+1;
                    }
                }
                else//左
                {
                    Vector2Int current = startRc;
                    while (current.x >= 0 && current.x < FightMain.instance.mapHeight && current.y >= 0 && current.y < FightMain.instance.mapWidth)
                    {
                        gridRanges.Add(current);
                        current.x = current.x + 2;
                        current.y = current.y - 1;
                        
                    }
                    current.x = startRc.x + 1;
                    current.y = startRc.y - 1;
                    while (current.x >= 0 && current.x < FightMain.instance.mapHeight && current.y >= 0 && current.y < FightMain.instance.mapWidth)
                    {
                        gridRanges.Add(current);
                        current.x = current.x +2;
                        current.y = current.y-1;
                    }
                }
            }
            else //中
            {
                if (startRc.y > endRc.y)
                {
                    for (int i = 0; i < startRc.y; ++i)
                    {
                        gridRanges.Add(new Vector2Int(startRc.x, i));
                    }
                }
                else
                {
                    for (int i = startRc.y + 1; i < FightMain.instance.mapWidth; ++i)
                    {
                        gridRanges.Add(new Vector2Int(startRc.x, i));
                    }
                }
            }
        }
        else
        {
            if (startRc.x > endRc.x)//上
            {
                if (startRc.y < endRc.y)//右
                {
                    Vector2Int current = startRc;
                    while (current.x >= 0 && current.x < FightMain.instance.mapHeight && current.y >= 0 && current.y < FightMain.instance.mapWidth - 1)
                    {
                        gridRanges.Add(current);
                        current.x = current.x - 2;
                        current.y = current.y + 1;
                    }
                    current.x = startRc.x - 1;
                    current.y = startRc.y + 1;
                    while (current.x >= 0 && current.x < FightMain.instance.mapHeight && current.y >= 0 && current.y < FightMain.instance.mapWidth - 1)
                    {
                        gridRanges.Add(current);
                        current.x = current.x - 2;
                        current.y = current.y + 1;
                    }
                }
                else//左
                {
                    Vector2Int current = startRc;
                    while (current.x >= 0 && current.x < FightMain.instance.mapHeight && current.y >= 0 && current.y < FightMain.instance.mapWidth - 1)
                    {
                        gridRanges.Add(current);
                        current.x = current.x - 2;
                        current.y = current.y - 1;
                    }
                    current.x = startRc.x - 1;
                    current.y = startRc.y;
                    while (current.x >= 0 && current.x < FightMain.instance.mapHeight && current.y >= 0 && current.y < FightMain.instance.mapWidth - 1)
                    {
                        gridRanges.Add(current);
                        current.x = current.x - 2;
                        current.y = current.y-1;
                    }
                }
            }
            else if (startRc.x < endRc.x) // 下
            {
                if (startRc.y < endRc.y)//右
                {
                    Vector2Int current = startRc;
                    while (current.x >= 0 && current.x < FightMain.instance.mapHeight && current.y >= 0 && current.y < FightMain.instance.mapWidth - 1)
                    {
                        gridRanges.Add(current);
                        current.x = current.x + 2;
                        current.y = current.y + 1;
                    }
                    current.x = startRc.x + 1;
                    current.y = startRc.y + 1;
                    while (current.x >= 0 && current.x < FightMain.instance.mapHeight && current.y >= 0 && current.y < FightMain.instance.mapWidth - 1)
                    {
                        gridRanges.Add(current);
                        current.x = current.x +2;
                        current.y = current.y+1;
                    }
                }
                else//左
                {
                    Vector2Int current = startRc;
                    while (current.x >= 0 && current.x < FightMain.instance.mapHeight && current.y >= 0 && current.y < FightMain.instance.mapWidth - 1)
                    {
                        gridRanges.Add(current);
                        current.x = current.x + 2;
                        current.y = current.y - 1;
                        
                    }
                    current.x = startRc.x + 1;
                    current.y = startRc.y;
                    while (current.x >= 0 && current.x < FightMain.instance.mapHeight && current.y >= 0 && current.y < FightMain.instance.mapWidth - 1)
                    {
                        gridRanges.Add(current);
                        current.x = current.x +2;
                        current.y = current.y-1;  
                    }
                }
            }
            else //中
            {
                if (startRc.y > endRc.y)
                {
                    for (int i = 0; i < startRc.y; ++i)
                    {
                        gridRanges.Add(new Vector2Int(startRc.x, i));
                    }
                }
                else
                {
                    for (int i = startRc.y + 1; i < FightMain.instance.mapWidth - 1; ++i)
                    {
                        gridRanges.Add(new Vector2Int(startRc.x, i));
                    }
                }
            }
        }
        return gridRanges;
    }
}
