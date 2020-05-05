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
                    if (KongMain.inners.Count > 4)
                    {
                        KongMain.center = (KongMain.center + KongMain.inners.Count - 1) % KongMain.inners.Count;
                        temp = KongMain.center;
                        KongMain.order[0] = Textchange(KongMain.inners[temp % KongMain.inners.Count].FixData.Name);
                        KongMain.order[1] = Textchange(KongMain.inners[(temp + 1) % KongMain.inners.Count].FixData.Name);
                        KongMain.order[2] = Textchange(KongMain.inners[(temp + 2) % KongMain.inners.Count].FixData.Name);
                        KongMain.order[3] = Textchange(KongMain.inners[(temp + KongMain.inners.Count - 2) % KongMain.inners.Count].FixData.Name);
                        KongMain.order[4] = Textchange(KongMain.inners[(temp + KongMain.inners.Count - 1) % KongMain.inners.Count].FixData.Name);
                        //标注当前功法
            if (string.Equals(KongMain.inners[KongMain.center].FixData.Name, KongMain.person.SelectedInnerGong.FixData.Name))
            {
                GameObject.Find("KongChange").SetActive(false);
                GameObject root = GameObject.Find("introduction");
                GameObject innerstate = root.transform.Find("KongState").gameObject;
                innerstate.SetActive(true);         
            }
            else
            {
                if (GameObject.Find("KongState"))
                {
                    GameObject.Find("KongState").SetActive(false);
                    GameObject root = GameObject.Find("Canvas");
                    GameObject innerstate = root.transform.Find("KongChange").gameObject;
                    innerstate.SetActive(true);
                }
            }
                    }
                    else
                    {
                        if (KongMain.center >0)
                        {
                            KongMain.center = (KongMain.center + 5-1) % 5;
                            temp = KongMain.center;
                            for (int m = 0; m < 5; m++)
                            {
                                if (temp < KongMain.inners.Count)
                                {
                                    KongMain.order[m] = Textchange(KongMain.inners[temp].FixData.Name);
                                }
                                else
                                    KongMain.order[m] = "待\n学\n习";
                                temp = (temp +1) % 5;
                            }
                            if (string.Equals(KongMain.inners[KongMain.center].FixData.Name, KongMain.person.SelectedInnerGong.FixData.Name))
                            {
                                GameObject.Find("KongChange").SetActive(false);
                                GameObject root = GameObject.Find("introduction");
                                GameObject innerstate = root.transform.Find("KongState").gameObject;
                                innerstate.SetActive(true);
                            }
                            else
                            {
                                if (GameObject.Find("KongState"))
                                {
                                    GameObject.Find("KongState").SetActive(false);
                                    GameObject root = GameObject.Find("Canvas");
                                    GameObject innerstate = root.transform.Find("KongChange").gameObject;
                                    innerstate.SetActive(true);
                                }
                            }
                        }
                    }
                    break;
                case "Right":
                    if (KongMain.inners.Count>4)
                    {
                        KongMain.center = (KongMain.center + 1) % KongMain.inners.Count;
                        temp = KongMain.center;
                        KongMain.order[0] =Textchange(KongMain.inners[temp% KongMain.inners.Count].FixData.Name);
                        KongMain.order[1] = Textchange(KongMain.inners[(temp+1) % KongMain.inners.Count].FixData.Name);
                        KongMain.order[2] = Textchange(KongMain.inners[(temp+2) % KongMain.inners.Count].FixData.Name);
                        KongMain.order[3] = Textchange(KongMain.inners[(temp+ KongMain.inners.Count-2) % KongMain.inners.Count].FixData.Name);
                        KongMain.order[4] = Textchange(KongMain.inners[(temp + KongMain.inners.Count-1) % KongMain.inners.Count].FixData.Name);
                        if (string.Equals(KongMain.inners[KongMain.center].FixData.Name, KongMain.person.SelectedInnerGong.FixData.Name))
                        {
                            GameObject.Find("KongChange").SetActive(false);
                            GameObject root = GameObject.Find("introduction");
                            GameObject innerstate = root.transform.Find("KongState").gameObject;
                            innerstate.SetActive(true);
                        }
                        else
                        {
                            if (GameObject.Find("KongState"))
                            {
                                GameObject.Find("KongState").SetActive(false);
                                GameObject root = GameObject.Find("Canvas");
                                GameObject innerstate = root.transform.Find("KongChange").gameObject;
                                innerstate.SetActive(true);
                            }
                        }
                    }
                    else
                    {
                        if (KongMain.center < KongMain.inners.Count - 1)
                        {
                            KongMain.center = (KongMain.center + 1) % 5;
                            temp = KongMain.center;
                            for (int m = 0; m < 5; m++)
                            {
                                if (temp < KongMain.inners.Count)
                                {
                                    KongMain.order[m] = Textchange(KongMain.inners[temp].FixData.Name);
                                }
                                else
                                    KongMain.order[m] = "待\n学\n习";
                                temp = (temp + 1) % 5;
                            }
                            if (string.Equals(KongMain.inners[KongMain.center].FixData.Name, KongMain.person.SelectedInnerGong.FixData.Name))
                            {
                                GameObject.Find("KongChange").SetActive(false);
                                GameObject root = GameObject.Find("introduction");
                                GameObject innerstate = root.transform.Find("KongState").gameObject;
                                innerstate.SetActive(true);
                            }
                            else
                            {
                                if (GameObject.Find("KongState"))
                                {
                                    GameObject.Find("KongState").SetActive(false);
                                    GameObject root = GameObject.Find("Canvas");
                                    GameObject innerstate = root.transform.Find("KongChange").gameObject;
                                    innerstate.SetActive(true);
                                }
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
            GameObject.Find("top").GetComponent<TextMesh>().text = (KongMain.center % KongMain.inners.Count+1).ToString() + "/" + KongMain.inners.Count.ToString();


            //显示内功信息
            InnerGong inner = KongMain.inners[KongMain.center];
            KongMain.inner = inner;
            GameObject.Find("KongName").GetComponent<TextMesh>().text = Textchange(inner.FixData.Name);
            GameObject.Find("RankValue").GetComponent<TextMesh>().text = inner.Rank.ToString();
            GameObject.Find("ProficiencyValue").GetComponent<TextMesh>().text = inner.Proficiency.ToString() + "/"
            +inner.GetMaxProFiciency();

            Person player = GameRunningData.GetRunningData().player;  //获取人物实例化对象

            //显示属性增益
            string pertalentgain = "";
            string fulltalentgain = "";
            //用来加顿号
            int x1 = 0;
            int x2 = 0;
            if (inner.FixData.PerTalentGain.Count == 0) { pertalentgain = pertalentgain + "无"; }
            else
            {
                foreach (var talent in inner.FixData.PerTalentGain)
                {
                    if (x1 > 0)
                        pertalentgain = pertalentgain + ",";
                    string talentname = talent.Name.ToString();
                    switch (talentname)
                    {
                        case "Bi":
                            pertalentgain = pertalentgain + "臂力*" + talent.Number.ToString();
                            break;
                        case "Gen":
                            pertalentgain = pertalentgain + "根骨*" + talent.Number.ToString();
                            break;
                        case "Jing":
                            pertalentgain = pertalentgain + "筋骨*" + talent.Number.ToString();
                            break;
                        case "Wu":
                            pertalentgain = pertalentgain + "悟性*" + talent.Number.ToString();
                            break;
                        case "Shen":
                            pertalentgain = pertalentgain + "身法*" + talent.Number.ToString();
                            break;
                        default: break;
                    }
                    x1++;

                }
            }
            if (inner.FixData.FullTalentGain.Count == 0) { fulltalentgain = fulltalentgain + "无"; }
            else
            {
                foreach (var talent in inner.FixData.FullTalentGain)
                {
                    if (x2 > 0)
                        fulltalentgain = fulltalentgain + ",";
                    string talentname = talent.Name.ToString();
                    switch (talentname)
                    {
                        case "Bi":
                            fulltalentgain = fulltalentgain + "臂力*" + talent.Number.ToString();
                            break;
                        case "Gen":
                            fulltalentgain = fulltalentgain + "根骨*" + talent.Number.ToString();
                            break;
                        case "Jing":
                            fulltalentgain = fulltalentgain + "筋骨*" + talent.Number.ToString();
                            break;
                        case "Wu":
                            fulltalentgain = fulltalentgain + "悟性*" + talent.Number.ToString();
                            break;
                        case "Shen":
                            fulltalentgain = fulltalentgain + "身法*" + talent.Number.ToString();
                            break;
                        default: break;
                    }
                    x2++;

                }
            }

            GameObject.Find("Gain").GetComponent<TextMesh>().text
                = "气血增益(每重/满重): " + inner.FixData.PerHPGain.ToString() + " / " + inner.FixData.FullHPGain.ToString()
                + "\n内力增益(每重/满重): " + inner.FixData.PerMPGain.ToString() + " / " + inner.FixData.FullMPGain.ToString()
                + "\n属性增益: \n每重: " + pertalentgain + "\n满重: " + fulltalentgain;

           


            //进度条
            var v = GameObject.Find("ProficiencyActual").transform;
            //真实数据
            float parcent = (float)inner.Proficiency/(float)inner.GetMaxProFiciency();
            float xlen = GameObject.Find("ProficiencyBackground").transform.localScale.x;
            float prex = v.localScale.x;
            float actualx = xlen * parcent;
            GameObject.Find("ProficiencyActual").transform.localScale = new Vector3(xlen * parcent, v.localScale.y, v.localScale.z);
            GameObject.Find("ProficiencyActual").transform.localPosition = new Vector3(v.localPosition.x - (prex - actualx) / 2, v.localPosition.y, v.localPosition.z);


            GameObject.Find("KongDetail").GetComponent<TextMesh>().text = "默认效果：\n" + Textnumchange(inner.FixData.DefaultEffect, 25) + "\n" + "第一重效果：\n" + Textnumchange(inner.FixData.FirstEffect, 25) + "\n" + "第六重效果：\n" + Textnumchange(inner.FixData.SixthEffect, 25) + "\n" + "第十重效果：\n" + Textnumchange(inner.FixData.TenthEffect, 25);

        });
    }

}
