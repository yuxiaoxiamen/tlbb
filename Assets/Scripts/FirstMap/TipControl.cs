using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipControl : MonoBehaviour
{
    public static TipControl instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void SetTip(string text)
    {
        GameObject tipObject = Instantiate(transform.Find("tipReal").gameObject);
        tipObject.transform.Find("text").GetComponent<Text>().text = text;
        var tipTransform = tipObject.GetComponent<RectTransform>();
        tipTransform.SetParent(gameObject.GetComponent<RectTransform>());
        tipTransform.localPosition = Vector3.zero;
        tipTransform.localRotation = Quaternion.identity;
        tipTransform.localScale = new Vector3(0, 1, 1);
        tipObject.transform.DOScaleX(1, 0.4f).OnComplete(() =>
        {
            StartCoroutine(RemoveTip(tipObject));
        });
    }

    IEnumerator RemoveTip(GameObject tipObject)
    {
        yield return new WaitForSeconds(0.2f);
        tipObject.transform.DOScaleX(0, 0.4f).OnComplete(() =>
        {
            Destroy(tipObject);
        });
    }
}
