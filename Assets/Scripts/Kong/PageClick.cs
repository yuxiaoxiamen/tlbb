using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageClick : MonoBehaviour
{
    private Button button;
    int temp = 0;
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

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            string ButtonName = button.name;

            switch (ButtonName)
            {
                case "Left":
                    if (KongMain.gong.Count > 4)
                    {
                        KongMain.center = (KongMain.center + KongMain.gong.Count - 1) % KongMain.gong.Count;
                        temp = KongMain.center;
                        KongMain.order[0] = Textchange(KongMain.gong[temp % KongMain.gong.Count].Name);
                        KongMain.order[1] = Textchange(KongMain.gong[(temp + 1) % KongMain.gong.Count].Name);
                        KongMain.order[2] = Textchange(KongMain.gong[(temp + 2) % KongMain.gong.Count].Name);
                        KongMain.order[3] = Textchange(KongMain.gong[(temp + KongMain.gong.Count - 2) % KongMain.gong.Count].Name);
                        KongMain.order[4] = Textchange(KongMain.gong[(temp + KongMain.gong.Count - 1) % KongMain.gong.Count].Name);
                    }
                    else
                    {
                        if (KongMain.center >0)
                        {
                            KongMain.center = (KongMain.center + 5-1) % 5;
                            temp = KongMain.center;
                            for (int m = 0; m < 5; m++)
                            {
                                if (temp < KongMain.gong.Count)
                                {
                                    KongMain.order[m] = Textchange(KongMain.gong[temp].Name);
                                }
                                else
                                    KongMain.order[m] = "待\n学\n习";
                                temp = (temp +1) % 5;
                            }
                        }
                    }
                    break;
                case "Right":
                    if (KongMain.gong.Count>4)
                    {
                        KongMain.center = (KongMain.center + 1) % KongMain.gong.Count;
                        temp = KongMain.center;
                        KongMain.order[0] =Textchange(KongMain.gong[temp% KongMain.gong.Count].Name);
                        KongMain.order[1] = Textchange(KongMain.gong[(temp+1) % KongMain.gong.Count].Name);
                        KongMain.order[2] = Textchange(KongMain.gong[(temp+2) % KongMain.gong.Count].Name);
                        KongMain.order[3] = Textchange(KongMain.gong[(temp+ KongMain.gong.Count-2) % KongMain.gong.Count].Name);
                        KongMain.order[4] = Textchange(KongMain.gong[(temp + KongMain.gong.Count-1) % KongMain.gong.Count].Name);

                    }
                    else
                    {
                        if (KongMain.center < KongMain.gong.Count - 1)
                        {
                            KongMain.center = (KongMain.center + 1) % 5;
                            temp = KongMain.center;
                            for (int m = 0; m < 5; m++)
                            {
                                if (temp < KongMain.gong.Count)
                                {
                                    KongMain.order[m] = Textchange(KongMain.gong[temp].Name);
                                }
                                else
                                    KongMain.order[m] = "待\n学\n习";
                                temp = (temp + 1) % 5;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

            //显示切换
            int i = 0;
            foreach (string s in KongMain.order)
            {
                GameObject.Find(i.ToString()).GetComponent<TextMesh>().text = s;
                i++;
            }

            //显示top数字
            GameObject.Find("top").GetComponent<TextMesh>().text = (KongMain.center % KongMain.gong.Count+1).ToString() + "/" + KongMain.gong.Count.ToString();

            //显示内功信息
            GameObject.Find("KongName").GetComponent<TextMesh>().text = KongMain.gong[KongMain.center].Name;
            GameObject.Find("RankValue").GetComponent<TextMesh>().text = "没找到这个数据";
            GameObject.Find("ProficiencyValue").GetComponent<TextMesh>().text = "n/100";
            GameObject.Find("KongDetail").GetComponent<TextMesh>().text = "默认效果：\n" + Textnumchange(KongMain.gong[KongMain.center].DefaultEffect, 25) + "\n" + "第一重效果：\n" + Textnumchange(KongMain.gong[KongMain.center].FirstEffect, 25) + "\n" + "第六重效果：\n" + Textnumchange(KongMain.gong[KongMain.center].SixthEffect, 25) + "\n" + "第十重效果：\n" + Textnumchange(KongMain.gong[KongMain.center].TenthEffect, 25);

        });
    }

}
