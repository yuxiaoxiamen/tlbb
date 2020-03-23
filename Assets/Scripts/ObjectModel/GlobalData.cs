using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GlobalData
{
    public static List<Person> Persons { get; set; } //所有人物实例
    public static List<AttackStyleFixData> StyleFixDatas { get; set; } //招式的固定数据
    public static List<AttackStyleEffect> StyleEffects { get; set; } //所有招式的效果数据
    public static List<InnerGongFixData> InnerGongFixDatas { get; set; } //所有内功固定数据
    public static List<Good> Items { get; set; } //所有物品数据
    public static List<WeaponManual> WeaponManuals { get; set; }//武器谱
    public static List<FoodManual> FoodManuals { get; set; }//菜谱
    public static List<FirstPlace> FirstPlaces { get; set; } //所有一级地图中地点数据
    public static List<SecondPlace> SecondPlaces { get; set; } //所有场所的数据
    public static Dictionary<string, List<MainConversation>> MainConversations { get; set; } //主线剧情对话
    public static List<Interaction> Interactions { get; set; }//交互选项
    public static Dictionary<string, MainLineConflict> MainLineConflicts { get; set; }

    static GlobalData()
    {
        Persons = new List<Person>();
        ReadItemData();
        ReadWeaponManualData();
        ReadFoodManualData();
        ReadInteractionData();
        ReadSecondPlaceData();
        ReadFirstPlaceData();
        ReadInnerGongData();
        ReadStyleEffectData();
        ReadStyleData();
        ReadPersonData();
        ReadChatDialogueData();
        ReadMainDialogueData();
        ReadMainLineConflictData();
        foreach(Person person in Persons)
        {
            if(person.BaseData.Id < 65)
            {
                TimeGoSubject.GetTimeSubject().Attach(person);
            }
        }
    }

    public static void Init()
    {

    }

    static void ReadMainLineConflictData()
    {
        var jsonData = Resources.Load<TextAsset>("jsonData/主线冲突");
        List<MainLineConflict> mainLineConflicts = JsonConvert.DeserializeObject<List<MainLineConflict>>(jsonData.text);
        List<MainLineConflictJson> conflictJsons = JsonConvert.DeserializeObject<List<MainLineConflictJson>>(jsonData.text);
        MainLineConflicts = new Dictionary<string, MainLineConflict>();
        for (int i = 0; i < conflictJsons.Count; ++i)
        {
            var mainLineConflict = mainLineConflicts[i];
            mainLineConflict.ZFriends = RevertConflictString(conflictJsons[i].ZFriendsString);
            mainLineConflict.ZEnemys = RevertConflictString(conflictJsons[i].ZEnemysString);
            mainLineConflict.FFriends = RevertConflictString(conflictJsons[i].FFriendsString);
            mainLineConflict.FEnemys = RevertConflictString(conflictJsons[i].FEnemysString);
            var key = conflictJsons[i].PlaceString + "/" + conflictJsons[i].DateString;
            MainLineConflicts.Add(key, mainLineConflict);
        }
    }

    private static List<Person> RevertConflictString(string s)
    {
        List<Person> persons = new List<Person>();
        if(s != "")
        {
            string[] ss = s.Split(',');
            foreach (string sid in ss)
            {
                if (sid.Contains("*"))
                {
                    string[] splits = sid.Split('*');
                    int num = int.Parse(splits[0]);
                    int id = int.Parse(splits[1]);
                    for(int i = 1; i < num; ++i)
                    {
                        persons.Add(CopyAPerson(Persons[id]));
                    }
                    persons.Add(Persons[id]);
                }
                else
                {
                    persons.Add(Persons[int.Parse(sid)]);
                }
            }
        }
        return persons;
    }

    private static Person CopyAPerson(Person p)
    {
        return (Person)p.Clone();
    }

    static void ReadInteractionData()
    {
        var jsonData = Resources.Load<TextAsset>("jsonData/交互选项");
        Interactions = JsonConvert.DeserializeObject<List<Interaction>>(jsonData.text);
    }

    static void ReadChatDialogueData()
    {
        Dictionary<int, List<ChatConversation>> ChatConversations = new Dictionary<int, List<ChatConversation>>();
        var jsonData = Resources.Load<TextAsset>("jsonData/交谈对话");
        var conversations = JsonConvert.DeserializeObject<List<ChatConversation>>(jsonData.text);
        List<ConversationJson> conversationJsons = JsonConvert.DeserializeObject<List<ConversationJson>>(jsonData.text);
        for (int i = 0; i < conversationJsons.Count; ++i)
        {
            var conversationJson = conversationJsons[i];
            ChatConversation conversation = conversations[i];
            conversation.People = Persons[conversationJson.PeopleId];
        }

        foreach (var conversation in conversations)
        {
            var key = conversation.BelongPersonId;
            if (!ChatConversations.ContainsKey(key))
            {
                ChatConversations.Add(key, new List<ChatConversation>());
            }
            ChatConversations[key].Add(conversation);
        }

        foreach(var key in ChatConversations.Keys)
        {
            Persons[key].BaseData.ChatConversations = ChatConversations[key];
        }
    }

    static void ReadMainDialogueData()
    {
        var jsonData = Resources.Load<TextAsset>("jsonData/主线对话");
        var conversations = JsonConvert.DeserializeObject<List<MainConversation>>(jsonData.text);
        List<ConversationJson> conversationJsons = JsonConvert.DeserializeObject<List<ConversationJson>>(jsonData.text);
        for (int i = 0; i < conversationJsons.Count; ++i)
        {
            var conversationJson = conversationJsons[i];
            MainConversation conversation = conversations[i];
            conversation.People = Persons[conversationJson.PeopleId];
        }
        MainConversations = new Dictionary<string, List<MainConversation>>();
        foreach(var conversation in conversations)
        {
            var key = conversation.PlaceString + "/" + conversation.DateString;
            if (!MainConversations.ContainsKey(key))
            {
                MainConversations.Add(key, new List<MainConversation>());
            }
            MainConversations[key].Add(conversation);
        }
    }

    static void ReadSecondPlaceData()
    {
        var jsonData = Resources.Load<TextAsset>("jsonData/场所");
        SecondPlaces = JsonConvert.DeserializeObject<List<SecondPlace>>(jsonData.text);
    }

    static void ReadFirstPlaceData()
    {
        var jsonData = Resources.Load<TextAsset>("jsonData/地点");
        FirstPlaces = JsonConvert.DeserializeObject<List<FirstPlace>>(jsonData.text);
        List<PlaceJson> placeJsons = JsonConvert.DeserializeObject<List<PlaceJson>>(jsonData.text);
        for(int i = 0; i < placeJsons.Count; ++i)
        {
            var placeJson = placeJsons[i];
            string[] entryDatas = placeJson.EntryData.Split('_');
            FirstPlaces[i].Entry = new Vector2Int(int.Parse(entryDatas[0]), int.Parse(entryDatas[1]));
            string[] holdDatas = placeJson.HoldData.Split(',');
            FirstPlaces[i].Hold = new List<Vector2Int>();

            if(placeJson.SiteData != "无")
            {
                string[] siteDatas = placeJson.SiteData.Split(',');
                FirstPlaces[i].Sites = new List<SecondPlace>();
                foreach(var siteData in siteDatas)
                {
                    FirstPlaces[i].Sites.Add(SecondPlaces[int.Parse(siteData)]);
                }
            }
            
            foreach (var holdData in holdDatas)
            {
                string[] holds = holdData.Split('_');
                FirstPlaces[i].Hold.Add(new Vector2Int(int.Parse(holds[0]), int.Parse(holds[1])));
            }
        }
    }

   static void ReadStyleData()
   {
        var jsonData = Resources.Load<TextAsset>("jsonData/招式");
        StyleFixDatas = JsonConvert.DeserializeObject<List<AttackStyleFixData>>(jsonData.text);
        List<EffectsJson> effects = JsonConvert.DeserializeObject<List<EffectsJson>>(jsonData.text);
        for(int i = 0; i < effects.Count; ++i)
        {
            string effect = effects[i].StyleEffects;
            if (!effect.Equals("无"))
            {
                string[] es = effect.Split(',');
                List<AttackStyleEffect> attackStyleEffects = new List<AttackStyleEffect>();
                foreach(var e in es)
                {
                    attackStyleEffects.Add(StyleEffects[int.Parse(e)]);
                }
                StyleFixDatas[i].Effects = attackStyleEffects;
            }
            else
            {
                StyleFixDatas[i].Effects = new List<AttackStyleEffect>();
            }
        }
    }

    static void ReadStyleEffectData()
    {
        var jsonData = Resources.Load<TextAsset>("jsonData/招式效果");
        StyleEffects = JsonConvert.DeserializeObject<List<AttackStyleEffect>>(jsonData.text);
    }

    static void ReadItemData()
    {
        var jsonData = Resources.Load<TextAsset>("jsonData/物品");
        Items = JsonConvert.DeserializeObject<List<Good>>(jsonData.text);
    }

    static void ReadWeaponManualData()
    {
        var jsonData = Resources.Load<TextAsset>("jsonData/武器谱");
        WeaponManuals = JsonConvert.DeserializeObject<List<WeaponManual>>(jsonData.text);
        var manualIds = JsonConvert.DeserializeObject<List<ManualJson>>(jsonData.text);
        for(int i = 0; i < WeaponManuals.Count; ++i)
        {
            WeaponManuals[i].Item = Items[manualIds[i].ItemId];
        }
    }

    static void ReadFoodManualData()
    {
        var jsonData = Resources.Load<TextAsset>("jsonData/菜谱");
        FoodManuals = JsonConvert.DeserializeObject<List<FoodManual>>(jsonData.text);
        var manualIds = JsonConvert.DeserializeObject<List<ManualJson>>(jsonData.text);
        for (int i = 0; i < FoodManuals.Count; ++i)
        {
            FoodManuals[i].Item = Items[manualIds[i].ItemId];
        }
    }

    static void ReadInnerGongData()
    {
        var jsonData = Resources.Load<TextAsset>("jsonData/内功");
        InnerGongFixDatas = JsonConvert.DeserializeObject<List<InnerGongFixData>>(jsonData.text);
        List<GongJson> gongJsons = JsonConvert.DeserializeObject<List<GongJson>>(jsonData.text);
        for(int i = 0; i < gongJsons.Count; ++i)
        {
            string pts = gongJsons[i].PerTalent;
            if (!pts.Equals("无"))
            {
                string[] ps = pts.Split(',');
                List<Talent> perTalents = new List<Talent>();
                foreach (var p in ps)
                {
                    string[] ts = p.Split('*');
                    Talent talent = new Talent()
                    {
                        Name = (TalentName)Enum.Parse(typeof(TalentName), ts[0], true),
                        Number = int.Parse(ts[1])
                    };
                    perTalents.Add(talent);
                }
                InnerGongFixDatas[i].PerTalentGain = perTalents;
            }
            else
            {
                InnerGongFixDatas[i].PerTalentGain = new List<Talent>();
            }

            string fts = gongJsons[i].FullTalent;
            if (!fts.Equals("无"))
            {
                string[] fs = fts.Split(',');
                List<Talent> fullTalents = new List<Talent>();
                foreach (var f in fs)
                {
                    string[] ts = f.Split('*');
                    Talent talent = new Talent()
                    {
                        Name = (TalentName)Enum.Parse(typeof(TalentName), ts[0], true),
                        Number = int.Parse(ts[1])
                    };
                    fullTalents.Add(talent);
                }
                InnerGongFixDatas[i].FullTalentGain = fullTalents;
            }
            else
            {
                InnerGongFixDatas[i].FullTalentGain = new List<Talent>();
            }
        }
    }

    static void ReadPersonData()
    {
        var jsonData = Resources.Load<TextAsset>("jsonData/人物");
        var personBaseData = JsonConvert.DeserializeObject<List<PersonBaseData>>(jsonData.text);
        var personJsons = JsonConvert.DeserializeObject<List<PersonsJson>>(jsonData.text);
        for (int i = 0; i < personBaseData.Count; ++i)
        {
            PersonBaseData p = personBaseData[i];
            string[] styles = personJsons[i].Styles.Split(',');
            string[] gongs = personJsons[i].Gongs.Split(',');
            string[] interactionDatas = personJsons[i].InteractionDatas.Split(',');
            List<AttackStyle> attackStyleDatas = new List<AttackStyle>();
            List<InnerGong> innerGongs = new List<InnerGong>();
            List<Interaction> interactions = new List<Interaction>();
            for(int j = 0; j < styles.Length; ++j)
            {
                AttackStyle attackStyleData = new AttackStyle()
                {
                    FixData = StyleFixDatas[int.Parse(styles[j])]
                };
                attackStyleDatas.Add(attackStyleData);
            }
            for (int j = 0; j < gongs.Length; ++j)
            {
                InnerGong innerGongData = new InnerGong()
                {
                    FixData = InnerGongFixDatas[int.Parse(gongs[j])]
                };
                innerGongs.Add(innerGongData);
            }
            for (int j = 0; j < interactionDatas.Length; ++j)
            {
                interactions.Add(Interactions[int.Parse(interactionDatas[j])]);
            }
            p.AttackStyles = attackStyleDatas;
            p.InnerGongs = innerGongs;
            p.Interactions = interactions;
            Person person = new Person()
            {
                BaseData = p,
                SelectedAttackStyle = attackStyleDatas[0],
                SelectedInnerGong = innerGongs[0],
                CurrentHP = p.HP,
                CurrentMP = p.MP,
                CurrentEnergy = p.Energy,
                CurrentPlaceString = p.InitPlaceString,
                EquippedWeapon = Items[p.WeaponId]
            };
            Persons.Add(person);
        }
    }
}

class ConversationJson
{
    public int PeopleId { get; set; }
}

class PlaceJson
{
    public string EntryData { get; set; }
    public string HoldData { get; set; }
    public string SiteData { get; set; }
}

class GongJson
{
    public string PerTalent { get; set; }
    public string FullTalent { get; set; }
}

class PersonsJson
{
    public string Styles { get; set; }
    public string Gongs { get; set; }
    public string InteractionDatas { get; set; }
}

class EffectsJson
{
    public string StyleEffects { get; set; }
}

class MainLineConflictJson
{
    public string PlaceString { get; set; }
    public string DateString { get; set; }
    public string ZFriendsString { get; set; }
    public string ZEnemysString { get; set; }
    public string FFriendsString { get; set; }
    public string FEnemysString { get; set; }
}

class ManualJson
{
    public int ItemId { get; set; }
}