using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Team_Member_Attributes : MonoBehaviour
{
    static readonly double XRANGE = 1.7;
    static readonly double YRANGE = 4.5;

    //public GameObject MemDel;
    //public GameObject MemDetInfo;
    // Start is called before the first frame update
    void Start()
    {
        int memIndex = Member_Init.MembersName.IndexOf(gameObject.transform.Find("Member_Name").GetComponent<TextMesh>().text);

        Debug.Log(memIndex);

    }

    // Update is called once per frame
    void Update()
    {
        int memIndex = Member_Init.MembersName.IndexOf(gameObject.transform.Find("Member_Name").GetComponent<TextMesh>().text);
        var gameObject_ = gameObject.transform;
        gameObject_.position = new Vector3(4 * memIndex - 6 + Member_Init.pageChangeSign * 16, gameObject.transform.position.y, gameObject.transform.position.z);
        //Debug.Log(gameObject_.position);

        var mouseP = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        if (System.Math.Abs(mouseP.x - gameObject.transform.position.x) <= XRANGE && System.Math.Abs(mouseP.y - gameObject.transform.position.y) <= YRANGE)
        {
            gameObject.transform.Find("Mem_Oper").gameObject.SetActive(true);
            if (gameObject.transform.Find("Mem_Oper").gameObject.transform.GetComponent<Mouse>().Id == 0)
            {
                gameObject.transform.Find("Mem_Oper").gameObject.transform.Find("Member_Delete").gameObject.SetActive(false);
            }
        }
        else
        {
            gameObject.transform.Find("Mem_Oper").gameObject.SetActive(false);
        }


    }



}
