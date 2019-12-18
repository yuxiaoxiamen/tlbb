using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModuleButtonClick : MonoBehaviour
{
    public Button personButton;
    public Button itemButton;
    public Button kongFuButton;
    public Button queueButton;
    public Button systemButton;
    public Button fightTestButton;
    // Start is called before the first frame update
    void Start()
    {
        personButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("BasicAttributes");
        });
        itemButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Items");
        });
        kongFuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("KongFu");
        });
        queueButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Team");
        });
        systemButton.onClick.AddListener(() =>
        {
            
        });
        fightTestButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Fight");
        });
    }
}
