using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SaveMouseControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image img;
    public static bool isSave;

    private void Start()
    {
        img = gameObject.GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        img.sprite = Resources.Load<Sprite>("ui/saveSelected");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.sprite = Resources.Load<Sprite>("ui/saveDefault");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int index = int.Parse(gameObject.name);
        if (isSave)
        {
            SaveAndReadMain.instance.WriteSaveFile(index);
        }
        else
        {
            SaveData saveData = SaveAndReadMain.instance.saveDatas[index];
            saveData.ResumeData();
        }
    }
}
