using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyControl : MonoBehaviour
{
    public bool isStay = false;
    private bool isStop = false;
    private Tween currentDOtween;

    public void Stop()
    {
        isStop = true;
    }

    private void Update()
    {
        if (isStop)
        {
            var g = Instantiate(gameObject);
            g.transform.SetParent(transform.parent);
            g.transform.localPosition = transform.localPosition;
            g.transform.localScale = transform.localScale;
            Destroy(gameObject);
        }
    }

    public void Move(bool isLeft)
    {
        if (isStop)
        {
            currentDOtween.Kill();
            return;
        }
        float time = Random.Range(0.4f, 0.6f);
        if (isLeft)
        {
            currentDOtween = transform.DOLocalMoveX(-0.5f, time).OnComplete(()=>
            {
                Move(false);
            }).SetEase(Ease.Linear);
        }
        else{
            currentDOtween = transform.DOLocalMoveX(0.5f, time).OnComplete(() =>
            {
                Move(true);
            }).SetEase(Ease.Linear);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isStay = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isStay = false;
    }
}
