using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMapBgm : MonoBehaviour
{
    public GameObject bgm;
    public bool isStart;
    // Start is called before the first frame update
    void Start()
    {
        if (isStart)
        {
            if (GameObject.FindGameObjectWithTag("StartBgm") == null)
            {
                Instantiate(bgm);
            }
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("MapBgm") == null)
            {
                Instantiate(bgm);
            }
        }
    }
}