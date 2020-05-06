using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public int Number { get; set; }
    public string RealTime { get; set; }
    public List<Person> Persons { get; set; }
    public int PlayerEquipWeaponId { get; set; }
    public Dictionary<int, int> Belongings { get; set; }
    public List<int> Teammates { get; set; }
    public GameDate Date { get; set; }
    public string CurrentPlace { get; set; }
    public List<int> PlayerRc { get; set; }
    public int Money { get; set; }
    public int Experspance { get; set; }

    public SaveData(int number)
    {
        Number = number;
        RealTime = DateTime.Now.ToString();
        Persons = GlobalData.Persons;
        var rData = GameRunningData.GetRunningData();
        PlayerEquipWeaponId = rData.player.EquippedWeapon == null ? -1 : rData.player.EquippedWeapon.Id;
        Belongings = new Dictionary<int, int>();
        Teammates = new List<int>();
        foreach(Good good in rData.belongings)
        {
            Belongings.Add(good.Id, good.Number);
        }
        foreach(Person p in rData.teammates)
        {
            Teammates.Add(p.BaseData.Id);
        }
        if(rData.currentPlace == null)
        {
            CurrentPlace = "";
        }
        else
        {
            CurrentPlace = rData.currentPlace.GetPlaceString();
        }
        Date = rData.date;
        PlayerRc = new List<int> { rData.playerPreRc.x, rData.playerPreRc.y };
        Money = rData.money;
        Experspance = rData.experspance;
    }

    public void ResumeData()
    {
        for(int i = 0; i < GlobalData.Persons.Count; ++i)
        {
            Person p1 = GlobalData.Persons[i];
            Person p2 = Persons[i];
            p2.EquippedWeapon = p1.EquippedWeapon;
            p2.BaseData.ChatConversations = p1.BaseData.ChatConversations;
            foreach(var attackStyle in p2.BaseData.AttackStyles)
            {
                attackStyle.FixData = GlobalData.StyleFixDatas[attackStyle.Id];
                if(attackStyle.Id == p2.SelectedAttackStyle.Id)
                {
                    p2.SelectedAttackStyle = attackStyle;
                }
            }
            foreach (var innerGong in p2.BaseData.InnerGongs)
            {
                innerGong.FixData = GlobalData.InnerGongFixDatas[innerGong.Id];
                if(innerGong.Id == p2.SelectedInnerGong.Id)
                {
                    p2.SelectedInnerGong = innerGong;
                }
            }
            var interactions = new List<Interaction>();
            foreach(var interaction in p2.BaseData.Interactions)
            {
                interactions.Add(GlobalData.Interactions[interaction.Id]);
            }
            p2.BaseData.Interactions = interactions;
        }
        GlobalData.Persons = Persons;
        var player = GlobalData.Persons[0];
        player.EquippedWeapon = PlayerEquipWeaponId == -1 ? null : GlobalData.Items[PlayerEquipWeaponId];
        GameRunningData.GetRunningData().player = player;
        GameRunningData.GetRunningData().date = Date;
        GameRunningData.GetRunningData().money = Money;
        GameRunningData.GetRunningData().experspance = Experspance;
        GameRunningData.GetRunningData().playerPreRc = new Vector2Int(PlayerRc[0], PlayerRc[1]);
        var items = new List<Good>();
        foreach(var key in Belongings.Keys)
        {
            var item = GlobalData.Items[key];
            item.Number = Belongings[key];
            items.Add(item);
        }
        GameRunningData.GetRunningData().belongings = items;
        var team = new List<Person>();
        foreach(var id in Teammates)
        {
            team.Add(GlobalData.Persons[id]);
        }
        GameRunningData.GetRunningData().teammates = team;
        if(CurrentPlace == "")
        {
            GameRunningData.GetRunningData().currentPlace = null;
        }
        else if (CurrentPlace.Contains("-"))
        {
            string[] splits = CurrentPlace.Split('-');
            var place = GlobalData.SecondPlaces[int.Parse(splits[1])];
            place.PrePlace = GlobalData.FirstPlaces[int.Parse(splits[0])];
            GameRunningData.GetRunningData().currentPlace = place;
        }
        else
        {
            GameRunningData.GetRunningData().currentPlace = GlobalData.FirstPlaces[int.Parse(CurrentPlace)];
        }
        GameRunningData.GetRunningData().ReturnToMap();
    }
}
