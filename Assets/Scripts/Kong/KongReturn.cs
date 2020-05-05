using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KongReturn : MonoBehaviour
{
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            KongMain.order.Clear();
            KongMain.inners.Clear();
            KongMain.center = 0;
            if(KongMain.preScene == "map")
            {
                GameRunningData.GetRunningData().ReturnToMap();
            }
            else
            {
                SceneManager.LoadScene("Team");
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
