using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            GameObject root = GameObject.Find("Canvas");
            GameObject dialog = root.transform.Find("Dialog").gameObject;
            dialog.SetActive(true);

            string ButtonName = button.name;
            int num = 0;
            int.TryParse(ButtonName, out num);

            GameObject.Find("num").GetComponent<Text>().text = "1";
            string storetype = StoreMain.storetype;

            switch(storetype)
            {
                case "Alcohol":
                    GameObject.Find("price").GetComponent<Text>().text = StoreMain.alcohol[num].SellingPrice.ToString();
                    break;
                case "Food":
                    GameObject.Find("price").GetComponent<Text>().text = StoreMain.alcohol[num].SellingPrice.ToString();   //食物尚未添加
                    break;
                default:
                    break;
            }

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
