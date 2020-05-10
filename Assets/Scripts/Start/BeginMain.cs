using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeginMain : MonoBehaviour
{
    public RectTransform rightPosition;
    public GameObject textObject;
    private float widthInterval;
    private RectTransform preTrans;
    public RectTransform parent;
    private bool isCompleted;
    // Start is called before the first frame update
    void Start()
    {
        string s = Resources.Load<TextAsset>("jsonData/游戏开始").text;
        var textTransform = textObject.GetComponent<RectTransform>();
        widthInterval = rightPosition.position.x - textTransform.position.x;
        preTrans = textTransform;
        s = s.Replace("{}", GameRunningData.GetRunningData().player.BaseData.Name);
        DoText(0, s, textObject.GetComponent<Text>());
    }

    void DoText(int startIndex, string s, Text text)
    {
        int length = 14;
        if(startIndex + length > s.Length)
        {
            length = s.Length - startIndex;
        }
        text.DOText(s.Substring(startIndex, length), length * 0.07f).SetEase(Ease.Linear).OnComplete(() =>
        {
            if(startIndex < s.Length)
            {
                var tOj = Instantiate(textObject);
                tOj.GetComponent<Text>().text = "";
                var tTrans = tOj.GetComponent<RectTransform>();
                tTrans.SetParent(parent);
                tTrans.localScale = textObject.GetComponent<RectTransform>().localScale;
                tTrans.position = preTrans.position + new Vector3(widthInterval, 0);
                preTrans = tTrans;
                DoText(startIndex + length, s, tOj.GetComponent<Text>());
            }
            else
            {
                isCompleted = true;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if(isCompleted && Input.GetMouseButtonDown(0))
        {
            GameRunningData.GetRunningData().ReturnToMap();
        }
    }
}
