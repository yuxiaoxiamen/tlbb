using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveMapBgm : MonoBehaviour
{
    public bool isStart;
    // Start is called before the first frame update
    void Start()
    {
        if (isStart)
        {
            GameObject startBgm = GameObject.FindGameObjectWithTag("StartBgm");
            if (startBgm != null)
            {
                Destroy(startBgm);
            }
        }
        else
        {
            GameObject mapBgm = GameObject.FindGameObjectWithTag("MapBgm");
            if (mapBgm != null)
            {
                Destroy(mapBgm);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
