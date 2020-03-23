using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutUpMain : MonoBehaviour
{
    public float throwForce = 10f;
    public GameObject foodPrefab;
    public static float minY;
    public static FoodManual manual;
    public static bool isGameStart;
    public static GameObject scorePanel;
    public static int fruitNum;
    public static int vegetableNum;
    public static int meatNum;

    // Start is called before the first frame update
    void Start()
    {
        //manual = GlobalData.FoodManuals[0];
        minY = foodPrefab.transform.position.y - 3;
        isGameStart = false;
        fruitNum = 0;
        vegetableNum = 0;
        meatNum = 0;
        GameObject startPanel = GameObject.Find("startPanel");
        scorePanel = GameObject.Find("scorePanel");
        Button startButton = startPanel.GetComponentInChildren<Button>();
        startButton.onClick.AddListener(() =>
        {
            isGameStart = true;
            StartCoroutine(CreateFoods());
            startPanel.SetActive(false);
        });
        SetScorePanel();
    }

    private void Update()
    {
        if(fruitNum >= manual.FruitNum && vegetableNum >= manual.VegetableNum && meatNum >= manual.MeatNum)
        {
            CutUpCountDown.isGameOver = true;
            isGameStart = false;
            CutUpCountDown.isSuccess = true;
        }
    }

    void SetScorePanel()
    {
        Transform manualNameTans = scorePanel.transform.Find("manualName");
        manualNameTans.GetComponent<Text>().text += manual.Item.Name;
        Transform needTextTans = scorePanel.transform.Find("needText");
        needTextTans.Find("fruit").GetComponent<Text>().text += manual.FruitNum;
        needTextTans.Find("vegetable").GetComponent<Text>().text += manual.VegetableNum;
        needTextTans.Find("meat").GetComponent<Text>().text += manual.MeatNum;
    }

    public static void SetAlreadyText(char type)
    {
        Transform alreadyTextTans = scorePanel.transform.Find("alreadyText");
        switch (type)
        {
            case 'f':
                ++fruitNum;
                alreadyTextTans.Find("fruit").GetComponent<Text>().text = fruitNum+"";
                break;
            case 'v':
                ++vegetableNum;
                alreadyTextTans.Find("vegetable").GetComponent<Text>().text = vegetableNum+"";
                break;
            case 'm':
                ++meatNum;
                alreadyTextTans.Find("meat").GetComponent<Text>().text = meatNum+"";
                break;
        }
    }

    IEnumerator CreateFoods()
    {
        int sum = manual.FruitNum + manual.VegetableNum + manual.MeatNum + 3 * 10;
        for (int i = 0; i < sum; ++i)
        {
            if (CutUpCountDown.isGameOver)
            {
                break;
            }
            int randomType = Random.Range(1, 4);
            CreateFood(randomType);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void CreateFood(int type)
    {
        int randomX = Random.Range(-6, 7);
        int randomZ = Random.Range(2, 100);
        float randomAngle = Random.Range(0, 361);
        var randomRotation = Quaternion.AngleAxis(randomAngle, new Vector3(0, 0, 1));
        GameObject foodObject = Instantiate(foodPrefab, new Vector3(randomX, foodPrefab.transform.position.y, randomZ), randomRotation);
        randomAngle = Random.Range(-10, 11);
        Vector2 force = Quaternion.AngleAxis(randomAngle, new Vector3(0, 0, 1)) * Vector3.up * throwForce;
        foodObject.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        SpriteRenderer spr = foodObject.GetComponent<SpriteRenderer>();
        int randomType = Random.Range(1, 3);
        switch (type)
        {
            case 1:
                foodObject.name = "f"+randomType;
                spr.sprite = Resources.Load<Sprite>("cutUp/f"+randomType);
                break;
            case 2:
                foodObject.name = "v" + randomType;
                spr.sprite = Resources.Load<Sprite>("cutUp/v" + randomType);
                break;
            case 3:
                foodObject.name = "m" + randomType;
                spr.sprite = Resources.Load<Sprite>("cutUp/m" + randomType);
                break;
        }
    }
}
