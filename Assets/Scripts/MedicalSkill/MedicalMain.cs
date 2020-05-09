using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedicalMain : MonoBehaviour
{
    public GameObject cardPrefab;
    public RectTransform parent;
    public RectTransform startPosition;
    public RectTransform rightPosition;
    public RectTransform downPosition;
    public GameObject startPanel;
    public static bool isGameStart;
    // Start is called before the first frame update
    void Start()
    {
        isGameStart = false;
        Button startButton = startPanel.transform.Find("Button").GetComponent<Button>();
        startButton.onClick.AddListener(() =>
        {
            List<int> typeList = new List<int>();
            for (int i = 0; i < 15; ++i)
            {
                typeList.Add(i);
                typeList.Add(i);
            }
            float widthInterval = rightPosition.position.x - startPosition.position.x;
            float heightInterval = startPosition.position.y - downPosition.position.y;
            for (int i = 0; i < 5; ++i)
            {
                for (int j = 0; j < 6; ++j)
                {
                    GameObject cardObject = Instantiate(cardPrefab);
                    RectTransform cardTransform = cardObject.GetComponent<RectTransform>();
                    cardTransform.position = startPosition.position + new Vector3(j * widthInterval, -i * heightInterval, 0);
                    cardTransform.SetParent(parent);
                    cardTransform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                    int x = Random.Range(0, typeList.Count);
                    cardObject.name = typeList[x] + "";
                    cardTransform.Find("front").GetComponent<Image>().sprite = Resources.Load<Sprite>("medical/" + typeList[x]);
                    typeList.RemoveAt(x);
                }
            }
            isGameStart = true;
            startPanel.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
