using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryKnifeLight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestoryLight());
    }

    IEnumerator DestoryLight()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
