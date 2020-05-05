using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeKnife : MonoBehaviour
{
    public static GameObject knife;
    public GameObject knifeLightPrefab;
    public GameObject damagedFoodPrefab1;
    public GameObject damagedFoodPrefab2;
    private readonly float leaveForce = 3f;
    private int cutCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(knife == null)
        {
            knife = GameObject.Find("knife");
        }
        StartCoroutine(Judge());
    }

    IEnumerator Judge()
    {
        var knifeScript = knife.GetComponent<DrawKnife>();
        var positionArray = knifeScript.positions.ToArray();
        foreach (var pos in positionArray)
        {
            Ray ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(pos));
            if (GetComponent<Collider>().Raycast(ray, out RaycastHit hit, 1000f))
            {
                Cutting();
                break;
            }
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Judge());
    }

    void Cutting()
    {
        CreateKnifeLight();
        
        char type = gameObject.name[0];
        ++cutCount;
        switch (type)
        {
            case 'f':
                SoundEffectControl.instance.PlaySoundEffect(0);
                CreateDamagedGreens();
                gameObject.transform.position = new Vector3(100, 10, 0);
                CutUpMain.SetAlreadyText(type);
                break;
            case 'v':
                SoundEffectControl.instance.PlaySoundEffect(1);
                if (cutCount == 2)
                {
                    CreateDamagedGreens();
                    gameObject.transform.position = new Vector3(100, 10, 0);
                    CutUpMain.SetAlreadyText(type);
                }
                break;
            case 'm':
                SoundEffectControl.instance.PlaySoundEffect(2);
                if (cutCount == 3)
                {
                    CreateDamagedGreens();
                    gameObject.transform.position = new Vector3(100, 10, 0);
                    CutUpMain.SetAlreadyText(type);
                }
                break;
        }
    }

    void CreateKnifeLight()
    {
        var knifeScript = knife.GetComponent<DrawKnife>();
        var positionArray = knifeScript.positions.ToArray();
        Vector3 startPosition = positionArray[0];
        Vector3 endPosition = positionArray[positionArray.Length - 1];
        Vector3 knifeVector = endPosition - startPosition;
        if (startPosition.y > endPosition.y)
        {
            knifeVector = startPosition - endPosition;
        }
        float angle = Vector3.Angle(knifeVector, Vector3.right);
        var knifeLightObject = Instantiate(knifeLightPrefab);
        knifeLightObject.transform.rotation = Quaternion.Euler(0, 0, angle);
        knifeLightObject.transform.position = transform.position;
    }

    void CreateDamagedGreens()
    {
        GameObject foodObject1 = Instantiate(damagedFoodPrefab1);
        foodObject1.transform.rotation = transform.rotation;
        foodObject1.transform.position = transform.position;
        foodObject1.GetComponent<Rigidbody2D>().
            AddForce(foodObject1.transform.right.normalized * -1 * leaveForce, ForceMode2D.Impulse);
        foodObject1.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("cutUp/"+gameObject.name+"d1");
        GameObject foodObject2 = Instantiate(damagedFoodPrefab2);
        foodObject2.transform.rotation = transform.rotation;
        foodObject2.transform.position = transform.position;
        foodObject2.GetComponent<Rigidbody2D>().AddForce(foodObject2.transform.right.normalized * leaveForce, ForceMode2D.Impulse);
        foodObject2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("cutUp/" + gameObject.name + "d2");
    }
}
