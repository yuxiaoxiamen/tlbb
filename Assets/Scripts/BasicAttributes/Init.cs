using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    public static Dictionary<string, string> attris = new Dictionary<string, string>(); //属性名称，属性释义文字

    static Init()
    {
        attris.Add("BiLi", "影响暴击率");
        attris.Add("GenGu", "影响反击率");
        attris.Add("WuXing", "影响学习武学的效率");
        attris.Add("ShenFa", "影响闪避率和移动范围");
        attris.Add("JinGu", "影响防御");
        attris.Add("ShengMing", "shengming");
        attris.Add("NeiLi", "neili");
        attris.Add("BaoShi", "baoshi");
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
