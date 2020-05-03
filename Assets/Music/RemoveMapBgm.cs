using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveMapBgm : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject mapBgm = GameObject.FindGameObjectWithTag("MapBgm");
        if (mapBgm != null)
        {
            Destroy(mapBgm);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
