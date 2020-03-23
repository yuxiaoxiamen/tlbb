﻿using System.Collections;
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

        });
        failPanel = GameObject.Find("failPanel");
        failPanel.SetActive(false);
        failPanel.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {

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
            int randomIndex = Random.Range(63, 68);
            Good item = GlobalData.Items[randomIndex];
            GameRunningData.GetRunningData().belongings.Add(item);
            successPanel.transform.Find("tipText").GetComponent<Text>().text += item.Name;
            successPanel.SetActive(true);
        }
        else
        {
            failPanel.SetActive(true);
        }
    }
}
