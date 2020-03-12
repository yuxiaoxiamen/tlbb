using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GongList : MonoBehaviour
{

    public static List<InnerGongFixData> gong = new List<InnerGongFixData>();     //内功读取接口
    public GameObject gongButtonPrefab;

    List<GameObject> temp = new List<GameObject>();


    static GongList()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

        gong.Add(GlobalData.InnerGongFixDatas[0]);
        gong.Add(GlobalData.InnerGongFixDatas[1]);
        gong.Add(GlobalData.InnerGongFixDatas[2]);
        gong.Add(GlobalData.InnerGongFixDatas[3]);
        gong.Add(GlobalData.InnerGongFixDatas[4]);
        gong.Add(GlobalData.InnerGongFixDatas[5]);
        for (int i = 0; i < gong.Count; ++i)
        {


            GameObject gongButton;

            gongButton = Instantiate(gongButtonPrefab);
            RectTransform gongButtonTransform = gongButton.GetComponent<RectTransform>();
            gongButtonTransform.SetParent(gameObject.GetComponent<RectTransform>());

            temp.Add(gongButton);
            temp[i].name = i.ToString();
            temp[i].transform.Find("Text").GetComponent<Text>().text = gong[i].Name;

        }
    }



}
