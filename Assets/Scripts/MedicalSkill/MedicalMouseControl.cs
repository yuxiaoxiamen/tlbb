using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MedicalMouseControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject frontObject;
    public GameObject backObject;
    public float speed = 0.3f;
    public static GameObject preCard;
    public static GameObject currentCard;
    private Vector3 defaultScale;
    public bool isOver;
    public static bool isJudgeOver;
    public static int Count;

    // Start is called before the first frame update
    void Start()
    {
        backObject.transform.eulerAngles = Vector3.zero;
        frontObject.transform.eulerAngles = new Vector3(0, 90, 0);
        defaultScale = transform.localScale;
        isJudgeOver = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnFront()
    {
        isOver = false;
        backObject.transform.DORotate(new Vector3(0, 90, 0), speed).OnComplete(() =>
        {
            frontObject.transform.DORotate(new Vector3(0, 0, 0), speed).OnComplete(() =>
            {
                isOver = true;
            });
        });
    }

    public void TurnBack()
    {
        isOver = false;
        frontObject.transform.DORotate(new Vector3(0, 90, 0), speed).OnComplete(() =>
        {
            backObject.transform.DORotate(new Vector3(0, 0, 0), speed).OnComplete(() =>
            {
                isOver = true;
            });
        });
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isJudgeOver)
        {
            if (preCard != null)
            {
                if (gameObject != preCard)
                {
                    TurnFront();
                    currentCard = gameObject;
                    StartCoroutine(JudgeCard());
                }
            }
            else
            {
                TurnFront();
                preCard = gameObject;
            }
        } 
    }

    IEnumerator JudgeCard()
    {
        isJudgeOver = false;
        yield return new WaitUntil(() => {
            return currentCard.GetComponent<MedicalMouseControl>().isOver &&
            preCard.GetComponent<MedicalMouseControl>().isOver;
         });
        yield return new WaitForSeconds(0.2f);
        int type1 = int.Parse(preCard.name);
        int type2 = int.Parse(currentCard.name);
        if(type1 == type2)
        {
            preCard.SetActive(false);
            currentCard.SetActive(false);
            ++Count;
            if (Count == 15)
            {
                MedicalCountDown.isGameOver = true;
            }
            currentCard = null;
            preCard = null;
            isJudgeOver = true;

        }
        else
        {
            preCard.GetComponent<MedicalMouseControl>().TurnBack();
            currentCard.GetComponent<MedicalMouseControl>().TurnBack();
            yield return new WaitUntil(() => {
                return currentCard.GetComponent<MedicalMouseControl>().isOver &&
                preCard.GetComponent<MedicalMouseControl>().isOver;
            });
            currentCard = null;
            preCard = null;
            isJudgeOver = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(defaultScale * 1.1f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(defaultScale, 0.2f);
    }
}