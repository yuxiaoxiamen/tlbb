using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouserOverPerson : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.Find("op").DOScaleY(1, 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.Find("op").DOScaleY(0, 0.5f);
    }
}
