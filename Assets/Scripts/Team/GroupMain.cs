using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GroupMain : MonoBehaviour
{
    public GameObject personPrefab;
    public Button returnButton;
    // Start is called before the first frame update
    void Start()
    {
        SetPerson();
        returnButton.onClick.AddListener(() =>
        {
            GameRunningData.GetRunningData().ReturnToMap();
        });
    }

    void SetPerson()
    {
        for(int i = 0; i < transform.childCount; ++i)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        var persons = GameRunningData.GetRunningData().teammates;
        foreach(Person p in persons)
        {
            GameObject pObject = Instantiate(personPrefab);
            RectTransform pTransform = pObject.GetComponent<RectTransform>();
            RectTransform parent = GetComponent<RectTransform>();
            pTransform.SetParent(parent);
            pTransform.localScale = Vector3.one;
            pTransform.localPosition = Vector3.zero;
            pTransform.localRotation = Quaternion.identity;
            pTransform.Find("head").GetComponent<Image>().sprite = Resources.Load<Sprite>("head/" + p.BaseData.HeadPortrait);
            pTransform.Find("name").GetComponent<Text>().text = p.BaseData.Name;
            Transform op = pTransform.Find("op");
            op.Find("attr").GetComponent<Button>().onClick.AddListener(() =>
            {
                MainAttributes.personId = p.BaseData.Id;
                BasicAttri_GoBack.preScene = "team";
                SceneManager.LoadScene("BasicAttributes");
            });
            op.Find("gong").GetComponent<Button>().onClick.AddListener(() =>
            {
                KongMain.preScene = "team";
                KongMain.person = p;
                SceneManager.LoadScene("Kong");
            });
            op.Find("zhao").GetComponent<Button>().onClick.AddListener(() =>
            {
                ZhaoMain.preScene = "team";
                ZhaoMain.person = p;
                SceneManager.LoadScene("Zhao");
            });
            op.Find("quit").GetComponent<Button>().onClick.AddListener(() =>
            {
                GameRunningData.GetRunningData().teammates.Remove(p);
                SetPerson();
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
