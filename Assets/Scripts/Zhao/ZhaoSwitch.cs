using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZhaoSwitch : MonoBehaviour
{
    private Button button;

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
            string type = ZhaoMain.zhaotype;     //获取当前招式类型

            if (!string.Equals(ButtonName, type))
            {
                //处理解释面板
                GameObject.Find("ZhaoName").GetComponent<TextMesh>().text = "";
                GameObject.Find("ZhaoRank").GetComponent<TextMesh>().text = "";
                GameObject.Find("ZhaoProficiency").GetComponent<TextMesh>().text = "";
                GameObject.Find("RankValue").GetComponent<TextMesh>().text = "";
                GameObject.Find("ProficiencyValue").GetComponent<TextMesh>().text = "";
                GameObject.Find("ZhaoDetail").GetComponent<TextMesh>().text = "";

                //单独处理进度条
                GameObject.Find("ProficiencyBackground").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                GameObject.Find("ProficiencyActual").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);

                //清空名字
                for (int n = 0; n < 20; n++)
                    GameObject.Find("book"+n.ToString()).GetComponent<TextMesh>().text = "";

                ZhaoMain.zhaotype = ButtonName;

                GameObject.Find(type).GetComponent<Button>().image.color = new Color32(255, 255, 255, 255);
                GameObject.Find(ButtonName).GetComponent<Button>().image.color = new Color32(212, 168, 93, 255);

                //加载新招式

                switch (ButtonName)
                {
                    case "Fist":
                        for (int m = 0; m < ZhaoMain.fist.Count; m++)
                            GameObject.Find("book"+m.ToString()).GetComponent<TextMesh>().text = Textchange(ZhaoMain.fist[m].FixData.Name);
                        break;
                    case "Palm":
                        for (int m = 0; m < ZhaoMain.palm.Count; m++)
                            GameObject.Find("book" + m.ToString()).GetComponent<TextMesh>().text = Textchange(ZhaoMain.palm[m].FixData.Name);
                        break;
                    case "Finger":
                        for (int m = 0; m < ZhaoMain.finger.Count; m++)
                            GameObject.Find("book" + m.ToString()).GetComponent<TextMesh>().text = Textchange(ZhaoMain.finger[m].FixData.Name);
                        break;
                    case "Knife":
                        for (int m = 0; m < ZhaoMain.knife.Count; m++)
                            GameObject.Find("book" + m.ToString()).GetComponent<TextMesh>().text = Textchange(ZhaoMain.knife[m].FixData.Name);
                        break;
                    case "Sword":
                        for (int m = 0; m < ZhaoMain.sword.Count; m++)
                            GameObject.Find("book" + m.ToString()).GetComponent<TextMesh>().text = Textchange(ZhaoMain.sword[m].FixData.Name);
                        break;
                    case "Rod":
                        for (int m = 0; m < ZhaoMain.rod.Count; m++)
                            GameObject.Find("book" + m.ToString()).GetComponent<TextMesh>().text = Textchange(ZhaoMain.rod[m].FixData.Name);
                        break;
                    default:
                        break;

                }

            }



        });
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
