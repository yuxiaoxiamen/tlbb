using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBottomPanel : MonoBehaviour
{
    public RectTransform showPosition;
    public RectTransform hidePosition;
    public static bool isMouseInPane;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.mousePosition.y < Screen.height * 0.1)
        {
            GetComponent<RectTransform>().DOMove(showPosition.position, 1);
            isMouseInPane = true;
        }
        else
        {
            GetComponent<RectTransform>().DOMove(hidePosition.position, 1);
            isMouseInPane = false;
        }
    }
}
