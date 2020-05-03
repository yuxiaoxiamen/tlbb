using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherAttributes : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        var y = gameObject.transform;
        float percent = 0;
        int characterId = Para_Pass.characterId;
        switch(y.name)
        {
            case "ShengMing_Current":
                percent = (float)GlobalData.Persons[characterId].BaseData.HP / 3000;   //3000：最大生命
                y.localScale = new Vector3(y.localScale.x * percent, y.localScale.y, y.localScale.z);
                y.localPosition = new Vector3(3*(percent - 1) / 2, y.localPosition.y, y.localPosition.z);
                break;
            case "NeiLi_Current":
                percent = (float)GlobalData.Persons[characterId].BaseData.MP / 3000;    //3000：最大内力
                y.localScale = new Vector3(y.localScale.x * percent, y.localScale.y, y.localScale.z);
                y.localPosition = new Vector3(3*(percent - 1) / 2, y.localPosition.y, y.localPosition.z);
                break;
            case "Baoshi_Current":
                percent = (float)GlobalData.Persons[characterId].BaseData.Energy / 100;     //100：最大饱食
                y.localScale = new Vector3(y.localScale.x * percent, y.localScale.y, y.localScale.z);
                y.localPosition = new Vector3(3*(percent - 1) / 2, y.localPosition.y, y.localPosition.z);
                break;

            case "YiShu_Text":
                y.GetComponent<TextMesh>().text = "    " + GlobalData.Persons[characterId].BaseData.MedicalSkill;
                break;
            case "ChuYi_Text":
                y.GetComponent<TextMesh>().text = "    " + GlobalData.Persons[characterId].BaseData.CookingSkill;
                break;
            case "JiuLi_Text":
                y.GetComponent<TextMesh>().text = "    " + GlobalData.Persons[characterId].BaseData.LiquorSkill;
                break;

            case "CharacterName":
                y.GetComponent<TextMesh>().text = GlobalData.Persons[characterId].BaseData.Name;
                break;

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
