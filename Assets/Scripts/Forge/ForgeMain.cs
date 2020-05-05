using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ForgeMain : MonoBehaviour
{
    public GameObject mineralPrefab;
    public float minX;
    public float maxX;
    public float maxY;
    public float minY;
    public float[] gravitys = new float[] { 0.9f, 1.3f, 1.7f, 2.1f, 2.5f };
    public static int sum;
    public static GameObject lastOne;
    public static bool isGameOver;
    public static bool isGameStart;
    public GameObject startPanel;
    public static GameObject failPanel;
    public static GameObject successPanel;

    // Start is called before the first frame update
    void Awake()
    {
        isGameOver = false;
        isGameStart = false;
        sum = HookControl.copperNumber + HookControl.ironNumber + HookControl.silverNumber + HookControl.goldNumber;

        startPanel.transform.Find("Button").GetComponent<Button>().onClick.AddListener(()=>
        {
            StartCoroutine(GenerateMinerals());
            isGameStart = true;
            startPanel.SetActive(false);
        });

        failPanel = GameObject.Find("failPanel");
        Button failButton = failPanel.transform.Find("Button").GetComponent<Button>();
        failButton.onClick.AddListener(() =>
        {
            GameRunningData.GetRunningData().ReturnToMap();
        });
        failPanel.SetActive(false);

        successPanel = GameObject.Find("successPanel");
        Button successButton = successPanel.transform.Find("Button").GetComponent<Button>();
        successButton.onClick.AddListener(() =>
        {
            GameRunningData.GetRunningData().ReturnToMap();
        });
        successPanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(!isGameOver && lastOne != null && lastOne.transform.position.y < minY)
        {
            GameOver();
        }
    }

    IEnumerator GenerateMinerals()
    {
        int currentCopper = 0;
        int currentIron = 0;
        int currentSilver = 0;
        int currentGold = 0;

        for (int i = 1; i <= sum; ++i)
        {
            if (isGameOver)
            {
                break;
            }
            int type = Random.Range(0, 4);
            bool canCreate = false;
            switch (type)
            {
                case 0:
                    if (currentCopper < HookControl.copperNumber)
                    {
                        ++currentCopper;
                        canCreate = true;
                    }
                    break;
                case 1:
                    if (currentIron < HookControl.ironNumber)
                    {
                        ++currentIron;
                        canCreate = true;
                    }
                    break;
                case 2:
                    if (currentSilver < HookControl.silverNumber)
                    {
                        ++currentSilver;
                        canCreate = true;
                    }
                    break;
                case 3:
                    if (currentGold < HookControl.goldNumber)
                    {
                        ++currentGold;
                        canCreate = true;
                    }
                    break;
            }
            if (canCreate)
            {
                GameObject mineralObject = CreateMineral(type);
                if(i == sum)
                {
                    lastOne = mineralObject;
                }
                yield return new WaitForSeconds(1f);
            }
            else
            {
                --i;
            }
        }
    }

    GameObject CreateMineral(int type)
    {
        GameObject mineralObject = Instantiate(mineralPrefab);
        SpriteRenderer spriteRenderer = mineralObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("mineral/" + type);
        mineralObject.name = type + "";
        mineralObject.transform.position = RandomPosition();
        mineralObject.GetComponent<Rigidbody2D>().gravityScale = gravitys[type];
        return mineralObject;
    }

    Vector3 RandomPosition()
    {
        float xVaule = Random.Range(minX, maxX);
        Vector3 tempPoint = new Vector3(xVaule, maxY, 0);
        return tempPoint;
    }

    public static void GameOver()
    {
        isGameOver = true;
        if (TunkControl.copperNumber >= MineralControl.manual.CopperNumber &&
            TunkControl.ironNumber >= MineralControl.manual.IronNumber &&
            TunkControl.silverNumber >= MineralControl.manual.SilverNumber &&
            TunkControl.goldNumber >= MineralControl.manual.GoldNumber)
        {
            successPanel.SetActive(true);
            GameRunningData.GetRunningData().AddItem(MineralControl.manual.Item);
            successPanel.transform.Find("tipText").GetComponent<Text>().text += 
                System.Environment.NewLine + "获得武器"+MineralControl.manual.Item.Name;
            GameRunningData.GetRunningData().AddItem(MineralControl.manual.Item);
        }
        else
        {
            failPanel.SetActive(true);
        }
    }
}
