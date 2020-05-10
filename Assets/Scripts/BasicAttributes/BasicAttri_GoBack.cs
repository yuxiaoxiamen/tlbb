using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicAttri_GoBack : MonoBehaviour
{
    public static string preScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (preScene == "map")
        {
            GameRunningData.GetRunningData().ReturnToMap();
        }
        else
        {
            SceneManager.LoadScene("Group");
        }
    }

}
