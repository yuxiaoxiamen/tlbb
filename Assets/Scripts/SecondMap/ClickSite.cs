using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickSite : MonoBehaviour
{
    Vector3 defaultScale;
    // Start is called before the first frame update
    void Start()
    {
        defaultScale = gameObject.transform.localScale;
    }

    private void OnMouseDown()
    {
        SecondPlace secondPlace = GlobalData.SecondPlaces[int.Parse(gameObject.name)];
        secondPlace.PrePlace = (FirstPlace)GameRunningData.GetRunningData().currentPlace;
        GameRunningData.GetRunningData().currentPlace = secondPlace;
        SceneManager.LoadScene("ThridMap");
    }

    private void OnMouseEnter()
    {
        if (!ControlBottomPanel.isMouseInPane)
        {
            gameObject.transform.DOScale(defaultScale * 1.1f, 0.5f);
        }
    }

    private void OnMouseExit()
    {
        gameObject.transform.DOScale(defaultScale, 0.5f);
    }
}
