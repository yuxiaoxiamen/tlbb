using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KongChange : MonoBehaviour
{
    private Button button;


    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            GameRunningData.GetRunningData().player.SelectedInnerGong = KongMain.inner;
            GameObject root = GameObject.Find("introduction");
            GameObject innerstate = root.transform.Find("KongState").gameObject;
            innerstate.SetActive(true);
            //Debug.Log(player.SelectedInnerGong.FixData.Name);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
