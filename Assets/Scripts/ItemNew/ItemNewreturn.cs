using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ItemNewreturn : MonoBehaviour
{
    private Button button;
    public static string preScene;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            ItemMain.ClearItems();
            if (KongMain.preScene == "map")
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
