using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    public static Dictionary<string,double[]> attributes = new Dictionary<string,double[]>();  //属性名称，属性当前值与最大值数组
    static Test()
    {
        
        attributes.Add("BiLi_Current",new double[2] { 50.00, 100.00 });
        attributes.Add("GenGu_Current", new double[2] { 50.00, 100.00 });
        attributes.Add("WuXing_Current", new double[2] { 30.00, 100.00 });
        attributes.Add("ShenFa_Current", new double[2] { 50.00, 100.00 });
        attributes.Add("JinGu_Current", new double[2] { 20.00, 100.00 });
        attributes.Add("ShengMing_Current", new double[2] { 50.00, 100.00 });
        attributes.Add("NeiLi_Current", new double[2] { 80.00, 100.00 });
        attributes.Add("BaoShi_Current", new double[2] { 0.00, 100.00 });
    }
    // Start is called before the first frame update
    void Start()
    {
        var y = gameObject.transform;

        float percent = (float)(attributes[gameObject.name][0] / attributes[gameObject.name][1]);
        
        y.localScale = new Vector3(y.localScale.x * percent, y.localScale.y, y.localScale.z);
        y.localPosition = new Vector3((percent-1)/2,y.localPosition.y,y.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
