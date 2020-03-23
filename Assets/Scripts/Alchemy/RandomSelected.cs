using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSelected : MonoBehaviour
{
    private GameObject selectedObject;

    // Start is called before the first frame update
    void Awake()
    {
        selectedObject = transform.Find("selected").gameObject;
        SetItem();
    }

    void SetItem()
    {
        float randomScale = Random.Range(0.1f, 0.3f);
        selectedObject.transform.localScale = new Vector3(randomScale, 1, 1);
        float maxX = 0.5f - randomScale / 2;
        float minX = -maxX;
        float randomX = Random.Range(minX, maxX);
        selectedObject.transform.localPosition = new Vector3(randomX, 0, 0);
    }
}
