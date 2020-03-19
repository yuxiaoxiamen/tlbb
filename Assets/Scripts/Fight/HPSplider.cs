using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSplider : MonoBehaviour
{
    public GameObject HPobject;

    private Transform HPParent;
    public GameObject HPObjectClone;
    private Vector3 EnemySceenPosition;

    void Awake()
    {
        //获取放HP血条的父物体
        HPParent = GameObject.Find("HPParent").transform;
        //把游戏物体的世界坐标转换为屏幕坐标
        EnemySceenPosition = Camera.main.WorldToScreenPoint(transform.position);
        //创建一个Clone血条图片
        HPObjectClone = Instantiate(HPobject, EnemySceenPosition, Quaternion.identity);
        //设置血条的父物体
        HPObjectClone.transform.SetParent(HPParent);
    }

    void Update()
    {
        //每帧都去执行使血条跟随物体
        PHFollowEnemy();
    }
    //血条放置到Canvas另一个Plane中 并跟随物体移动
    void PHFollowEnemy()
    {
        //把物体坐标转换为屏幕坐标，修改偏移量
        EnemySceenPosition = Camera.main.WorldToScreenPoint(transform.position);
        HPObjectClone.transform.position = EnemySceenPosition;
    }

    public void SetSliderColor()
    {
        HPObjectClone.transform.Find("HP").GetComponent<Image>().color = Color.green;
    }
}
