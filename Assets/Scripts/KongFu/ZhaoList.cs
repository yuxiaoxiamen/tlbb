using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZhaoList : MonoBehaviour
{
    public static List<AttackStyle> zh = new List<AttackStyle>();    //招式读取接口
    public GameObject zhaoButtonPrefab;

    List<GameObject> temp = new List<GameObject>();

    static ZhaoList()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in GlobalData.Persons[0].BaseData.AttackStyles)
        { zh.Add(item); }


        for (int i = 0; i < zh.Count; ++i)
        {
            GameObject zhaoButton;

            zhaoButton = Instantiate(zhaoButtonPrefab);
            RectTransform zhaoButtonTransform = zhaoButton.GetComponent<RectTransform>();
            zhaoButtonTransform.SetParent(gameObject.GetComponent<RectTransform>());

            temp.Add(zhaoButton);
            temp[i].name = i.ToString();
            temp[i].transform.Find("Text").GetComponent<Text>().text = zh[i].FixData.Name + "         " + zh[i].Rank + "       " + zh[i].Proficiency;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
