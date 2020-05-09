using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedicalCountDown : MonoBehaviour
{
    public int TotalTime;
    public Text text;
    private bool isOnce = false;
    public static bool isGameOver;
    private GameObject successPanel;
    private GameObject failPanel;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
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
        if (!isOnce && MedicalMain.isGameStart)
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
        if (MedicalMouseControl.Count == 15)
        {
            int promoteValue = GameRunningData.GetRunningData().player.PromoteMedicalSkill();
            successPanel.transform.Find("tipText").GetComponent<Text>().text +=
                System.Environment.NewLine + "医术提升" + promoteValue;
            successPanel.SetActive(true);
        }
        else
        {
            failPanel.SetActive(true);
        }
    }
}
