using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes_Init : MonoBehaviour
{

    static Attributes_Init()
    {

    }

    void Start()
    {
        var label = gameObject.transform;
        
        int memIndex = Member_Init.MembersName.IndexOf(label.parent.transform.Find("Member_Name").GetComponent<TextMesh>().text);

        switch (gameObject.name)
        {
            case "BiLi":
                label.GetComponent<TextMesh>().text = label.GetComponent<TextMesh>().text + "    " + Member_Init.Members[memIndex].BaseData.Bi;
                break;
            case "GenGu":
                label.GetComponent<TextMesh>().text = label.GetComponent<TextMesh>().text + "    " + Member_Init.Members[memIndex].BaseData.Gen;
                break;
            case "WuXing":
                label.GetComponent<TextMesh>().text = label.GetComponent<TextMesh>().text + "    " + Member_Init.Members[memIndex].BaseData.Wu;
                break;
            case "ShenFa":
                label.GetComponent<TextMesh>().text = label.GetComponent<TextMesh>().text + "    " + Member_Init.Members[memIndex].BaseData.Shen;
                break;
            case "JinGu":
                label.GetComponent<TextMesh>().text = label.GetComponent<TextMesh>().text + "    " + Member_Init.Members[memIndex].BaseData.Jin;
                break;
            case "ShengMing":
                label.GetComponent<TextMesh>().text = label.GetComponent<TextMesh>().text + "    " + Member_Init.Members[memIndex].BaseData.HP;
                break;
            case "NeiLi":
                label.GetComponent<TextMesh>().text = label.GetComponent<TextMesh>().text + "    " + Member_Init.Members[memIndex].BaseData.MP;
                break;
            case "BaoShi":
                label.GetComponent<TextMesh>().text = label.GetComponent<TextMesh>().text + "    " + Member_Init.Members[memIndex].BaseData.Energy;
                break;
        }


    }


    void Update()
    {

    }
}
