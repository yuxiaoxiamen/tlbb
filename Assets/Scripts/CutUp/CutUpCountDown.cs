using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutUpCountDown : MonoBehaviour
{
    public int TotalTime;
    public Text text;
    private bool isOnce = false;
    public static bool isGameOver;
    public static bool isSuccess;
    public GameObject successPanel;
    public GameObject failPanel;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        isSuccess = false;
        successPanel = GameObject.Find("successPanel");
        successPanel.SetActive(false);
        successPanel.transform.Find("Button").GetComponent<Button>().onClick.AddListener(()=> 
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
            successPanel.SetActive(true);
        }
        else
        {
            failPanel.SetActive(true);
        }
    }
}
