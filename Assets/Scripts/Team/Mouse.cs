using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mouse : MonoBehaviour
{
    public int Id;
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
        if (!gameObject.name.Equals("Mem_Oper"))
        {
            Para_Pass.characterId = gameObject.transform.parent.GetComponent<Mouse>().Id;
        }
        
    }

    private void OnMouseDown()
    {
        string name = gameObject.name;
        if (name.Equals("Member_Delete"))
        {
            //Para_Pass.characterId = gameObject.transform.parent.GetComponent<Mouse>().Id;
            int protagonist = 0;     //主角id
            if (Para_Pass.characterId != protagonist)   //非主角可请离
            {
                DeleteMember(gameObject);
            }
            else
            {
                Debug.Log("主角无法移出队伍");
            }
            //Debug.Log(name);
        }
        else if (name.Equals("Member_ShuXing"))
        {
            //Para_Pass.characterId = gameObject.transform.parent.GetComponent<Mouse>().Id;
            Debug.Log(name);
            Debug.Log(Para_Pass.characterId);
            MainAttributes.personId = Para_Pass.characterId;
            BasicAttri_GoBack.preScene = "team";
            SceneManager.LoadScene("BasicAttributes");
        }
        else if (name.Equals("Member_ZhaoShi"))
        {
            ZhaoMain.preScene = "team";
            ZhaoMain.person = GlobalData.Persons[Para_Pass.characterId];
            SceneManager.LoadScene("Zhao");
        }
        else if (name.Equals("Member_NeiGong"))
        {
            KongMain.preScene = "team";
            KongMain.person = GlobalData.Persons[Para_Pass.characterId];
            SceneManager.LoadScene("Kong");
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
