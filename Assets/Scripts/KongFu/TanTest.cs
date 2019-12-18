using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanTest : MonoBehaviour
{
    public GameObject gg,temp;
    float x, y, z;

    // Start is called before the first frame update
    void Start()
    {
        x = 0.4968166f;
        y = 0.07170042f;
        z = -0.02539063f;

        temp = Instantiate(gg, new Vector3(x, y, z), Quaternion.identity);
        temp.SetActive(false);
        gg = GameObject.Find("GongList");
        gg.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            temp.SetActive(false);
            gg.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        temp.SetActive(true);
        gg.SetActive(true);
    }
}
