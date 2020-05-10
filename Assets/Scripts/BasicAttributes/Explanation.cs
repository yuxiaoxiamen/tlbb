using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explanation : MonoBehaviour
{
    public GameObject ex; //弹出的释义面板
    public GameObject text; //释义文字内容

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseEnter()
    {
        ex.SetActive(true);
        var text_ = text.transform;
        //Debug.Log(gameObject.name.Split('_')[0]);
        switch (gameObject.name.Split('_')[0])
        {
            case "BiLi":
                text_.GetComponent<TextMesh>().text = "影响暴击率      当前暴击率为： " + GlobalData.Persons[Para_Pass.characterId].CountCrit() + "%";
                break;
            case "GenGu":
                text_.GetComponent<TextMesh>().text = "影响反击率      当前反击率为： " + GlobalData.Persons[Para_Pass.characterId].CountCounterattack() + "%";
                break;
            case "WuXing":
                text_.GetComponent<TextMesh>().text = "影响学习武学的效率";
                break;
            case "ShenFa":
                text_.GetComponent<TextMesh>().text = "影响闪避率和移动范围      当前闪避率为： " + GlobalData.Persons[Para_Pass.characterId].CountDodge() + "%      当前移动范围为："  + GlobalData.Persons[Para_Pass.characterId].CountMoveRank();
                break;
            case "JinGu":
                text_.GetComponent<TextMesh>().text = "影响防御力      当前防御力为： " + GlobalData.Persons[Para_Pass.characterId].CountDefend();
                break;

        }
       
    }

    private void OnMouseExit()
    {
        ex.SetActive(false);
    }
}
