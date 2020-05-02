using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunkControl : MonoBehaviour
{
    private GameObject mineral;
    private int tunkCount = 0;
    public GameObject mineralObject;
    public GameObject tunkNumberObject;
    public static int copperNumber;
    public static int ironNumber;
    public static int silverNumber;
    public static int goldNumber;
    public GameObject hammerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        tunkNumberObject.GetComponent<TextMesh>().text = ForgeMain.sum + "";
        copperNumber = 0;
        ironNumber = 0;
        silverNumber = 0;
        goldNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ForgeMain.isGameStart)
        {
            if (tunkCount < ForgeMain.sum)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (mineral != null)
                    {
                        MineWasForge(mineral);
                    }
                    ++tunkCount;
                    tunkNumberObject.GetComponent<TextMesh>().text = ForgeMain.sum - tunkCount + "";
                }
            }
            else if (!ForgeMain.isGameOver)
            {
                ForgeMain.GameOver();
            }
        }
    }

    private void MineWasForge(GameObject gameObject)
    {
        int type = int.Parse(gameObject.name);
        Transform titleTransform = mineralObject.transform.GetChild(type);
        TextMesh textMesh = titleTransform.GetChild(0).GetComponent<TextMesh>();
        switch (type)
        {
            case 0:
                ++copperNumber;
                textMesh.text = copperNumber + "";
                break;
            case 1:
                ++ironNumber;
                textMesh.text = ironNumber + "";
                break;
            case 2:
                ++silverNumber;
                textMesh.text = silverNumber + "";
                break;
            case 3:
                ++goldNumber;
                textMesh.text = goldNumber + "";
                break;
        }
        Destroy(gameObject);
        StartCoroutine(CreateHammer(gameObject.transform.position));
    }

    public IEnumerator CreateHammer(Vector3 position)
    {
        GameObject hammerObject = Instantiate(hammerPrefab);
        hammerObject.transform.position = position;
        yield return new WaitForSeconds(0.4f);
        Destroy(hammerObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        mineral = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        mineral = null;
    }
}
