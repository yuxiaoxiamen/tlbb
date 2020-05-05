using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Member_Init : MonoBehaviour
{
    public GameObject MemberInfo;

    static readonly float M_INTERVAL = 4;
    static readonly float M_ORIGIN = -6;

    public static int memberCount = 0;
    public static int pageCount = 1;
    public static int currentPage = 1;

    public static List<Person> Members = new List<Person>();     //人物序列
    public static List<string> MembersName = new List<string>(); //姓名序列

    //public static List<string> names = new List<string>();
    public static int pageChangeSign = 1-currentPage;

    


    // Start is called before the first frame update
    void Start()
    {
        //****************************测试数据******************************
        //int count = 5;
        //for (int i = 0; i < count ; ++i)
        //{
        //    Members.Add(GlobalData.Persons[i]);
        //    Debug.Log(GlobalData.Persons[i].BaseData.Name);
        //}
        Members = GameRunningData.GetRunningData().teammates;

        for (int i=0;i<Members.ToArray().Length; ++i)
        {
            MembersName.Add(Members[i].BaseData.Name);
        }


            memberCount = Members.ToArray().Length;
            pageCount = memberCount / 4 + 1;


            for (int i = 0; i < memberCount; ++i)
            {
                var newMem = Instantiate(MemberInfo).transform;
                newMem.position = new Vector3(M_INTERVAL * i + M_ORIGIN, -1, 0);
                GameObject memName = newMem.Find("Member_Name").gameObject;
                memName.transform.GetComponent<TextMesh>().text = Members[i].BaseData.Name;

            newMem.Find("Mem_Oper").GetComponent<Mouse>().Id = Members[i].BaseData.Id;
            Debug.Log("输出"+newMem.Find("Mem_Oper").GetComponent<Mouse>().Id);

            }

        

    }

    // Update is called once per frame
    void Update()
    {
        memberCount = Members.ToArray().Length;
        pageCount = (memberCount-1) / 4 + 1;
        pageChangeSign = 1 - currentPage;
    }

    

}
