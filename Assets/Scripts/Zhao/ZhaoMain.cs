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
        while(i!=0)
        {
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
