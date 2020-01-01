using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickNPC : MonoBehaviour
{
    private Vector3 defaultScale;

    // Start is called before the first frame update
    void Start()
    {
        defaultScale = transform.localScale;
    }

    private void OnMouseDown()
    {
        gameObject.transform.DOScale(defaultScale, 0.3f);
        Person person = GlobalData.Persons[int.Parse(gameObject.name)];
        InteractControl.instance.ShowAndFillPanel(person);
        ThridMapMain.HideAllHeads();
        ThridMapMain.ShowPeople(person);
    }

    private void OnMouseEnter()
    {
        gameObject.transform.DOScale(defaultScale * 1.2f, 0.3f);
    }

    private void OnMouseExit()
    {
        gameObject.transform.DOScale(defaultScale, 0.3f);
    }
}
