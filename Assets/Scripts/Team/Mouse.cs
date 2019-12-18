using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name.Equals("Mem_Oper"))
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        string name = gameObject.name;
        if (name.Equals("Member_Delete"))
        {
            DeleteMember(gameObject);
            Debug.Log(name);
        }
        else if (name.Equals("Member_Detail"))
        {
            Debug.Log(name);
        }
        else { }
    
    }
    public void DeleteMember(GameObject xMem)
    {
        //Member_Init.memberCount--;  

        GameObject delMem = xMem.transform.parent.parent.gameObject;
        string memName = delMem.transform.Find("Member_Name").GetComponent<TextMesh>().text;
        Debug.Log(memName + "已离队");
        delMem.SetActive(false);
        int delIndex = Member_Init.MembersName.IndexOf(memName);
        if (delIndex == Member_Init.memberCount - 1 && delIndex % 4 == 0)
        {
            Member_Init.currentPage--;
        }
        //Debug.Log(delIndex);
        Member_Init.Members.Remove(Member_Init.Members[delIndex]);
        Member_Init.MembersName.Remove(memName);
    }


}
