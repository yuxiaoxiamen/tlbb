using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlchemyMain : MonoBehaviour
{
    public GameObject itemObject;
    private List<BuoyControl> scripts;
    private int currentIndex = 0;
    public int count = 0;
    public GameObject startPanel;
    public static bool isGameStart;

    // Start is called before the first frame update
    void Start()
    {
        isGameStart = false;
        Button startButton = startPanel.transform.Find("Button").GetComponent<Button>();
        startButton.onClick.AddListener(() =>
        {
            scripts[currentIndex].Move(false);
            isGameStart = true;
            startPanel.SetActive(false);
        });
        scripts = new List<BuoyControl>()
        {
            itemObject.transform.Find("buoy").GetComponent<BuoyControl>()
        };
        for(int i = 1; i <= 4; ++i)
        {
            Vector3 position = new Vector3(itemObject.transform.position.x, itemObject.transform.position.y - i * 2f);
            GameObject itemClone = Instantiate(itemObject, position, Quaternion.identity);
            scripts.Add(itemClone.transform.Find("buoy").GetComponent<BuoyControl>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !AlchemyCountDown.isGameOver)
        {
            if (scripts[currentIndex].isStay)
            {
                ++count;
            }
            scripts[currentIndex].Stop();
            if (++currentIndex < scripts.Count)
            {
                scripts[currentIndex].Move(false);
            }
        }
        if(currentIndex == scripts.Count)
        {
            if(count == scripts.Count)
            {
                AlchemyCountDown.isSuccess = true;
            }
            AlchemyCountDown.isGameOver = true;
        }
    }
}
