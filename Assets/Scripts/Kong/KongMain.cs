using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KongMain : MonoBehaviour
{
    public static List<InnerGong> gong = new List<InnerGong>();     //内功读取接口
    public static List<string> order = new List<string>();      //用于存储功法顺序
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
        gong.Add(GlobalData.InnerGongs[0]);
        gong.Add(GlobalData.InnerGongs[1]);
        gong.Add(GlobalData.InnerGongs[2]);
        gong.Add(GlobalData.InnerGongs[3]);
        gong.Add(GlobalData.InnerGongs[4]);
        gong.Add(GlobalData.InnerGongs[5]);

        order.Add("待\n学\n习");
        order.Add("待\n学\n习");
        order.Add("待\n学\n习");
        order.Add("待\n学\n习");
        order.Add("待\n学\n习");

        int gongnum = gong.Count;

        switch (gongnum)
        {
            case 0:
                GameObject.Find("top").GetComponent<TextMesh>().text = "0/0";
                break;
            case 1:
                order[0] = Textchange(gong[0].Name);
                GameObject.Find("top").GetComponent<TextMesh>().text = "1/1";
                break;
            case 2:
                order[0] = Textchange(gong[0].Name);
                order[1] = Textchange(gong[1].Name);
                GameObject.Find("top").GetComponent<TextMesh>().text = "1/2";
                break;
            case 3:
                order[0] = Textchange(gong[0].Name);
                order[1] = Textchange(gong[1].Name);
                order[2] = Textchange(gong[2].Name);
                GameObject.Find("top").GetComponent<TextMesh>().text = "1/3";
                break;
            case 4:
                order[0] = Textchange(gong[0].Name);
                order[1] = Textchange(gong[1].Name);
                order[2] = Textchange(gong[2].Name);
                order[3] = Textchange(gong[3].Name);
                GameObject.Find("top").GetComponent<TextMesh>().text = "1/4";
                break;
            default:
                order[0] = Textchange(gong[0].Name);
                order[1] = Textchange(gong[1].Name);
                order[2] = Textchange(gong[2].Name);
                order[3] = Textchange(gong[gong.Count - 2].Name);
                order[4] = Textchange(gong[gong.Count - 1].Name);
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
        if (gong.Count==0)
            GameObject.Find("top").GetComponent<TextMesh>().text = "0";
        else
            GameObject.Find("top").GetComponent<TextMesh>().text = (center% gong.Count+1).ToString()+"/"+ gong.Count.ToString();

        //标注当前功法
        /*if(string.Equals(gong[center].Name, GlobalData.Persons[0].SelectedInnerGong.Name))
            GameObject.Find("KongChange").GetComponent<TextMesh>().text = "居然成功了";
        else
        GameObject.Find("KongChange").transform.GetComponent<Text>().text="居然成功了";*/

        //显示功法信息
        GameObject.Find("KongName").GetComponent<TextMesh>().text = gong[center].Name;
        GameObject.Find("RankValue").GetComponent<TextMesh>().text = "没找到这个数据";
        GameObject.Find("ProficiencyValue").GetComponent<TextMesh>().text = "n/100";
        GameObject.Find("KongDetail").GetComponent<TextMesh>().text = "默认效果：\n"+ Textnumchange(gong[center].DefaultEffect,25)+"\n"+ "第一重效果：\n"+ Textnumchange(gong[center].FirstEffect,25) + "\n"+ "第六重效果：\n"+ Textnumchange(gong[center].SixthEffect,25) + "\n"+ "第十重效果：\n"+ Textnumchange(gong[center].TenthEffect,25) ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
