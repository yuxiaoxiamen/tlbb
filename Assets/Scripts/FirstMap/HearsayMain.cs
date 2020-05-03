using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HearsayMain : MonoBehaviour
{
    public static List<string> says = new List<string>();
    public int lastCount;
    public bool isFirst = true;
    public GameObject hearsayPrefab;
    public GameObject hearsayList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetSays()
    {
        int i = lastCount;
        if (isFirst)
        {
            i = 0;
            isFirst = false;
        }
        for (; i < says.Count; ++i)
        {
            RectTransform hearsayTransform = Instantiate(hearsayPrefab).GetComponent<RectTransform>();
            hearsayTransform.SetParent(gameObject.transform);
            hearsayTransform.localPosition = Vector3.zero;
            hearsayTransform.localRotation = Quaternion.identity;
            hearsayTransform.localScale = Vector3.one;
            hearsayTransform.Find("text").GetComponent<Text>().text = says[i];

        }
        lastCount = says.Count;
    }
}
