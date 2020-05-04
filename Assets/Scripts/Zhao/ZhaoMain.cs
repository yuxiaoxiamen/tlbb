using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZhaoMain : MonoBehaviour
{
    //public static List<AttackStyleFixData> zhao = new List<AttackStyleFixData>();    //招式固定数据读取接口

    public static List<AttackStyle> attackStyles = new List<AttackStyle>();
    public static List<AttackStyle> fist = new List<AttackStyle>();
    public static List<AttackStyle> palm = new List<AttackStyle>();
    public static List<AttackStyle> finger = new List<AttackStyle>();
    public static List<AttackStyle> knife = new List<AttackStyle>();
    public static List<AttackStyle> sword = new List<AttackStyle>();
    public static List<AttackStyle> rod = new List<AttackStyle>();

    public static string zhaotype;

    public string Textchange(string s)
    {
        string temp = null;
        for (int i = 0; i < s.Length; i++)
            temp = temp + s[i].ToString() + '\n';
        return temp;
    }

    public string Textnumchange(string s, int num)
    {
        string temp = null;
        if (s.Length < num)
            temp = s;
        else
        {
            int i = 0;
            for (int k = 0; k < s.Length / num + 1; k++)
            {
                for (; i < s.Length && i < num * (k + 1); i++)
                    temp = temp + s[i].ToString();
                if (i < s.Length)
                    temp = temp + "\r\n";
            }
        }
        temp = temp + "\n";

        return temp;
    }

    static ZhaoMain()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        Person player = GameRunningData.GetRunningData().player;  //获取人物实例化对象

        foreach (AttackStyle x in player.BaseData.AttackStyles)
            attackStyles.Add(x);

        for (int i = 0; i < attackStyles.Count; i++)
        {
            string kind = attackStyles[i].FixData.WeaponKind.ToString();
            switch (kind)
            {
                case "Fist":
                    fist.Add(attackStyles[i]);
                    break;
                case "Palm":
                    palm.Add(attackStyles[i]);
                    break;
                case "Finger":
                    finger.Add(attackStyles[i]);
                    break;
                case "Knife":
                    knife.Add(attackStyles[i]);
                    break;
                case "Sword":
                    sword.Add(attackStyles[i]);
                    break;
                case "Rod":
                    rod.Add(attackStyles[i]);
                    break;
                default:
                    break;
            }

        }

        //默认显示所有拳法
        for (int m = 0; m <fist.Count; m++)
            GameObject.Find("book"+m.ToString()).GetComponent<TextMesh>().text=Textchange(fist[m].FixData.Name);

        //清楚解释文字
        GameObject.Find("ZhaoName").GetComponent<TextMesh>().text = "";
        GameObject.Find("ZhaoRank").GetComponent<TextMesh>().text = "";
        GameObject.Find("ZhaoProficiency").GetComponent<TextMesh>().text = "";
        GameObject.Find("RankValue").GetComponent<TextMesh>().text = "";
        GameObject.Find("ProficiencyValue").GetComponent<TextMesh>().text = "";
        GameObject.Find("ZhaoDetail").GetComponent<TextMesh>().text = "";

        //单独处理进度条
        GameObject.Find("ProficiencyBackground").GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
        GameObject.Find("ProficiencyActual").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);

        zhaotype = "Fist";

    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
