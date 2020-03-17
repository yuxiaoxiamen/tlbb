using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumChange : MonoBehaviour
{
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            string ButtonName = button.name;

            switch (ButtonName)
            {
                case "up":
                    string temp = GameObject.Find("num").GetComponent<Text>().text;
                    string price= GameObject.Find("price").GetComponent<Text>().text;
                    int num = 0;
                    int pnum = 0;
                    int.TryParse(temp, out num);
                    int.TryParse(price, out pnum);

                    int perprice = pnum / num;
                    num++;
                    int totalprice = perprice * num;
                    GameObject.Find("num").GetComponent<Text>().text = num.ToString();
                    GameObject.Find("price").GetComponent<Text>().text = totalprice.ToString();
                    break;
                case "down":
                    string temp2 = GameObject.Find("num").GetComponent<Text>().text;
                    string price2 = GameObject.Find("price").GetComponent<Text>().text;
                    int num2 = 0;
                    int pnum2 = 0;
                    int.TryParse(temp2, out num2);
                    int.TryParse(price2, out pnum2);
                    if (num2>1)
                    {
                        int perprice2 = pnum2 / num2;
                        num2--;
                        int totalprice2 = perprice2 * num2;
                        GameObject.Find("num").GetComponent<Text>().text = num2.ToString();
                        GameObject.Find("price").GetComponent<Text>().text = totalprice2.ToString();
                    }
                    break;
                case "all":
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
