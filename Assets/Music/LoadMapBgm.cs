using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMapBgm : MonoBehaviour
{
    public GameObject bgm;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("MapBgm") == null)
        {
            Instantiate(bgm);
        }
    }
}