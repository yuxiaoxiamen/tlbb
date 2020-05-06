using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Person : ICloneable
{
    //[NonSerialized]
    private AttackStyle selectedAttackStyle;
    public AttackStyle SelectedAttackStyle { get { return selectedAttackStyle; } set { selectedAttackStyle = value; } }
    [NonSerialized]
    private List<AttackBuff> attackBuffs;
    public List<AttackBuff> AttackBuffs { get { return attackBuffs; } set { attackBuffs = value; } }
    [NonSerialized]
    private GongBuff gongBuff;
    public GongBuff GongBuff { get { return gongBuff; } set{ gongBuff = value; } }
    //[NonSerialized]
    private InnerGong selectedInnerGong;
    public InnerGong SelectedInnerGong { get { return selectedInnerGong; } set { selectedInnerGong = value; } }
    [NonSerialized]
    private Vector2Int rowCol;
    public Vector2Int RowCol { get { return rowCol; } set { rowCol = value; } }
    [NonSerialized]
    private GameObject personObject;
    public GameObject PersonObject { get { return personObject; } set { personObject = value; } }
    [NonSerialized]
    private BattleControlState controlState;
    public BattleControlState ControlState { get { return controlState; } set { controlState = value; } }
    public int CurrentHP { get; set; }
    public int CurrentMP { get; set; }
    public int CurrentEnergy { get; set; }
    public int Crit { get; set; } //暴击率
    public int Counterattack { get; set; } //反击率
    public int Dodge { get; set; } //闪避率
    public int Accuracy { get; set; } //命中率
    public int Defend { get; set; }
    public int AttackPowerRate { get; set; } //攻击强度提升比例
    public int MoveRank { get; set; }
    public string CurrentPlaceString { get; set; }
    public bool IsMoved { get; set; }
    [NonSerialized]
    private Good equippedWeapon;
    public Good EquippedWeapon { get { return equippedWeapon; } set { equippedWeapon = value; } }
    public PersonBaseData BaseData { get; set; }

    public Person()
    {
        IsMoved = false;
        MoveRank = 3;
        ControlState = BattleControlState.Moving;
        AttackBuffs = new List<AttackBuff>();
        //测试数据
        //EquippedWeapon = GlobalData.Items[56];
    }

    public void InitAttribute()
    {
        Crit = CountCrit();
        Counterattack = CountCounterattack();
        Dodge = CountDodge();
        Defend = CountDefend();
        MoveRank = CountMoveRank();
        Accuracy = 100;
        AttackPowerRate = 0;
    }

    public int CountCrit()
    {
        return BaseData.Bi / 2;
    }

    public int CountCounterattack()
    {
        return BaseData.Gen / 2;
    }

    public int CountDodge()
    {
        return BaseData.Shen / 2;
    }

    public int CountMoveRank()
    {
        return BaseData.Shen / 15;
    }

    public int CountDefend()
    {
        return BaseData.Jin;
    }

    public void UpdatePlace(string placeString)
    {
        CurrentPlaceString = placeString;
    }

    public void ChangeHP(int value, bool isAdd)
    {
        if (isAdd)
        {
            CurrentHP += value;
            if (CurrentHP >= BaseData.HP)
            {
                CurrentHP = BaseData.HP;
            }
        }
        else
        {
            CurrentHP -= value;
            if (CurrentHP <= 0)
            {
                CurrentHP = 0;
            }
        }
    }

    public void ChangeMP(int value, bool isAdd)
    {
        if (isAdd)
        {
            CurrentMP += value;
            if (CurrentMP >= BaseData.MP)
            {
                CurrentMP = BaseData.MP;
            }
        }
        else
        {
            CurrentMP -= value;
            if (CurrentMP <= 0)
            {
                CurrentMP = 0;
            }
        }
    }

    public void ChangeEnergy(int value, bool isAdd)
    {
        if (isAdd)
        {
            CurrentEnergy += value;
            if (CurrentEnergy >= BaseData.Energy)
            {
                CurrentEnergy = BaseData.Energy;
            }
        }
        else
        {
            CurrentEnergy -= value;
            if (CurrentEnergy <= 0)
            {
                CurrentEnergy = 0;
            }
        }
    }

    public int MedicalSkillResumeHP()
    {
        return (int)(BaseData.MedicalSkill / GameConfig.MaxMedicalSkill * 1.0f * GameConfig.PerMedicalSkillResume);
    }

    public int CountStyleExperience()
    {
        return (int)(GameConfig.PerStyleExperience * (BaseData.Wu * GameConfig.WuXingAddition * 1.0f / 100));
    }

    public int CountGongExperience()
    {
        return (int)(GameConfig.PerGongExperience * (BaseData.Wu * GameConfig.WuXingAddition * 1.0f / 100));
    }

    public void PromoteGong()
    {
        if (SelectedInnerGong.AddExperience(CountGongExperience()))
        {
            BaseAttributeIncrease(SelectedInnerGong.FixData.PerHPGain, 
                SelectedInnerGong.FixData.PerMPGain, SelectedInnerGong.FixData.PerTalentGain);
            if(SelectedInnerGong.Rank == GameConfig.MaxRank)
            {
                BaseAttributeIncrease(SelectedInnerGong.FixData.FullHPGain, 
                    SelectedInnerGong.FixData.FullMPGain, SelectedInnerGong.FixData.FullTalentGain);
            }
        }
    }

    public int PromoteLiquorSkill()
    {
        int x = UnityEngine.Random.Range(3, 8);
        BaseData.LiquorSkill += x;
        return x;
    }

    public int PromoteCookingSkill()
    {
        int x = UnityEngine.Random.Range(3, 8);
        BaseData.CookingSkill += x;
        return x;
    }

    public int PromoteMedicalSkill()
    {
        int x = UnityEngine.Random.Range(3, 8);
        BaseData.MedicalSkill += x;
        return x;
    }

    private void BaseAttributeIncrease(int hp, int mp, List<Talent> talents)
    {
        BaseData.HP += hp;
        BaseData.MP += mp;
        foreach (Talent talent in talents)
        {
            switch (talent.Name)
            {
                case TalentName.Bi:
                    BaseData.Bi += talent.Number;
                    break;
                case TalentName.Gen:
                    BaseData.Gen += talent.Number;
                    break;
                case TalentName.Wu:
                    BaseData.Wu += talent.Number;
                    break;
                case TalentName.Shen:
                    BaseData.Shen += talent.Number;
                    break;
                case TalentName.Jing:
                    BaseData.Jin += talent.Number;
                    break;
            }
        }
    }

    public void PromoteAttackStyle()
    {
        foreach(AttackStyle style in BaseData.AttackStyles)
        {
            style.AddExperience(CountStyleExperience());
        }
    }

    public void HpMpEnergyChange()
    {
        int value = (int)(BaseData.HP * 0.15f);
        ChangeHP(value, true);
        value = (int)(BaseData.MP * 0.15f);
        ChangeMP(value, true);
        ChangeEnergy(5, false);
    }

    public object Clone()
    {
        object obj = MemberwiseClone();
        return obj;
    }
}
public enum BattleControlState { Moving, Moved, End, Attacking, Treating };
