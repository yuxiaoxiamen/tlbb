using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ZhaoReturn : MonoBehaviour
{
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            ZhaoMain.fist.Clear();
            ZhaoMain.finger.Clear();
            ZhaoMain.palm.Clear();
            ZhaoMain.sword.Clear();
            ZhaoMain.knife.Clear();
            ZhaoMain.rod.Clear();
            if (ZhaoMain.preScene == "map")
            {
                GameRunningData.GetRunningData().ReturnToMap();
            }
            else
            {
                SceneManager.LoadScene("Group");
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
