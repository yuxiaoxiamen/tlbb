using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LikabilityTool
{
    public static void PromoteInnManager(int value)
    {
        for(int i = 65; i <= 68; ++i)
        {
            GlobalData.Persons[i].Likability += value;
        }
    }

    public static void PromoteBoiteManager(int value)
    {
        for (int i = 69; i <= 72; ++i)
        {
            GlobalData.Persons[i].Likability += value;
        }
    }

    public static void PromoteDoctor(int value)
    {
        for (int i = 73; i <= 76; ++i)
        {
            GlobalData.Persons[i].Likability += value;
        }
    }

    public static void PromoteBlacksmith(int value)
    {
        for (int i = 77; i <= 80; ++i)
        {
            GlobalData.Persons[i].Likability += value;
        }
    }

    public static void PromoteWaiter(int value)
    {
        for (int i = 81; i <= 84; ++i)
        {
            GlobalData.Persons[i].Likability += value;
        }
    }

    public static void PromoteBartender(int value)
    {
        for (int i = 85; i <= 88; ++i)
        {
            GlobalData.Persons[i].Likability += value;
        }
    }

    public static int GetInnManager()
    {
        return GlobalData.Persons[65].Likability;
    }

    public static int GetBoiteManager()
    {
        return GlobalData.Persons[69].Likability;
    }

    public static int GetDoctor()
    {
        return GlobalData.Persons[73].Likability;
    }

    public static int GetBlacksmith()
    {
        return GlobalData.Persons[77].Likability;
    }

    public static int GetWaiter()
    {
        return GlobalData.Persons[81].Likability;
    }

    public static int GetBartender()
    {
        return GlobalData.Persons[85].Likability;
    }

    public static void SetStoryLike(Good item)
    {
        if(item.Type == ItemKind.Alcohol)
        {
            if (item.Id <= 4)
            {
                PromoteBartender(1);
            }
            else if (item.Id <= 7)
            {
                PromoteBartender(2);
            }
            else
            {
                PromoteBartender(3);
            }
        }
        else if(item.Type == ItemKind.Food)
        {
            if (item.Id <= 14)
            {
                PromoteWaiter(1);
            }
            else if (item.Id <= 17)
            {
                PromoteWaiter(2);
            }
            else
            {
                PromoteWaiter(3);
            }
        }
        else if(item.Type == ItemKind.Sword)
        {
            if (item.Id <= 30)
            {
                PromoteBlacksmith(1);
            }
            else if (item.Id <= 34)
            {
                PromoteBlacksmith(2);
            }
            else
            {
                PromoteBlacksmith(3);
            }
        }
        else if (item.Type == ItemKind.Knife)
        {
            if (item.Id <= 46)
            {
                PromoteBlacksmith(1);
            }
            else if (item.Id <= 50)
            {
                PromoteBlacksmith(2);
            }
            else
            {
                PromoteBlacksmith(3);
            }
        }
        else
        {
            if (item.Id <= 58)
            {
                PromoteBlacksmith(1);
            }
            else if (item.Id <= 61)
            {
                PromoteBlacksmith(2);
            }
            else
            {
                PromoteBlacksmith(3);
            }
        }
    }

    public static List<Good> GetGoods(string storeType)
    {
        List<Good> result = new List<Good>();
        List<Good> items = new List<Good>();
        int final = 0;
        if (storeType == "Alcohol" || storeType == "Food")
        {
            foreach(var item in GlobalData.Items)
            {
                var kind = storeType == "Alcohol" ? ItemKind.Alcohol : ItemKind.Food;
                if(item.Type == kind)
                {
                    items.Add(item);
                }
            }
            final = items.Count / 2 - 1;
            int like = storeType == "Alcohol" ? GetBartender() : GetWaiter();
            if (like >= 40)
            {
                final = (int)(items.Count * 0.7);
            }
            if (like >= 100)
            {
                final = items.Count - 1;
            }
            for(int i = 0; i <= final; ++i)
            {
                result.Add(items[i]);
            }
        }
        if (storeType == "Blacksmith")
        {
            List<Good> swords = new List<Good>();
            List<Good> knifes = new List<Good>();
            List<Good> rods = new List<Good>();
            foreach (var item in GlobalData.Items)
            {
                if (item.Type == ItemKind.Sword)
                {
                    swords.Add(item);
                }
                if (item.Type == ItemKind.Knife)
                {
                    knifes.Add(item);
                }
                if (item.Type == ItemKind.Rod)
                {
                    rods.Add(item);
                }
            }
            if (GetBlacksmith() < 40)
            {
                for(int i = 0; i <= swords.Count / 2; ++i)
                {
                    result.Add(swords[i]);
                }
                for (int i = 0; i <= knifes.Count / 2; ++i)
                {
                    result.Add(knifes[i]);
                }
                for (int i = 0; i <= rods.Count / 2; ++i)
                {
                    result.Add(rods[i]);
                }
            }
            if(GetBlacksmith() >= 40 && GetBlacksmith() < 100)
            {
                for (int i = 0; i <= (int)(swords.Count * 0.8); ++i)
                {
                    result.Add(swords[i]);
                }
                for (int i = 0; i <= (int)(knifes.Count * 0.8); ++i)
                {
                    result.Add(knifes[i]);
                }
                for (int i = 0; i <= (int)(rods.Count * 0.8); ++i)
                {
                    result.Add(rods[i]);
                }
            }
            if (GetBlacksmith() >= 100)
            {
                for (int i = 0; i <= swords.Count; ++i)
                {
                    result.Add(swords[i]);
                }
                for (int i = 0; i <= knifes.Count; ++i)
                {
                    result.Add(knifes[i]);
                }
                for (int i = 0; i <= rods.Count; ++i)
                {
                    result.Add(rods[i]);
                }
            }
        }
        return result;
    }
}
