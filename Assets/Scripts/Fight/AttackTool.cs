using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTool
{
    public static HashSet<GameObject> attackDistance = new HashSet<GameObject>();
    public static HashSet<GameObject> attackRange = new HashSet<GameObject>();

    public static void CountAttackDistance(Person person, List<Person> friends)
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
                distanceRange = PersonMoveTool.CreateRange(person.RowCol, person.SelectedAttackStyle.FixData.AttackRank);
                attackRange = RangeRemoveFriend(distanceRange, friends);
                attackDistance.Clear();
                HashSet<Person> canAttackEnemys = new HashSet<Person>();
                foreach (Person enemy in FightMain.enemyQueue)
                {
                    GameObject gridObject = FightMain.gridDataToObject[enemy.RowCol];
                    if (attackRange.Contains(gridObject))
                    {
                        canAttackEnemys.Add(enemy);
                    }
                }
                if (canAttackEnemys.Count > 0)
                {
                    AttackAction(person, canAttackEnemys);
                    FightMain.instance.PlayerFinished();
                    FightGUI.HideBattlePane();
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

    public static void PlayAttackMusic(Person person)
    {
        switch (person.SelectedAttackStyle.FixData.WeaponKind)
        {
            case AttackWeaponKind.Knife:
                SoundEffectControl.instance.PlaySoundEffect(1);
                break;
            case AttackWeaponKind.Sword:
                SoundEffectControl.instance.PlaySoundEffect(2);
                break;
            case AttackWeaponKind.Rod:
                SoundEffectControl.instance.PlaySoundEffect(3);
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

    public static bool AttackEnemys(Person attacker, List<Person> enemys)
    {
        HashSet<Person> canAttackEnemys = new HashSet<Person>();
        foreach (Person enemy in enemys)
        {
            GameObject gridObject = FightMain.gridDataToObject[enemy.RowCol];
            if (attackRange.Contains(gridObject))
            {
                canAttackEnemys.Add(enemy);
            }
        }
        return AttackAction(attacker, canAttackEnemys);
    }

    private static bool AttackAction(Person attacker, HashSet<Person> canAttackEnemys)
    {
        if (canAttackEnemys.Count > 0)
        {
            AttackStyle attackStyle = attacker.SelectedAttackStyle;
            int mpCost = attackStyle.GetRealMPCost();
            if (mpCost > attacker.CurrentMP)
            {
                return false;
            }
            PersonChangeMP(attacker, mpCost, false);

            PromoteStyleProficiency(attacker);
            AttackBuffTool.PersonGetBuff(attacker);

            foreach (Person enemy in canAttackEnemys)
            {
                AttackBuffTool.EnemyGetDeBuff(attacker, enemy);
                GongBuffTool.SixOne(attacker, enemy);
                GongBuffTool.EightSix(attacker, enemy);
                int value = CountHPLoseValue(attacker, enemy);
                value = GongBuffTool.ThreeGradeReduceInjury(enemy, value);
                value = GongBuffTool.OneSix(enemy, value);
                value = GongBuffTool.TwoOne(enemy, value);
                value = GongBuffTool.TriggerFiveTen(enemy, value);
                GongBuffTool.OneTen(attacker, enemy);
                GongBuffTool.EightTen(attacker, enemy, value);
                if (PersonChangeHP(enemy, value, false))
                {
                    GongBuffTool.TwoTen(attacker);
                }
                AttackBuffTool.TriggerReboundBuff(attacker, enemy, value);
                AttackBuffTool.TriggerAbsorbBuff(attacker, enemy);

                float angle = PersonMoveTool.GetAngle(enemy.PersonObject.transform.position,
                    attacker.PersonObject.transform.position);
                FightMain.RotatePerson(enemy, angle);
            }

            AttackBuffTool.TriggerDubleHitBuff(attacker, canAttackEnemys);
            FightMain.OneRoundOver(attacker);
            PlayAttackMusic(attacker);
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
        if (!AttackBuffTool.IsPersonHasInvincibleBuff(enemy))
        {
            if (!GongBuffTool.ThirteenOne(attacker) && 
                !GongBuffTool.FourOne(attacker) && ComputeProbability(enemy.Dodge + (100 - attacker.Accuracy)))
            {
                GongBuffTool.ZeroSix(enemy);
                GongBuffTool.FiveSix(enemy);
                GongBuffTool.SixSix(enemy);
                GongBuffTool.TwentyfourSix(enemy);
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
                    GongBuffTool.ElevenTen(attacker);
                    GongBuffTool.TwelveSix(attacker, enemy);
                    SpecialEffectTool.instance.RateEffect(attacker, "暴击");
                }

                if (!GongBuffTool.FourSix(attacker) && ComputeProbability(enemy.Counterattack))
                {
                    GongBuffTool.SixteenTen(enemy);
                    GongBuffTool.TwentyfiveSix(enemy);
                    SpecialEffectTool.instance.RateEffect(enemy, "反击");
                }
            }
        }
        return value;
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
        GongBuffTool.HPBuffTrigger(person);
        return false;
    }

    public static void PersonChangeMP(Person person, int value, bool isAdd)
    {
        person.ChangeMP(value, isAdd);
        GongBuffTool.SevenTen(person);
    }

    private static void PersonDead(Person person)
    {
        Object.Destroy(person.PersonObject);
        FightMain.DestoryHPSplider(person);
        if (FightMain.friendQueue.Contains(person))
        {
            FightMain.friendQueue.Remove(person);
            if(FightMain.friendQueue.Count == 0)
            {
                FightMain.isFail = true;
            }
        }
        if (FightMain.enemyQueue.Contains(person))
        {
            FightMain.enemyQueue.Remove(person);
            if (FightMain.enemyQueue.Count == 0)
            {
                FightMain.isSuccess = true;
            }
        }
    }

    public static void ShowAttackRange()
    {
        foreach (var grid in attackRange)
        {
            FightGridClick.SwitchGridColor(grid, FightGridClick.attackRangeColor);
        }
    }

    public static void CountAttackRange(GameObject gridObject, Person person, List<Person> friends)
    {
        if (person.SelectedAttackStyle.FixData.AttackKind == AttackStyleKind.Line)
        {
            var lineRange = CreateLineRange(person.RowCol, FightMain.gridObjectToData[gridObject]);
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
                PersonMoveTool.CreateRange(FightMain.gridObjectToData[gridObject], 
                person.SelectedAttackStyle.FixData.AttackRank);
            attackRange = RangeRemoveFriend(remoteRange, friends);
            attackRange.Add(gridObject);
        }
        else
        {
            HashSet<Vector2Int> range1 =
                PersonMoveTool.CreateRange(FightMain.gridObjectToData[gridObject], 1);
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

    public static void ClearAttackDistance()
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

    public static void ClearAttackRange()
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

    static HashSet<GameObject> RangeRemoveFriend(HashSet<Vector2Int> gridRange, List<Person> friends)
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
                range.Add(FightMain.gridDataToObject[rc]);
            }
        }
        return range;
    }

    public static void ShowAttackDistance()
    {
        foreach (var grid in attackDistance)
        {
            FightGridClick.SwitchGridColor(grid, FightGridClick.attackDistanceColor); 
        }
    }

    static HashSet<Vector2Int> CreateLineRange(Vector2Int startRc, Vector2Int endRc)
    {
        HashSet<Vector2Int> gridRanges = new HashSet<Vector2Int>();
        if (startRc.x % 2 == 0)//偶
        {
            if (startRc.x > endRc.x)//上
            {
                if (startRc.y <= endRc.y)//右
                {
                    Vector2Int current = startRc;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth)
                    {
                        gridRanges.Add(current);
                        current.x = current.x - 2;
                        current.y = current.y + 1;         
                    }
                    current.x = startRc.x-1;
                    current.y = startRc.y;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth)
                    {
                        gridRanges.Add(current);
                        current.x = current.x - 2;
                        current.y = current.y +1;
                    }
                }
                else//左
                {
                    Vector2Int current = startRc;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth)
                    {
                        gridRanges.Add(current);
                        current.x = current.x - 2;
                        current.y = current.y - 1;
                    }
                    current.x = startRc.x-1;
                    current.y = startRc.y - 1;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth)
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
   
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth)
                    {
                        gridRanges.Add(current);
                        current.x = current.x + 2 ;
                        current.y = current.y + 1;
                    }
                    current.x = startRc.x + 1;
                    current.y = startRc.y;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth)
                    {
                        gridRanges.Add(current);
                        current.x = current.x +2;
                        current.y = current.y+1;
                    }
                }
                else//左
                {
                    Vector2Int current = startRc;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth)
                    {
                        gridRanges.Add(current);
                        current.x = current.x + 2;
                        current.y = current.y - 1;
                        
                    }
                    current.x = startRc.x + 1;
                    current.y = startRc.y - 1;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth)
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
                    for (int i = startRc.y + 1; i < FightMain.mapWidth; ++i)
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
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth - 1)
                    {
                        gridRanges.Add(current);
                        current.x = current.x - 2;
                        current.y = current.y + 1;
                    }
                    current.x = startRc.x - 1;
                    current.y = startRc.y + 1;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth - 1)
                    {
                        gridRanges.Add(current);
                        current.x = current.x - 2;
                        current.y = current.y + 1;
                    }
                }
                else//左
                {
                    Vector2Int current = startRc;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth - 1)
                    {
                        gridRanges.Add(current);
                        current.x = current.x - 2;
                        current.y = current.y - 1;
                    }
                    current.x = startRc.x - 1;
                    current.y = startRc.y;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth - 1)
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
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth - 1)
                    {
                        gridRanges.Add(current);
                        current.x = current.x + 2;
                        current.y = current.y + 1;
                    }
                    current.x = startRc.x + 1;
                    current.y = startRc.y + 1;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth - 1)
                    {
                        gridRanges.Add(current);
                        current.x = current.x +2;
                        current.y = current.y+1;
                    }
                }
                else//左
                {
                    Vector2Int current = startRc;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth - 1)
                    {
                        gridRanges.Add(current);
                        current.x = current.x + 2;
                        current.y = current.y - 1;
                        
                    }
                    current.x = startRc.x + 1;
                    current.y = startRc.y;
                    while (current.x >= 0 && current.x < FightMain.mapHeight && current.y >= 0 && current.y < FightMain.mapWidth - 1)
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
                    for (int i = startRc.y + 1; i < FightMain.mapWidth - 1; ++i)
                    {
                        gridRanges.Add(new Vector2Int(startRc.x, i));
                    }
                }
            }
        }
        return gridRanges;
    }
}
