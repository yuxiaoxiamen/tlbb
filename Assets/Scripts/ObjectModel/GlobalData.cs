using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData
{
    public static List<Person> Persons { get; set; } //所有人物实例
    private static List<AttackStyleFixData> StyleFixDatas { get; set; } //招式的固定数据
    private static List<AttackStyleEffect> StyleEffects { get; set; } //所有招式的效果数据
    public static List<InnerGong> InnerGongs { get; set; } //所有内功数据
    public static List<Good> Items { get; set; } //所有物品数据
    public static List<FirstPlace> FirstPlaces { get; set; } //所有一级地图中地点数据
    public static List<SecondPlace> SecondPlaces { get; set; } //所有场所的数据
    public static Dictionary<string, List<Conversation>> MainConversations { get; set; } //主线剧情对话

    static GlobalData()
    {
        Persons = new List<Person>();
        ReadItemData();
        ReadSecondPlaceData();
        ReadFirstPlaceData();
        ReadInnerGongData();
        ReadStyleEffectData();
        ReadStyleData();
        ReadPersonData();
        ReadMainDialogueData();
    }

    public static void Init()
    {

    }

    static void ReadMainDialogueData()
    {
        var jsonData = Resources.Load<TextAsset>("jsonData/主线对话");
        var conversations = JsonConvert.DeserializeObject<List<Conversation>>(jsonData.text);
        List<ConversationJson> conversationJsons = JsonConvert.DeserializeObject<List<ConversationJson>>(jsonData.text);
        for (int i = 0; i < conversationJsons.Count; ++i)
        {
            var conversationJson = conversationJsons[i];
            Conversation conversation = conversations[i];
            conversation.People = Persons[conversationJson.PeopleId];
            //string[] dateDatas = conversationJson.DateData.Split('-');
            //conversation.Date = new GameDate(int.Parse(dateDatas[0]), int.Parse(dateDatas[1]),
            //    int.Parse(dateDatas[2]), int.Parse(dateDatas[3]));

            //string[] placedatas = conversationJson.PlaceData.Split('-');
            //if(placedatas.Length == 1)
            //{
            //    conversation.PlaceString = FirstPlaces[int.Parse(placedatas[0])];
            //}
            //else if(placedatas.Length == 2)
            //{
            //    SecondPlace second = SecondPlaces[int.Parse(placedatas[1])];
            //    second.PrePlace = FirstPlaces[int.Parse(placedatas[0])];
            //    conversation.PlaceString = second;
            //}
        }
        MainConversations = new Dictionary<string, List<Conversation>>();
        foreach(var conversation in conversations)
        {
            var key = conversation.PlaceString + "/" + conversation.DateString;
            if (!MainConversations.ContainsKey(key))
            {
                MainConversations.Add(key, new List<Conversation>());
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

    static void ReadInnerGongData()
    {
        var jsonData = Resources.Load<TextAsset>("jsonData/内功");
        InnerGongs = JsonConvert.DeserializeObject<List<InnerGong>>(jsonData.text);
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
                    perTalents.Add((Talent)Enum.Parse(typeof(Talent), p, true));
                }
                InnerGongs[i].PerTalentGain = perTalents;
            }
            else
            {
                InnerGongs[i].PerTalentGain = new List<Talent>();
            }

            string fts = gongJsons[i].FullTalent;
            if (!fts.Equals("无"))
            {
                string[] fs = fts.Split(',');
                List<Talent> fullTalents = new List<Talent>();
                foreach (var f in fs)
                {
                    fullTalents.Add((Talent)Enum.Parse(typeof(Talent), f, true));
                }
                InnerGongs[i].FullTalentGain = fullTalents;
            }
            else
            {
                InnerGongs[i].FullTalentGain = new List<Talent>();
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
            List<AttackStyle> attackStyleDatas = new List<AttackStyle>();
            List<InnerGong> innerGongs = new List<InnerGong>();
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
                innerGongs.Add(InnerGongs[int.Parse(gongs[j])]);
            }
            p.AttackStyles = attackStyleDatas;
            p.InnerGongs = innerGongs;
            Person person = new Person()
            {
                BaseData = p,
                SelectedAttackStyle = attackStyleDatas[0],
                CurrentHP = p.HP,
                CurrentMP = p.MP,
                CurrentEnergy = p.Energy,
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
}

class EffectsJson
{
    public string StyleEffects { get; set; }
}