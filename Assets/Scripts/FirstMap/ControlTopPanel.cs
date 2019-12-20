using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlTopPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text dateText = transform.Find("dateText").GetComponent<Text>();
        GameDate gameDate = GameRunningData.GetRunningData().date;
        dateText.text = "第" + gameDate.Year + "年" + gameDate.Month + "月" + gameDate.Day + "日"+GetSoltString(gameDate.Slot);
        Button returnButton = transform.Find("returnButton").GetComponent<Button>();
        Place currentPlace = GameRunningData.GetRunningData().currentPlace;
        if(currentPlace == null)
        {
            returnButton.gameObject.SetActive(false);
        }
        else
        {
            returnButton.gameObject.SetActive(true);
        }
        returnButton.onClick.AddListener(() =>
        {
            if(currentPlace is SecondPlace)
            {
                GameRunningData.GetRunningData().currentPlace = ((SecondPlace)currentPlace).PrePlace;
                SceneManager.LoadScene("SecondMap");
            }
            else
            {
                GameRunningData.GetRunningData().currentPlace = null;
                GameRunningData.GetRunningData().player.RowCol = ((FirstPlace)currentPlace).Entry;
                SceneManager.LoadScene("FirstMap");
            }
        });
    }

    string GetSoltString(int solt)
    {
        switch (solt)
        {
            case 1:
                return "早晨";
            case 2:
                return "中午";
            case 3:
                return "晚上";
        }
        return "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
