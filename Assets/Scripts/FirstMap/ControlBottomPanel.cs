using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlBottomPanel : MonoBehaviour
{
    public RectTransform showPosition;
    public RectTransform hidePosition;
    public static bool isMouseInPane;
    public static bool IsBanPane = false;
    public GameObject hearsayList;
    public GameObject listContent;
    public Transform systemPanel;
    // Start is called before the first frame update
    void Start()
    {
        hearsayList.SetActive(false);
        transform.Find("hearsay").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (hearsayList.activeSelf)
            {
                hearsayList.SetActive(false);
            }
            else
            {
                hearsayList.SetActive(true);
                listContent.GetComponent<HearsayMain>().SetSays();
            } 
        });
        transform.Find("person").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameRunningData.GetRunningData().SavePlayerMapRc();
            BasicAttri_GoBack.preScene = "map";
            MainAttributes.personId = GameRunningData.GetRunningData().player.BaseData.Id;
            SceneManager.LoadScene("BasicAttributes");
        });
        transform.Find("item").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameRunningData.GetRunningData().SavePlayerMapRc();
            SceneManager.LoadScene("ItemNew");
        });
        transform.Find("gong").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameRunningData.GetRunningData().SavePlayerMapRc();
            KongMain.preScene = "map";
            KongMain.person = GameRunningData.GetRunningData().player;
            SceneManager.LoadScene("Kong");
        });
        transform.Find("zhao").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameRunningData.GetRunningData().SavePlayerMapRc();
            ZhaoMain.preScene = "map";
            ZhaoMain.person = GameRunningData.GetRunningData().player;
            SceneManager.LoadScene("Zhao");
        });
        transform.Find("queue").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameRunningData.GetRunningData().SavePlayerMapRc();
            SceneManager.LoadScene("Group");
        });
        transform.Find("system").GetComponent<Button>().onClick.AddListener(() =>
        {
            if(systemPanel.localScale.y == 0)
            {
                systemPanel.DOScaleY(1, 0.2f);
                IsBanPane = true;
            }
            else
            {
                systemPanel.DOScaleY(0, 0.2f);
                IsBanPane = false;
            }
        });
        systemPanel.Find("save").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameRunningData.GetRunningData().SavePlayerMapRc();
            SaveAndReadMain.isStartPre = false;
            SaveMouseControl.isSave = true;
            SceneManager.LoadScene("SaveAndRead");
        });
        systemPanel.Find("read").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameRunningData.GetRunningData().SavePlayerMapRc();
            SaveAndReadMain.isStartPre = false;
            SaveMouseControl.isSave = false;
            SceneManager.LoadScene("SaveAndRead");
        });
        systemPanel.Find("exit").GetComponent<Button>().onClick.AddListener(() =>
        {
            GlobalData.Init();
            GameRunningData.GetRunningData().Init();
            SceneManager.LoadScene("Start");
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsBanPane)
        {
            if (Input.mousePosition.y < Screen.height * 0.1)
            {
                GetComponent<RectTransform>().DOMove(showPosition.position, 1);
                isMouseInPane = true;
            }
            else
            {
                GetComponent<RectTransform>().DOMove(hidePosition.position, 1);
                isMouseInPane = false;
            }
        }
    }
}
