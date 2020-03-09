using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonBaseData
{
    public int Id { get; set; } //编号
    public string Name { get; set; } //名字
    public int Bi { get; set; }
    public int Gen { get; set; }
    public int Wu { get; set; }
    public int Shen { get; set; }
    public int Jin { get; set; }
    public int MedicalSkill { get; set; }
    public int CookingSkill { get; set; }
    public int LiquorSkill { get; set; }
    public int HP { get; set; }
    public int MP { get; set; }
    public int Energy { get; set; }
    public string InitPlaceString { get; set; }
    public int HeadPortrait { get; set; }
    public int WeaponId { get; set; }
    public List<Interaction> Interactions { get; set; }
    public List<ChatConversation> ChatConversations { get; set; }
    public List<AttackStyle> AttackStyles { get; set; }
    public List<InnerGong> InnerGongs { get; set; }
}

