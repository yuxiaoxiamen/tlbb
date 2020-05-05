using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManualMouseControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image img;
    
    private void Start()
    {
        img = gameObject.GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        img.sprite = Resources.Load<Sprite>("ui/manualSelected");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.sprite = Resources.Load<Sprite>("ui/manualDefault");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ManualMain.isFood)
        {
            CutUpMain.manual = GlobalData.FoodManuals[int.Parse(name)];
            if (GameRunningData.GetRunningData().money >= CutUpMain.manual.Price)
            {
                GameRunningData.GetRunningData().money -= CutUpMain.manual.Price;
                TimeGoSubject.GetTimeSubject().UpdateTime(1);
                SceneManager.LoadScene("CutUp");
            }
            else
            {
                TipControl.instance.SetTip("金钱不足");
            }
            
        }
        else
        {
            MineralControl.manual = GlobalData.WeaponManuals[int.Parse(name)];
            if (GameRunningData.GetRunningData().money >= MineralControl.manual.Price)
            {
                GameRunningData.GetRunningData().money -= MineralControl.manual.Price;
                TimeGoSubject.GetTimeSubject().UpdateTime(1);
                SceneManager.LoadScene("Mining");
            }
            else
            {
                TipControl.instance.SetTip("金钱不足");
            }
        }
    }
}
