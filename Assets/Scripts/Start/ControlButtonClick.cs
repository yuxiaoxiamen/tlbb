using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlButtonClick : MonoBehaviour
{
    public Button newButton;
    public Button oldButton;
    public Button quitButton;
    // Start is called before the first frame update
    void Start()
    {
        newButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("RandomAttribute");
        });
        oldButton.onClick.AddListener(() =>
        {
            SaveMouseControl.isSave = false;
            SaveAndReadMain.isStartPre = true;
            SceneManager.LoadScene("SaveAndRead");
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
