using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialEffectTool : MonoBehaviour
{
    public static SpecialEffectTool instance;
    public GameObject hpEffectPrefab;
    public GameObject rateEffectPrefab;
    private Transform specialEffectParent;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        specialEffectParent = GameObject.Find("SpecialEffectParent").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HPEffect(Person person, int value, bool isAdd)
    {
        Vector3 offset = new Vector3(-10, 0, 0);
        if (isAdd)
        {
            offset = new Vector3(10, 0, 0);
        }
        Vector3 position = Camera.main.WorldToScreenPoint(person.PersonObject.transform.position + offset);
        GameObject hpEffectObject = Instantiate(hpEffectPrefab, position, Quaternion.identity);
        hpEffectObject.transform.SetParent(specialEffectParent);
        Text text = hpEffectObject.GetComponent<Text>();
        if (isAdd)
        {
            text.color = Color.green;
        }
        text.text = value + "";
        hpEffectObject.transform.DOMove(position + new Vector3(0, 30, 0), 0.4f);
        text.DOFade(0, 0.8f).OnComplete(()=>
        {
            Destroy(hpEffectObject);
        });
    }

    public void RateEffect(Person person, string name)
    {
        Vector3 position = Camera.main.WorldToScreenPoint(person.PersonObject.transform.position + new Vector3(10, 0, 0));
        GameObject rateEffectObject = Instantiate(rateEffectPrefab, position, Quaternion.identity);
        rateEffectObject.transform.SetParent(specialEffectParent);
        Text text = rateEffectObject.GetComponent<Text>();
        text.text = name;
        rateEffectObject.transform.DOScale(rateEffectPrefab.transform.localScale * 2f, 0.4f).OnComplete(()=>
        {
            rateEffectObject.transform.DOMove(position + new Vector3(0, 20, 0), 0.4f);
            text.DOFade(0, 0.8f).OnComplete(() =>
            {
                Destroy(rateEffectObject);
            });
        });
    }
}
