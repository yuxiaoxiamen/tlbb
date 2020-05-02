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
    }

    public void SetItem()
    {
        int randomCount = Random.Range(1, 5);
        float width = selectedObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        float maxX = 5 - width * randomCount;
        float randomX = Random.Range(-5, maxX);
        selectedObject.transform.localPosition = new Vector3(randomX, 0, 0);
        for(int i = 1; i <= randomCount; ++i)
        {
            GameObject selectedOj = Instantiate(selectedObject);
            selectedOj.transform.SetParent(transform);
            selectedOj.transform.localPosition = new Vector3(randomX + width * i, 0, 0);
        }
    }
}
