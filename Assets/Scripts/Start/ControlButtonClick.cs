using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlButtonClick : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("FirstMap");
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
