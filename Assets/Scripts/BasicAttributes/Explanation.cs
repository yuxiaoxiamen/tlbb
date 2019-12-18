using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explanation : MonoBehaviour
{
    public GameObject ex; //弹出的释义面板
    public GameObject text; //释义文字内容

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseEnter()
    {
        ex.SetActive(true);
        var text_ = text.transform;
        //Debug.Log(gameObject.name.Split('_')[0]);
        text_.GetComponent<TextMesh>().text= Init.attris[gameObject.name.Split('_')[0]];
    }

    private void OnMouseExit()
    {
        ex.SetActive(false);
    }
}
