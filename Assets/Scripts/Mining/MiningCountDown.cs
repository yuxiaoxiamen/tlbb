using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiningCountDown : MonoBehaviour
{
    public GameObject text;
    public int TotalTime;
    public static GameObject failPanel;
    public static GameObject successPanel;
    public static bool isGameOver;
    private static GameObject moneyText;
    private bool isOnce = false;

    void Start()
    {
        failPanel = GameObject.Find("failPanel");
        moneyText = failPanel.transform.Find("moneyText").gameObject;
        Button failButton = failPanel.transform.Find("Button").GetComponent<Button>();
        failButton.onClick.AddListener(()=>
        {

        });
        failPanel.SetActive(false);

        successPanel = GameObject.Find("successPanel");
        Button successButton = successPanel.transform.Find("Button").GetComponent<Button>();
        successButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Forge");
        });
        successPanel.SetActive(false);
    }

    private void Update()
    {
        if (!isOnce && MineralControl.isGameStart)
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
            text.GetComponent<TextMesh>().text = TotalTime.ToString();
            yield return new WaitForSeconds(1);
            TotalTime--;
        }
        if (!isGameOver)
        {
            GameOver();
        }
    }

    public static void GameOver()
    {
        isGameOver = true;
        if (HookControl.copperNumber >= MineralControl.Manual.CopperNumber &&
            HookControl.ironNumber >= MineralControl.Manual.IronNumber &&
            HookControl.silverNumber >= MineralControl.Manual.SilverNumber &&
            HookControl.goldNumber >= MineralControl.Manual.GoldNumber)
        {
            successPanel.SetActive(true);
        }
        else
        {
            int money = 10;
            moneyText.GetComponent<Text>().text += money;
            failPanel.SetActive(true);
        }
    }
}
