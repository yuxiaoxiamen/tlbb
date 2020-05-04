using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectInit : MonoBehaviour
{
    public GameObject herbObject;
    public static int operate_count = 0;
    public static int operate_id;
    public static bool[] herbStatus = new bool[15];
    public static bool[] changeSign = new bool[30];
    public static int[] order = new int[30];

    public GameObject countDown;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        System.Random ro = new System.Random();
        int iResult;
        int iUp = 30;
        int iDown = 0;
        int[] order_ = new int[30];
        order[0] = ro.Next(iDown, iUp);
        for (int i = 1; i < 30; ++i)
        {
            iResult = ro.Next(iDown, iUp);
            while (haveExisted(iResult, i))
            {
                iResult = ro.Next(iDown, iUp);
            }
            order[i] = iResult;
            Debug.Log(order[i]);
        }

        for(int i = 0; i < 30; ++i)
        {
            var newHerb = Instantiate(herbObject).transform;
            newHerb.GetComponent<CoverAct>().id = i;
            newHerb.position = new Vector3(-5 + order[i] % 6 * 2, 5 - (int)(order[i] / 6) * 2, 0);
            newHerb.Find("Text").GetComponent<TextMesh>().text = i.ToString();
        }


        int timeLimit = 15;
        countDown.transform.GetComponent<TextMesh>().text = timeLimit.ToString();
        for (int i = 1; i < timeLimit+1; ++i)
        {
            yield return new WaitForSeconds(1);
            countDown.transform.GetComponent<TextMesh>().text = (timeLimit-i).ToString();
        }
        countDown.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool haveExisted(int result,int count)
    {
        for(int j = 0; j < count; ++j)
        {
            if (result == order[j])
            {
                return true;
            }
        }
        return false;
    }

}
