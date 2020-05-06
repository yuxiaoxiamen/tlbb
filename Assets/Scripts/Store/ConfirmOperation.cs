using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmOperation : MonoBehaviour
{
    public static ConfirmOperation instance;
    private Good item;
    private Text moneyText;
    private int price;
    private Text priceText;
    private Text numText;
    Transform okButton;
    Transform confirmTransform;
    public bool isInConfirm;
    public Button returnButton;

    public GameObject items;
    public Image goodBg;
    public Image handleBg1;
    public Image handleBg2;
    public Image people;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        isInConfirm = false;
        confirmTransform = GameObject.Find("confirm").transform;
        moneyText = GameObject.Find("money").transform.GetChild(0).GetComponent<Text>();
        numText = transform.Find("num").GetComponent<Text>();
        numText.text = "1";
        priceText = transform.Find("price").GetComponent<Text>();
        InitStore();
        
        transform.Find("up").GetComponent<Button>().onClick.AddListener(() =>
        {
            int num = int.Parse(numText.text) + 1;
            if (!GoodDisplay.isBuy && num >= item.Number)
            {
                num = item.Number;
            }
            numText.text = num + "";
            priceText.text = price * num + "";
        });
        transform.Find("down").GetComponent<Button>().onClick.AddListener(() =>
        {
            int num = int.Parse(numText.text) - 1;
            if (num <= 1)
            {
                num = 1;
            }
            numText.text = num + "";
            priceText.text = price * num + "";
        });
        transform.Find("cancel").GetComponent<Button>().onClick.AddListener(() =>
        {
            transform.gameObject.SetActive(false);
            isInConfirm = false;
            numText.text = "1";
        });
        okButton = transform.Find("ok");
        okButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            confirmTransform.gameObject.SetActive(true);
        });
        gameObject.SetActive(false);

        confirmTransform.Find("ok").GetComponent<Button>().onClick.AddListener(() =>
        {
            int number = int.Parse(numText.text);
            int allPrice = number * price;
            if (GoodDisplay.isBuy)
            {
                if (allPrice > GameRunningData.GetRunningData().money)
                {
                    GameObject.Find("info").transform.Find("text").GetComponent<Text>().text = "你的钱不够";
                }
                else
                {
                    var belongings = GameRunningData.GetRunningData().belongings;
                    if (belongings.Contains(item))
                    {
                        item.Number += number;
                    }
                    else
                    {
                        belongings.Add(item);
                        item.Number = number;
                    }
                    GameRunningData.GetRunningData().money -= allPrice;
                    SetMoneyText();
                }
            }
            else
            {
                if (item.Number == number)
                {
                    GameRunningData.GetRunningData().belongings.Remove(item);
                    GameObject.Find("Items").transform.Find("Viewport").Find("Content").
                    GetComponent<GoodDisplay>().SetItemList();
                }
                GameRunningData.GetRunningData().money += allPrice;
                SetMoneyText();
                item.Number -= number;
            }
            numText.text = "1";
            transform.gameObject.SetActive(false);
            isInConfirm = false;
            confirmTransform.gameObject.SetActive(false);
        });
        confirmTransform.Find("cancel").GetComponent<Button>().onClick.AddListener(() =>
        {
            confirmTransform.gameObject.SetActive(false);
            GameObject.Find("info").transform.Find("text").GetComponent<Text>().text = "";
        });
        confirmTransform.gameObject.SetActive(false);
    }

    void SetMoneyText()
    {
        moneyText.text = "当前资产：" + GameRunningData.GetRunningData().money + "钱";
    }

    public void SetItem(Good good)
    {
        item = good;
        price = item.SellingPrice;
        if (GoodDisplay.isBuy)
        {
            price = item.BuyingPrice;
        }
        priceText.text = price + "";

        okButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("ui/sellButton");
        if (GoodDisplay.isBuy)
        {
            okButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("ui/buyButton");
        }
        confirmTransform.GetComponent<Image>().sprite = Resources.Load<Sprite>("ui/sellBg");
        if (GoodDisplay.isBuy)
        {
            confirmTransform.GetComponent<Image>().sprite = Resources.Load<Sprite>("ui/buyBg");
        }
        transform.Find("itempic").GetComponent<Image>().sprite = Resources.Load<Sprite>("itemIcon/" + item.Id);
        gameObject.SetActive(true);
        isInConfirm = true;
    }

    public void InitStore()
    {
        ChangeImage();
        items.SetActive(false);
        returnButton.onClick.AddListener(() =>
        {
            ControlBottomPanel.IsBanPane = false;
            ThridMapMain.storeUI.SetActive(false);
            ThridMapMain.ShowAllHeads();
        });
    }

    public void ChangeImage()
    {
        moneyText.text = GameRunningData.GetRunningData().money + "";
        if (GoodDisplay.storeType == "Alcohol")
        {
            people.sprite = Resources.Load<Sprite>("ui/waiter");
        }
        else if (GoodDisplay.storeType == "Blacksmith")
        {
            items.GetComponent<Image>().sprite = Resources.Load<Sprite>("ui/scrollBg");
            goodBg.sprite = Resources.Load<Sprite>("ui/scrollBg");
            handleBg1.sprite = Resources.Load<Sprite>("ui/handle");
            handleBg2.sprite = Resources.Load<Sprite>("ui/handle");
            people.sprite = Resources.Load<Sprite>("ui/blacksmith");
        }
    }
}
