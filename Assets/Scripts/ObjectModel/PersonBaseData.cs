using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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
    [System.NonSerialized]
    private List<ChatConversation> chatConversations;
    public List<ChatConversation> ChatConversations { get { return chatConversations; } set { chatConversations = value; } }
    //[System.NonSerialized]
    private List<AttackStyle> attackStyles;
    public List<AttackStyle> AttackStyles { get { return attackStyles; } set { attackStyles = value; } }
    //[System.NonSerialized]
    private List<InnerGong> innerGongs;
    public List<InnerGong> InnerGongs { get { return innerGongs; } set { innerGongs = value; } }
}

