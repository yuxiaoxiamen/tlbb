using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    private Button button;
    public GameObject itemButtonPrefab;                    //商品
    List<GameObject> temp = new List<GameObject>();
    List<GameObject> itemtemp = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            string ButtonName = button.name;

            switch(ButtonName)
            {
                case "buy":
                    if(!string.Equals("Buy",StoreMain.state))
                    {
                        GameObject.Find("Items").SetActive(false);
                        GameObject root = GameObject.Find("Canvas");
                        GameObject goods = root.transform.Find("Goods").gameObject;
                        goods.SetActive(true);

                        StoreMain.state = "Buy";
                        GameObject.Find("buy").GetComponent<Image>().color = Color.clear;
                        GameObject.Find("sell").GetComponent<Image>().color = Color.gray;
                    }
                    break;
                case "sell":
                    if (!string.Equals("Sell", StoreMain.state))
                    {
                        GameObject.Find("Goods").SetActive(false);
                        GameObject root = GameObject.Find("Canvas");
                        GameObject items = root.transform.Find("Items").gameObject;
                        items.SetActive(true);

                        StoreMain.state = "Sell";
                        GameObject.Find("buy").GetComponent<Image>().color = Color.gray;
                        GameObject.Find("sell").GetComponent<Image>().color = Color.clear;
                    }
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
