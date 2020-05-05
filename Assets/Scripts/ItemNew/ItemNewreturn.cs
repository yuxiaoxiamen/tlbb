using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ItemNewreturn : MonoBehaviour
{
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            ItemMain.ClearItems();
            GameRunningData.GetRunningData().ReturnToMap();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
