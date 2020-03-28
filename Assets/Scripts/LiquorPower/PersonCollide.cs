using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonCollide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LiquorPowerMain.instance.isGameStart = false;
        LiquorPowerMain.instance.failPanel.SetActive(true);
        LiquorPowerMain.instance.sequence.Kill();
    }
}
