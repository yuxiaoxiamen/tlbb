using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverAct : MonoBehaviour
{
    public GameObject cover;
    public int id;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        int timeLimit = 15;
        cover.SetActive(false);
        yield return new WaitForSeconds(timeLimit);
        cover.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ObjectInit.herbStatus[id / 2])
        {
            gameObject.SetActive(false);
            ObjectInit.operate_count = 0;
            //Debug.Log("  操作数量" + ObjectInit.operate_count);
        }

        if (ObjectInit.operate_id == id && ObjectInit.changeSign[id])
        {
            //Debug.Log("标志："+ObjectInit.changeSign + "  操作数量" + ObjectInit.operate_count);
            cover.SetActive(true);
            ObjectInit.operate_count = 0;
            ObjectInit.changeSign[id] = false;
            //Debug.Log("标志：" + ObjectInit.changeSign + "  操作数量" + ObjectInit.operate_count);
        }

    }

    private IEnumerator OnMouseDown()
    {
        if (ObjectInit.operate_count == 0)
        {
            cover.SetActive(false);
            ObjectInit.operate_count++;
            //Debug.Log("  操作数量" + ObjectInit.operate_count);
            ObjectInit.operate_id = id;
            //Debug.Log("操作Id：" + ObjectInit.operate_id + "  操作数量" + ObjectInit.operate_count);
            yield return new WaitForSeconds(3);
            if (ObjectInit.operate_count == 1)
            {
                ObjectInit.changeSign[id] = true;
            }

        }
        else if (ObjectInit.operate_count == 1&&id!=ObjectInit.operate_id)
        {
            cover.SetActive(false);
            ObjectInit.operate_count++;
            //Debug.Log("  操作数量" + ObjectInit.operate_count);
            yield return new WaitForSeconds(1);
            if (id / 2 == ObjectInit.operate_id / 2)
            {
                ObjectInit.herbStatus[ObjectInit.operate_id / 2] = true;
            }
            else
            {
                ObjectInit.changeSign[ObjectInit.operate_id] = true;
                //Debug.Log("操作Id：" + ObjectInit.operate_id + "  操作数量" + ObjectInit.operate_count);
                cover.SetActive(true);
            }
        }
    }
}
