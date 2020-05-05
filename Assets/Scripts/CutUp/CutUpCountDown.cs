using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutUpCountDown : MonoBehaviour
{
    public int TotalTime;
    public Text text;
    private bool isOnce = false;
    public static bool isGameOver;
    public static bool isSuccess;
    private GameObject successPanel;
    private GameObject failPanel;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        isSuccess = false;
        successPanel = GameObject.Find("successPanel");
        successPanel.SetActive(false);
        successPanel.transform.Find("Button").GetComponent<Button>().onClick.AddListener(()=> 
        {
            GameRunningData.GetRunningData().ReturnToMap();
        });
        failPanel = GameObject.Find("failPanel");
        failPanel.SetActive(false);
        failPanel.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameRunningData.GetRunningData().ReturnToMap();
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOnce && CutUpMain.isGameStart)
        {
            StartCoroutine(Count());
            isOnce = true;
        }
    }

    IEnumerator Count()
    {
        while (TotalTime >= 0)
        {
            if (isGameOver)
            {
                break;
            }
            text.text = TotalTime.ToString();
            yield return new WaitForSeconds(1);
            TotalTime--;
        }
        CutUpMain.isGameStart = false;
        GameOver();
    }

    void GameOver()
    {
        if (isSuccess)
        {
            GameRunningData.GetRunningData().AddItem(CutUpMain.manual.Item);
            successPanel.SetActive(true);
            successPanel.transform.Find("tipText").GetComponent<Text>().text += 
                System.Environment.NewLine + ("获得" + CutUpMain.manual.Item.Name+"")+ System.Environment.NewLine
                +"厨艺提高"+GameRunningData.GetRunningData().player.PromoteCookingSkill();
        }
        else
        {
            failPanel.SetActive(true);
        }
    }
}
