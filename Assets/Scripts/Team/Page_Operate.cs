using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page_Operate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (gameObject.name.Equals("Page_Next"))
        {

            if (Member_Init.currentPage < Member_Init.pageCount)
            {
                Member_Init.currentPage++;
            }
            else
            {
                Debug.Log("当前已是最大页");
            }
        }
        else if (gameObject.name.Equals("Page_Prev"))
        {
            if (Member_Init.currentPage > 1)
            {
                Member_Init.currentPage--;
            }
            else
            {
                Debug.Log("当前已是第一页");
            }
        }

    }
}
