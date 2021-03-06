﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherAttributes : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        var y = gameObject.transform;
        float percent = 0;
        int characterId = MainAttributes.personId;
        switch(y.name)
        {
            case "ShengMing_Current":
                percent = (float)GlobalData.Persons[characterId].CurrentHP / GlobalData.Persons[characterId].BaseData.HP; 
                y.localScale = new Vector3(y.localScale.x * percent, y.localScale.y, y.localScale.z);
                y.localPosition = new Vector3(3*(percent - 1) / 2, y.localPosition.y, y.localPosition.z);
                break;
            case "ShengMing_Value":
                y.GetComponent<TextMesh>().text = GlobalData.Persons[characterId].CurrentHP + " / " + GlobalData.Persons[characterId].BaseData.HP;
                break;

            case "NeiLi_Current":
                percent = (float)GlobalData.Persons[characterId].CurrentMP / GlobalData.Persons[characterId].BaseData.MP;
                y.localScale = new Vector3(y.localScale.x * percent, y.localScale.y, y.localScale.z);
                y.localPosition = new Vector3(3*(percent - 1) / 2, y.localPosition.y, y.localPosition.z);
                break;
            case "NeiLi_Value":
                y.GetComponent<TextMesh>().text = GlobalData.Persons[characterId].CurrentMP + " / " + GlobalData.Persons[characterId].BaseData.MP;
                break;

            case "BaoShi_Current":
                percent = (float)GlobalData.Persons[characterId].CurrentEnergy / GlobalData.Persons[characterId].BaseData.Energy;
                y.localScale = new Vector3(y.localScale.x * percent, y.localScale.y, y.localScale.z);
                y.localPosition = new Vector3(3*(percent - 1) / 2, y.localPosition.y, y.localPosition.z);
                break;
            case "BaoShi_Value":
                y.GetComponent<TextMesh>().text = GlobalData.Persons[characterId].CurrentEnergy + " / " + GlobalData.Persons[characterId].BaseData.Energy;
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
            case "CharacterImage":
                y.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("head/" + GlobalData.Persons[characterId].BaseData.HeadPortrait);
                break;

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
