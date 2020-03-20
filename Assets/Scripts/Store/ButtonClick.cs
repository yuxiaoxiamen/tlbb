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
            GameObject.Find("price").GetComponent<Text>().text = GoodDisplay.good[num].SellingPrice.ToString();

        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
