using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutUpMain : MonoBehaviour
{
    public float throwForce = 10f;
    public GameObject foodPrefab;
    public static float minY;
    public static FoodManual manual;

    // Start is called before the first frame update
    void Start()
    {
        minY = foodPrefab.transform.position.y - 3;
        manual = new FoodManual()
        {
            FruitNum = 10,
            VegetableNum = 15,
            MeatNum = 20
        };
        StartCoroutine(CreateFoods());
    }

    IEnumerator CreateFoods()
    {
        int sum = manual.FruitNum + manual.VegetableNum + manual.MeatNum + 3 * 10;
        for (int i = 0; i < sum; ++i)
        {
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
