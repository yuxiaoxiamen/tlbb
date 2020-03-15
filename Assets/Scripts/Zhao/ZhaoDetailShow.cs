using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZhaoDetailShow : MonoBehaviour
{

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
        
    }

    private void OnMouseDown()
    {
        float num = float.Parse(gameObject.name);           //获取招式序号
        int num1 = (int)num;
        string type = ZhaoMain.zhaotype;                   //获取招式类型

        switch (type)
        {
            case "Fist":
                if (num1 < ZhaoMain.fist.Count)
                { 
                    GameObject.Find("ZhaoName").GetComponent<TextMesh>().text = ZhaoMain.fist[num1].Name;

                    GameObject.Find("ZhaoRank").GetComponent<TextMesh>().text = "修炼层数：";
                    GameObject.Find("ZhaoProficiency").GetComponent<TextMesh>().text = "熟练度：";
                    GameObject.Find("ZhaoIntroduction").GetComponent<TextMesh>().text = "招式介绍：";
                    
                    AttackStyle attack = new AttackStyle();
                    GameObject.Find("RankValue").GetComponent<TextMesh>().text = attack.Rank.ToString();
                    //GameObject.Find("RankValue").GetComponent<TextMesh>().text = "没找到这个数据";
                    GameObject.Find("ProficiencyValue").GetComponent<TextMesh>().text = attack.Proficiency.ToString() + "/100";

                    //进度条
                    GameObject.Find("ProficiencyBackground").GetComponent<SpriteRenderer>().color = new Color32(108, 153, 192, 255);
                    GameObject.Find("ProficiencyActual").GetComponent<SpriteRenderer>().color = new Color32(226, 115, 110, 255);

                    var v = GameObject.Find("ProficiencyActual").transform;
                    //真实数据
                    //float parcent = (float)attack.Proficiency/100;
                    //测试数据进度条70%
                    float parcent = 0.7F;
                    float xlen = GameObject.Find("ProficiencyBackground").transform.localScale.x;
                    float prex = v.localScale.x;
                    float actualx = xlen * parcent;
                    GameObject.Find("ProficiencyActual").transform.localScale = new Vector3(xlen * parcent, v.localScale.y, v.localScale.z);
                    GameObject.Find("ProficiencyActual").transform.localPosition = new Vector3(v.localPosition.x - (prex - actualx) / 2, v.localPosition.y, v.localPosition.z);

                    //招式效果读取
                    string effect = "";
                    for (int n = 0; n < ZhaoMain.fist[num1].Effects.Count; n++)
                    {
                        effect = effect+ZhaoMain.fist[num1].Effects[n].Name + ":\n" + ZhaoMain.fist[num1].Effects[n].Detail;
                        if (ZhaoMain.fist[num1].Effects[n].Value != 0)
                            effect = effect + "： " + ZhaoMain.fist[num1].Effects[n].Value;
                        effect = effect + "\n"+"持续时间： "+ ZhaoMain.fist[num1].Effects[n].TimeValue+"\n";
                    }

                    GameObject.Find("ZhaoDetail").GetComponent<TextMesh>().text = "真气消耗量： "+ZhaoMain.fist[num1].MPCost.ToString()+"\n"+"攻击范围： "+ZhaoMain.fist[num1].DetailInfo + "\n\n"+effect;

                }
                else
                {
                    GameObject.Find("ZhaoName").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ZhaoRank").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ZhaoProficiency").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ZhaoIntroduction").GetComponent<TextMesh>().text = "";
                    GameObject.Find("RankValue").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ProficiencyValue").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ZhaoDetail").GetComponent<TextMesh>().text = "";

                    //单独处理进度条
                    GameObject.Find("ProficiencyBackground").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                    GameObject.Find("ProficiencyActual").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                }
                break;
            case "Palm":
                if (num1 < ZhaoMain.palm.Count)
                {
                    GameObject.Find("ZhaoName").GetComponent<TextMesh>().text = ZhaoMain.palm[num1].Name;

                    GameObject.Find("ZhaoRank").GetComponent<TextMesh>().text = "修炼层数：";
                    GameObject.Find("ZhaoProficiency").GetComponent<TextMesh>().text = "熟练度：";
                    GameObject.Find("ZhaoIntroduction").GetComponent<TextMesh>().text = "招式介绍：";

                    AttackStyle attack = new AttackStyle();
                    GameObject.Find("RankValue").GetComponent<TextMesh>().text = attack.Rank.ToString();
                    //GameObject.Find("RankValue").GetComponent<TextMesh>().text = "没找到这个数据";
                    GameObject.Find("ProficiencyValue").GetComponent<TextMesh>().text = attack.Proficiency.ToString() + "/100";

                    //进度条
                    GameObject.Find("ProficiencyBackground").GetComponent<SpriteRenderer>().color = new Color32(108, 153, 192, 255);
                    GameObject.Find("ProficiencyActual").GetComponent<SpriteRenderer>().color = new Color32(226, 115, 110, 255);

                    var v = GameObject.Find("ProficiencyActual").transform;
                    //真实数据
                    //float parcent = (float)attack.Proficiency/100;
                    //测试数据进度条70%
                    float parcent = 0.7F;
                    float xlen = GameObject.Find("ProficiencyBackground").transform.localScale.x;
                    float prex = v.localScale.x;
                    float actualx = xlen * parcent;
                    GameObject.Find("ProficiencyActual").transform.localScale = new Vector3(xlen * parcent, v.localScale.y, v.localScale.z);
                    GameObject.Find("ProficiencyActual").transform.localPosition = new Vector3(v.localPosition.x - (prex - actualx) / 2, v.localPosition.y, v.localPosition.z);

                    //招式效果读取
                    string effect = "";
                    for (int n = 0; n < ZhaoMain.palm[num1].Effects.Count; n++)
                    {
                        effect = effect + ZhaoMain.palm[num1].Effects[n].Name + ":\n" + ZhaoMain.palm[num1].Effects[n].Detail;
                        if (ZhaoMain.palm[num1].Effects[n].Value != 0)
                            effect = effect + "： " + ZhaoMain.palm[num1].Effects[n].Value;
                        effect = effect + "\n" + "持续时间： " + ZhaoMain.palm[num1].Effects[n].TimeValue + "\n";
                    }

                    GameObject.Find("ZhaoDetail").GetComponent<TextMesh>().text = "真气消耗量： " + ZhaoMain.palm[num1].MPCost.ToString() + "\n" + "攻击范围： " + ZhaoMain.palm[num1].DetailInfo + "\n\n" + effect;

                }
                else
                {
                    GameObject.Find("ZhaoName").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ZhaoRank").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ZhaoProficiency").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ZhaoIntroduction").GetComponent<TextMesh>().text = "";
                    GameObject.Find("RankValue").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ProficiencyValue").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ZhaoDetail").GetComponent<TextMesh>().text = "";

                    //单独处理进度条
                    GameObject.Find("ProficiencyBackground").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                    GameObject.Find("ProficiencyActual").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                }
                break;
            case "Finger":
                if (num1 < ZhaoMain.finger.Count)
                {
                    GameObject.Find("ZhaoName").GetComponent<TextMesh>().text = ZhaoMain.finger[num1].Name;

                    GameObject.Find("ZhaoRank").GetComponent<TextMesh>().text = "修炼层数：";
                    GameObject.Find("ZhaoProficiency").GetComponent<TextMesh>().text = "熟练度：";
                    GameObject.Find("ZhaoIntroduction").GetComponent<TextMesh>().text = "招式介绍：";

                    AttackStyle attack = new AttackStyle();
                    GameObject.Find("RankValue").GetComponent<TextMesh>().text = attack.Rank.ToString();
                    //GameObject.Find("RankValue").GetComponent<TextMesh>().text = "没找到这个数据";
                    GameObject.Find("ProficiencyValue").GetComponent<TextMesh>().text = attack.Proficiency.ToString() + "/100";

                    //进度条
                    GameObject.Find("ProficiencyBackground").GetComponent<SpriteRenderer>().color = new Color32(108, 153, 192, 255);
                    GameObject.Find("ProficiencyActual").GetComponent<SpriteRenderer>().color = new Color32(226, 115, 110, 255);

                    var v = GameObject.Find("ProficiencyActual").transform;
                    //真实数据
                    //float parcent = (float)attack.Proficiency/100;
                    //测试数据进度条70%
                    float parcent = 0.7F;
                    float xlen = GameObject.Find("ProficiencyBackground").transform.localScale.x;
                    float prex = v.localScale.x;
                    float actualx = xlen * parcent;
                    GameObject.Find("ProficiencyActual").transform.localScale = new Vector3(xlen * parcent, v.localScale.y, v.localScale.z);
                    GameObject.Find("ProficiencyActual").transform.localPosition = new Vector3(v.localPosition.x - (prex - actualx) / 2, v.localPosition.y, v.localPosition.z);

                    //招式效果读取
                    string effect = "";
                    for (int n = 0; n < ZhaoMain.finger[num1].Effects.Count; n++)
                    {
                        effect = effect + ZhaoMain.finger[num1].Effects[n].Name + ":\n" + ZhaoMain.finger[num1].Effects[n].Detail;
                        if (ZhaoMain.finger[num1].Effects[n].Value != 0)
                            effect = effect + "： " + ZhaoMain.finger[num1].Effects[n].Value;
                        effect = effect + "\n" + "持续时间： " + ZhaoMain.finger[num1].Effects[n].TimeValue + "\n";
                    }

                    GameObject.Find("ZhaoDetail").GetComponent<TextMesh>().text = "真气消耗量： " + ZhaoMain.finger[num1].MPCost.ToString() + "\n" + "攻击范围： " + ZhaoMain.finger[num1].DetailInfo + "\n\n" + effect;

                }
                else
                {
                    GameObject.Find("ZhaoName").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ZhaoRank").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ZhaoProficiency").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ZhaoIntroduction").GetComponent<TextMesh>().text = "";
                    GameObject.Find("RankValue").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ProficiencyValue").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ZhaoDetail").GetComponent<TextMesh>().text = "";

                    //单独处理进度条
                    GameObject.Find("ProficiencyBackground").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                    GameObject.Find("ProficiencyActual").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                }
                break;
            case "Knife":
                if (num1 < ZhaoMain.knife.Count)
                {
                    GameObject.Find("ZhaoName").GetComponent<TextMesh>().text = ZhaoMain.knife[num1].Name;

                    GameObject.Find("ZhaoRank").GetComponent<TextMesh>().text = "修炼层数：";
                    GameObject.Find("ZhaoProficiency").GetComponent<TextMesh>().text = "熟练度：";
                    GameObject.Find("ZhaoIntroduction").GetComponent<TextMesh>().text = "招式介绍：";

                    AttackStyle attack = new AttackStyle();
                    GameObject.Find("RankValue").GetComponent<TextMesh>().text = attack.Rank.ToString();
                    //GameObject.Find("RankValue").GetComponent<TextMesh>().text = "没找到这个数据";
                    GameObject.Find("ProficiencyValue").GetComponent<TextMesh>().text = attack.Proficiency.ToString() + "/100";

                    //进度条
                    GameObject.Find("ProficiencyBackground").GetComponent<SpriteRenderer>().color = new Color32(108, 153, 192, 255);
                    GameObject.Find("ProficiencyActual").GetComponent<SpriteRenderer>().color = new Color32(226, 115, 110, 255);

                    var v = GameObject.Find("ProficiencyActual").transform;
                    //真实数据
                    //float parcent = (float)attack.Proficiency/100;
                    //测试数据进度条70%
                    float parcent = 0.7F;
                    float xlen = GameObject.Find("ProficiencyBackground").transform.localScale.x;
                    float prex = v.localScale.x;
                    float actualx = xlen * parcent;
                    GameObject.Find("ProficiencyActual").transform.localScale = new Vector3(xlen * parcent, v.localScale.y, v.localScale.z);
                    GameObject.Find("ProficiencyActual").transform.localPosition = new Vector3(v.localPosition.x - (prex - actualx) / 2, v.localPosition.y, v.localPosition.z);

                    //招式效果读取
                    string effect = "";
                    for (int n = 0; n < ZhaoMain.knife[num1].Effects.Count; n++)
                    {
                        effect = effect + ZhaoMain.knife[num1].Effects[n].Name + ":\n" + ZhaoMain.knife[num1].Effects[n].Detail;
                        if (ZhaoMain.fist[num1].Effects[n].Value != 0)
                            effect = effect + "： " + ZhaoMain.knife[num1].Effects[n].Value;
                        effect = effect + "\n" + "持续时间： " + ZhaoMain.knife[num1].Effects[n].TimeValue + "\n";
                    }

                    GameObject.Find("ZhaoDetail").GetComponent<TextMesh>().text = "真气消耗量： " + ZhaoMain.knife[num1].MPCost.ToString() + "\n" + "攻击范围： " + ZhaoMain.knife[num1].DetailInfo + "\n\n" + effect;

                }
                else
                {
                    GameObject.Find("ZhaoName").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ZhaoRank").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ZhaoProficiency").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ZhaoIntroduction").GetComponent<TextMesh>().text = "";
                    GameObject.Find("RankValue").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ProficiencyValue").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ZhaoDetail").GetComponent<TextMesh>().text = "";

                    //单独处理进度条
                    GameObject.Find("ProficiencyBackground").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                    GameObject.Find("ProficiencyActual").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                }
                break;
            case "Sword":
                if (num1 < ZhaoMain.sword.Count)
                {
                    GameObject.Find("ZhaoName").GetComponent<TextMesh>().text = ZhaoMain.sword[num1].Name;

                    GameObject.Find("ZhaoRank").GetComponent<TextMesh>().text = "修炼层数：";
                    GameObject.Find("ZhaoProficiency").GetComponent<TextMesh>().text = "熟练度：";
                    GameObject.Find("ZhaoIntroduction").GetComponent<TextMesh>().text = "招式介绍：";

                    AttackStyle attack = new AttackStyle();
                    GameObject.Find("RankValue").GetComponent<TextMesh>().text = attack.Rank.ToString();
                    //GameObject.Find("RankValue").GetComponent<TextMesh>().text = "没找到这个数据";
                    GameObject.Find("ProficiencyValue").GetComponent<TextMesh>().text = attack.Proficiency.ToString() + "/100";

                    //进度条
                    GameObject.Find("ProficiencyBackground").GetComponent<SpriteRenderer>().color = new Color32(108, 153, 192, 255);
                    GameObject.Find("ProficiencyActual").GetComponent<SpriteRenderer>().color = new Color32(226, 115, 110, 255);

                    var v = GameObject.Find("ProficiencyActual").transform;
                    //真实数据
                    //float parcent = (float)attack.Proficiency/100;
                    //测试数据进度条70%
                    float parcent = 0.7F;
                    float xlen = GameObject.Find("ProficiencyBackground").transform.localScale.x;
                    float prex = v.localScale.x;
                    float actualx = xlen * parcent;
                    GameObject.Find("ProficiencyActual").transform.localScale = new Vector3(xlen * parcent, v.localScale.y, v.localScale.z);
                    GameObject.Find("ProficiencyActual").transform.localPosition = new Vector3(v.localPosition.x - (prex - actualx) / 2, v.localPosition.y, v.localPosition.z);

                    //招式效果读取
                    string effect = "";
                    for (int n = 0; n < ZhaoMain.sword[num1].Effects.Count; n++)
                    {
                        effect = effect + ZhaoMain.sword[num1].Effects[n].Name + ":\n" + ZhaoMain.sword[num1].Effects[n].Detail;
                        if (ZhaoMain.sword[num1].Effects[n].Value != 0)
                            effect = effect + "： " + ZhaoMain.sword[num1].Effects[n].Value;
                        effect = effect + "\n" + "持续时间： " + ZhaoMain.sword[num1].Effects[n].TimeValue + "\n";
                    }

                    GameObject.Find("ZhaoDetail").GetComponent<TextMesh>().text = "真气消耗量： " + ZhaoMain.sword[num1].MPCost.ToString() + "\n" + "攻击范围： " + ZhaoMain.sword[num1].DetailInfo + "\n\n" + effect;

                }
                else
                {
                    GameObject.Find("ZhaoName").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ZhaoRank").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ZhaoProficiency").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ZhaoIntroduction").GetComponent<TextMesh>().text = "";
                    GameObject.Find("RankValue").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ProficiencyValue").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ZhaoDetail").GetComponent<TextMesh>().text = "";

                    //单独处理进度条
                    GameObject.Find("ProficiencyBackground").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                    GameObject.Find("ProficiencyActual").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                }
                break;
            case "Rod":
                if (num1 < ZhaoMain.rod.Count)
                {
                    GameObject.Find("ZhaoName").GetComponent<TextMesh>().text = ZhaoMain.rod[num1].Name;

                    GameObject.Find("ZhaoRank").GetComponent<TextMesh>().text = "修炼层数：";
                    GameObject.Find("ZhaoProficiency").GetComponent<TextMesh>().text = "熟练度：";
                    GameObject.Find("ZhaoIntroduction").GetComponent<TextMesh>().text = "招式介绍：";

                    AttackStyle attack = new AttackStyle();
                    GameObject.Find("RankValue").GetComponent<TextMesh>().text = attack.Rank.ToString();
                    //GameObject.Find("RankValue").GetComponent<TextMesh>().text = "没找到这个数据";
                    GameObject.Find("ProficiencyValue").GetComponent<TextMesh>().text = attack.Proficiency.ToString() + "/100";

                    //进度条
                    GameObject.Find("ProficiencyBackground").GetComponent<SpriteRenderer>().color = new Color32(108, 153, 192, 255);
                    GameObject.Find("ProficiencyActual").GetComponent<SpriteRenderer>().color = new Color32(226, 115, 110, 255);

                    var v = GameObject.Find("ProficiencyActual").transform;
                    //真实数据
                    //float parcent = (float)attack.Proficiency/100;
                    //测试数据进度条70%
                    float parcent = 0.7F;
                    float xlen = GameObject.Find("ProficiencyBackground").transform.localScale.x;
                    float prex = v.localScale.x;
                    float actualx = xlen * parcent;
                    GameObject.Find("ProficiencyActual").transform.localScale = new Vector3(xlen * parcent, v.localScale.y, v.localScale.z);
                    GameObject.Find("ProficiencyActual").transform.localPosition = new Vector3(v.localPosition.x - (prex - actualx) / 2, v.localPosition.y, v.localPosition.z);

                    //招式效果读取
                    string effect = "";
                    for (int n = 0; n < ZhaoMain.rod[num1].Effects.Count; n++)
                    {
                        effect = effect + ZhaoMain.rod[num1].Effects[n].Name + ":\n" + ZhaoMain.rod[num1].Effects[n].Detail;
                        if (ZhaoMain.rod[num1].Effects[n].Value != 0)
                            effect = effect + "： " + ZhaoMain.rod[num1].Effects[n].Value;
                        effect = effect + "\n" + "持续时间： " + ZhaoMain.rod[num1].Effects[n].TimeValue + "\n";
                    }

                    GameObject.Find("ZhaoDetail").GetComponent<TextMesh>().text = "真气消耗量： " + ZhaoMain.rod[num1].MPCost.ToString() + "\n" + "攻击范围： " + ZhaoMain.rod[num1].DetailInfo + "\n\n" + effect;

                }
                else
                {
                    GameObject.Find("ZhaoName").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ZhaoRank").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ZhaoProficiency").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ZhaoIntroduction").GetComponent<TextMesh>().text = "";
                    GameObject.Find("RankValue").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ProficiencyValue").GetComponent<TextMesh>().text = "";
                    GameObject.Find("ZhaoDetail").GetComponent<TextMesh>().text = "";

                    //单独处理进度条
                    GameObject.Find("ProficiencyBackground").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                    GameObject.Find("ProficiencyActual").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                }
                break;
            default:
                break;
        }


        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
