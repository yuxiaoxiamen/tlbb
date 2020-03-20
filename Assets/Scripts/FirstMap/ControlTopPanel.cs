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
        dateText.text = GameRunningData.GetRunningData().date.GetDateText();
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
