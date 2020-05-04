using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KongMain : MonoBehaviour
{
    //public static List<InnerGongFixData> gong = new List<InnerGongFixData>();     
    public static List<InnerGong> inners = new List<InnerGong>();      //内功读取接口
    public static List<string> order = new List<string>();      //用于存储功法顺序
    public static InnerGong inner = new InnerGong();
    public static int center = 0;
    int i = 0;

    List<GameObject> temp = new List<GameObject>();

    public string Textchange(string s)
    {
        string temp = null;
        for (int i = 0; i < s.Length; i++)
            temp = temp + s[i].ToString() + '\n';
        return temp;
    }

    public string Textnumchange(string s,int num)
    {
        string temp = null;
        if (s.Length < num)
            temp = s;
        else
        {
            int i = 0;
            for (int k = 0; k < s.Length / num+1; k++)
            {
                for (; i < s.Length && i<num*(k+1); i++)
                    temp = temp + s[i].ToString();
                if(i<s.Length)
                    temp = temp + "\r\n";
            }
        }
        temp = temp + "\n";

        return temp;
    }

    static KongMain()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        Person player = GameRunningData.GetRunningData().player;  //获取人物实例化对象

        foreach (InnerGong x in player.BaseData.InnerGongs)
            inners.Add(x);


        order.Add("待\n学\n习");
        order.Add("待\n学\n习");
        order.Add("待\n学\n习");
        order.Add("待\n学\n习");
        order.Add("待\n学\n习");

        //int gongnum = gong.Count;
        int innernum = inners.Count;

        switch (innernum)
        {
            case 0:
                GameObject.Find("top").GetComponent<TextMesh>().text = "0/0";
                break;
            case 1:
                order[0] = Textchange(inners[0].FixData.Name);
                GameObject.Find("top").GetComponent<TextMesh>().text = "1/1";
                break;
            case 2:
                order[0] = Textchange(inners[0].FixData.Name);
                order[1] = Textchange(inners[1].FixData.Name);
                GameObject.Find("top").GetComponent<TextMesh>().text = "1/2";
                break;
            case 3:
                order[0] = Textchange(inners[0].FixData.Name);
                order[1] = Textchange(inners[1].FixData.Name);
                order[2] = Textchange(inners[2].FixData.Name);
                GameObject.Find("top").GetComponent<TextMesh>().text = "1/3";
                break;
            case 4:
                order[0] = Textchange(inners[0].FixData.Name);
                order[1] = Textchange(inners[1].FixData.Name);
                order[2] = Textchange(inners[2].FixData.Name);
                order[3] = Textchange(inners[3].FixData.Name);
                GameObject.Find("top").GetComponent<TextMesh>().text = "1/4";
                break;
            default:
                order[0] = Textchange(inners[0].FixData.Name);
                order[1] = Textchange(inners[1].FixData.Name);
                order[2] = Textchange(inners[2].FixData.Name);
                order[3] = Textchange(inners[inners.Count - 2].FixData.Name);
                order[4] = Textchange(inners[inners.Count - 1].FixData.Name);
                GameObject.Find("top").GetComponent<TextMesh>().text = "1/5";
                break;
        }

        //显示功法
        foreach (string s in order)
        {
            GameObject.Find(i.ToString()).GetComponent<TextMesh>().text = s;
            i++;
        }

        //显示top数字
        if (inners.Count==0)
            GameObject.Find("top").GetComponent<TextMesh>().text = "0";
        else
            GameObject.Find("top").GetComponent<TextMesh>().text = (center% inners.Count+1).ToString()+"/"+ inners.Count.ToString();

        //标注当前功法
        if(string.Equals(inners[center].FixData.Name, player.SelectedInnerGong.FixData.Name))
              { 
            GameObject.Find("KongChange").SetActive(false);
            GameObject root = GameObject.Find("introduction");
            GameObject innerstate = root.transform.Find("KongState").gameObject;
            innerstate.SetActive(true);
            //显示正在修炼            
        }
          else
        {
            GameObject.Find("KongState").SetActive(false);
            GameObject root = GameObject.Find("Canvas");
            GameObject innerstate = root.transform.Find("KongChange").gameObject;
            innerstate.SetActive(true);
        }




        //显示功法信息
        inner = inners[center];
        GameObject.Find("KongName").GetComponent<TextMesh>().text = Textchange(inner.FixData.Name);                                                    
        GameObject.Find("RankValue").GetComponent<TextMesh>().text = inner.Rank.ToString();
        GameObject.Find("ProficiencyValue").GetComponent<TextMesh>().text = inner.Proficiency.ToString()+"/"
            +inner.GetMaxProFiciency();


        //进度条
        var v = GameObject.Find("ProficiencyActual").transform;
        //真实数据
        float parcent = (float)inner.Proficiency/(float)inner.GetMaxProFiciency();
        float xlen = GameObject.Find("ProficiencyBackground").transform.localScale.x;
        float prex = v.localScale.x;
        float actualx = xlen * parcent;
        GameObject.Find("ProficiencyActual").transform.localScale = new Vector3(xlen * parcent, v.localScale.y, v.localScale.z);
        GameObject.Find("ProficiencyActual").transform.localPosition = new Vector3(v.localPosition.x - (prex - actualx) / 2, v.localPosition.y, v.localPosition.z);

        //显示属性增益
        string pertalentgain="";
        string fulltalentgain="";
        //用来加顿号
        int x1 = 0;
        int x2 = 0;
        if (inner.FixData.PerTalentGain.Count == 0) { pertalentgain = pertalentgain + "无"; }
        else
        {
            foreach (var i in inner.FixData.PerTalentGain)
            {
                if (x1 > 0)
                    pertalentgain = pertalentgain + ",";
                string talentname = i.Name.ToString();
                switch (talentname)
                {
                    case "Bi":
                        pertalentgain = pertalentgain + "臂力*" + i.Number.ToString();
                        break;
                    case "Gen":
                        pertalentgain = pertalentgain + "根骨*" + i.Number.ToString();
                        break;
                    case "Jing":
                        pertalentgain = pertalentgain + "筋骨*" + i.Number.ToString();
                        break;
                    case "Wu":
                        pertalentgain = pertalentgain + "悟性*" + i.Number.ToString();
                        break;
                    case "Shen":
                        pertalentgain = pertalentgain + "身法*" + i.Number.ToString();
                        break;
                    default: break;
                }
                x1++;

            }
        }
        if (inner.FixData.FullTalentGain.Count == 0) { fulltalentgain = fulltalentgain + "无"; }
        else
        {
            foreach (var i in inner.FixData.FullTalentGain)
            {
                if (x2 > 0)
                    fulltalentgain = fulltalentgain + ",";
                string talentname = i.Name.ToString();
                switch (talentname)
                {
                    case "Bi":
                        fulltalentgain = fulltalentgain + "臂力*" + i.Number.ToString();
                        break;
                    case "Gen":
                        fulltalentgain = fulltalentgain + "根骨*" + i.Number.ToString();
                        break;
                    case "Jing":
                        fulltalentgain = fulltalentgain + "筋骨*" + i.Number.ToString();
                        break;
                    case "Wu":
                        fulltalentgain = fulltalentgain + "悟性*" + i.Number.ToString();
                        break;
                    case "Shen":
                        fulltalentgain = fulltalentgain + "身法*" + i.Number.ToString();
                        break;
                    default: break;
                }
                x2++;

            }
        }

        GameObject.Find("Gain").GetComponent<TextMesh>().text 
            = "气血增益(每重/满重): " + inner.FixData.PerHPGain.ToString() + " / " + inner.FixData.FullHPGain.ToString()
            + "\n内力增益(每重/满重): " + inner.FixData.PerMPGain.ToString() + " / " + inner.FixData.FullMPGain.ToString()
            + "\n属性增益: \n每重: "+pertalentgain+"\n满重: "+fulltalentgain;


        GameObject.Find("KongDetail").GetComponent<TextMesh>().text 
            = "默认效果：\n"+ Textnumchange(inner.FixData.DefaultEffect,25)+"\n"
            + "第一重效果：\n"+ Textnumchange(inner.FixData.FirstEffect,25) + "\n"
            + "第六重效果：\n"+ Textnumchange(inner.FixData.SixthEffect,25) + "\n"
            + "第十重效果：\n"+ Textnumchange(inner.FixData.TenthEffect,25) ;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
