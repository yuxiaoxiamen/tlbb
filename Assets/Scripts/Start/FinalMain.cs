using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalMain : MonoBehaviour
{
    public static bool isSuccess;
    public Text mainText;
    public Text personText;
    private string key;
    private bool isCompleted;
    // Start is called before the first frame update
    void Start()
    {
        key = GameRunningData.GetRunningData().GetPlaceDateKey();
        //key = "9/2-1-27-2";
        var conflict = GlobalData.MainLineConflicts[key];
        
        //isSuccess = true;
        //conflict.IsZ = false;

        string textString = "";
        if (conflict.IsZ)
        {
            if (isSuccess)
            {
                textString = GetText(4);
            }
            else
            {
                textString = GetText(5);
            }
        }
        else
        {
            if (isSuccess)
            {
                textString = GetText(6);
            }
            else
            {
                textString = GetText(7);
            }
        }
        mainText.DOText(textString, textString.Length * 0.07f).SetEase(Ease.Linear).OnComplete(() =>
        {
            StartCoroutine(DisplayPersonText());
        });
    }

    IEnumerator DisplayPersonText()
    {
        yield return new WaitForSeconds(1f);
        mainText.text = "";
        string textString = "制作人员" + System.Environment.NewLine + "冷琪瑶  刘雨璇  张砾夫  喻枭  于梦恺";
        personText.DOText(textString, textString.Length * 0.07f).SetEase(Ease.Linear).OnComplete(() =>
        {
            isCompleted = true;
        });
    }

    string GetText(int type)
    {
        string text = "";
        foreach (var mc in GlobalData.MainConversations[key])
        {
            if (mc.ContentType == type)
            {
                text = mc.Content;
            }
        }
        text = text.Replace("{}", GameRunningData.GetRunningData().player.BaseData.Name);
        return text;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCompleted && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Start");
        }
    }
}
