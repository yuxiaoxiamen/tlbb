using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlchemyCountDown : MonoBehaviour
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
        successPanel.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
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
        if (!isOnce && AlchemyMain.isGameStart)
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
        GameOver();
    }

    void GameOver()
    {
        isGameOver = true;
        if (isSuccess)
        {
            int id = Random.Range(GlobalData.Items.Count - 5, GlobalData.Items.Count);
            GameRunningData.GetRunningData().AddItem(GlobalData.Items[id]);
            successPanel.transform.Find("tipText").GetComponent<Text>().text += 
                System.Environment.NewLine + "获得"+GlobalData.Items[id].Name;
            successPanel.SetActive(true);
        }
        else
        {
            failPanel.SetActive(true);
        }
    }
}
