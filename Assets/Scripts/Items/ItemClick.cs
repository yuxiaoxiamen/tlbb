using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClick : MonoBehaviour
{
    private Button button;
    // Start is called before the first frame update

    
    void Start()
    {

        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            float num = float.Parse(gameObject.name);
            int num1 = (int)num;
            ItemKind type = ButtonList.item[num1].Type;
            string itemName= ButtonList.item[num1].Name;

            //Debug.Log(type);

            switch (type)
            {
                case ItemKind.Alcohol:
                    { 
                        //效果
                        Debug.Log("酒");
                        int temp=ButtonList.itemNum[itemName];
                        temp--;
                        if (temp == 0)
                            gameObject.SetActive(false);
                        ButtonList.itemNum[itemName] = temp;
                    }
                    break;
                case ItemKind.Food:
                    {
                        //效果
                        Debug.Log("食物");
                        int temp = ButtonList.itemNum[itemName];
                        temp--;
                        if (temp == 0)
                            gameObject.SetActive(false);
                        ButtonList.itemNum[itemName] = temp;
                    }
                    break;
                case ItemKind.Sword:
                    {
                        //效果
                        Debug.Log("剑");
                        /*int temp = ButtonList.itemNum[itemName];
                        temp--;

                        if (temp == 0)
                            gameObject.SetActive(false);
                        ButtonList.itemNum[itemName] = temp;*/
                        ButtonList.equipmentNow= ButtonList.item[num1].Name;
                        GameObject.Find("equipment").GetComponent<TextMesh>().text = ButtonList.equipmentNow;
                    }
                    break;
                case ItemKind.Knife:
                    {
                        //效果
                        Debug.Log("刀");
                        /*int temp = ButtonList.itemNum[itemName];
                        temp--;
                        if (temp == 0)
                            gameObject.SetActive(false);
                        ButtonList.itemNum[itemName] = temp;*/
                        ButtonList.equipmentNow = ButtonList.item[num1].Name;
                        GameObject.Find("equipment").GetComponent<TextMesh>().text = ButtonList.equipmentNow;

                        
                        
                    }
                    break;
                case ItemKind.Rod:
                    {
                        //效果
                        Debug.Log("棍");
                        /*int temp = ButtonList.itemNum[itemName];
                        temp--;
                        if (temp == 0)
                            gameObject.SetActive(false);
                    

                        ButtonList.itemNum[itemName] = temp;*/
                        ButtonList.equipmentNow = ButtonList.item[num1].Name;
                        GameObject.Find("equipment").GetComponent<TextMesh>().text = ButtonList.equipmentNow;
                        
                    }
                    break;
            }
        });
    }
}
