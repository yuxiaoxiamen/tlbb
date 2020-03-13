using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZhaoMain : MonoBehaviour
{
    public static List<AttackStyleFixData> zhao = new List<AttackStyleFixData>();    //招式固定数据读取接口
    public static List<AttackStyleFixData> fist = new List<AttackStyleFixData>();
    public static List<AttackStyleFixData> palm = new List<AttackStyleFixData>();
    public static List<AttackStyleFixData> finger = new List<AttackStyleFixData>();
    public static List<AttackStyleFixData> knife = new List<AttackStyleFixData>();
    public static List<AttackStyleFixData> sword = new List<AttackStyleFixData>();
    public static List<AttackStyleFixData> rod = new List<AttackStyleFixData>();

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
        int i = 54;
        int n = 0;
        while (i!=0)
        {
            zhao.Add(GlobalData.StyleFixDatas[i]);
            string type = zhao[n].WeaponKind.ToString();
            switch (type)
            {
                case "Fist":
                    fist.Add(zhao[n]);
                    break;
                case "Palm":
                    palm.Add(zhao[n]);
                    break;
                case "Finger":
                    finger.Add(zhao[n]);
                    break;
                case "Knife":
                    knife.Add(zhao[n]);
                    break;
                case "Sword":
                    sword.Add(zhao[n]);
                    break;
                case "Rod":
                    rod.Add(zhao[n]);
                    break;
                default:
                    break;

            }
            i--;
            n++;

        }

        //默认显示所有拳法
        for (int m = 0; m <fist.Count; m++)
            GameObject.Find(m.ToString()).GetComponent<TextMesh>().text=Textchange(fist[m].Name);

        //清楚解释文字
        GameObject.Find("ZhaoName").GetComponent<TextMesh>().text = "";
        GameObject.Find("ZhaoRank").GetComponent<TextMesh>().text = "";
        GameObject.Find("ZhaoProficiency").GetComponent<TextMesh>().text = "";
        GameObject.Find("ZhaoIntroduction").GetComponent<TextMesh>().text = "";
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
