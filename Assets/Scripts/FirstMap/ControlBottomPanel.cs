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
            SceneManager.LoadScene("BasicAttributes");
        });
        transform.Find("item").GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Items");
        });
        transform.Find("kongFu").GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene("KongFu");
        });
        transform.Find("queue").GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Team");
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
