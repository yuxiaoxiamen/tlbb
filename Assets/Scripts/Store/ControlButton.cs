using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!ConfirmOperation.instance.isInConfirm)
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                string ButtonName = button.name;
                switch (ButtonName)
                {
                    case "buy":
                        if (!GoodDisplay.isBuy)
                        {
                            transform.parent.Find("Items").gameObject.SetActive(false);
                            transform.parent.Find("Goods").gameObject.SetActive(true);

                            GoodDisplay.isBuy = true;
                            GameObject.Find("buy").GetComponent<Image>().sprite = Resources.Load<Sprite>("ui/buyY");
                            GameObject.Find("sell").GetComponent<Image>().sprite = Resources.Load<Sprite>("ui/sellN");
                        }
                        break;
                    case "sell":
                        if (GoodDisplay.isBuy)
                        {
                            transform.parent.Find("Goods").gameObject.SetActive(false);
                            GameObject items = transform.parent.Find("Items").gameObject;
                            items.SetActive(true);

                            GoodDisplay.isBuy = false;
                            items.transform.Find("Viewport").Find("Content").GetComponent<GoodDisplay>().SetItemList();
                            GameObject.Find("buy").GetComponent<Image>().sprite = Resources.Load<Sprite>("ui/buyN");
                            GameObject.Find("sell").GetComponent<Image>().sprite = Resources.Load<Sprite>("ui/sellY");
                        }
                        break;
                    default:
                        break;
                }
            });
        }
    }
}
