﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineralControl : MonoBehaviour
{
    public static WeaponManual Manual { get; set; }
    public GameObject mineralPrefab;
    public float[] scales = new float[] { 1.2f, 1, 0.8f, 0.6f };
    public static float minX = -6;
    public static float maxX = 6;
    public static float minY = -3;
    public static float maxY = 3;
    public static bool isOver;
    public static bool isGameStart;
    public GameObject manualObject;
    public GameObject startPanel;
    public static int sum;

    private void Awake()
    {
        Manual = new WeaponManual()
        {
            CopperNumber = 1,
            IronNumber = 0,
            SilverNumber = 0,
            GoldNumber = 0
        };
        Manual.Item = GlobalData.Items[50];
        isOver = false;
        isGameStart = false;
        startPanel.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            SetManual();
            CountDown.isGameOver = false;
            sum = 0;
            for (int i = 1; i <= Manual.CopperNumber + 1; ++i)
            {
                CreateMineralObject(0);
                ++sum;
            }
            for (int i = 1; i <= Manual.IronNumber + 1; ++i)
            {
                CreateMineralObject(1);
                ++sum;
            }
            for (int i = 1; i <= Manual.SilverNumber + 1; ++i)
            {
                ++sum;
                CreateMineralObject(2);
            }
            for (int i = 1; i <= Manual.GoldNumber + 1; ++i)
            {
                CreateMineralObject(3);
                ++sum;
            }
            isGameStart = true;
            startPanel.SetActive(false);
        });
    }

    void SetManual()
    {
        for(int i = 0; i < 4; ++i)
        {
            Transform titleTransform = manualObject.transform.GetChild(i);
            TextMesh textMesh = titleTransform.GetChild(0).GetComponent<TextMesh>();
            switch (i)
            {
                case 0:
                    textMesh.text = Manual.CopperNumber + "";
                    break;
                case 1:
                    textMesh.text = Manual.IronNumber + "";
                    break;
                case 2:
                    textMesh.text = Manual.SilverNumber + "";
                    break;
                case 3:
                    textMesh.text = Manual.GoldNumber + "";
                    break;
            }
        }
    }

    void CreateMineralObject(int type)
    {
        GameObject mineralObject = Instantiate(mineralPrefab);
        mineralObject.transform.localScale *= scales[type];
        SpriteRenderer spriteRenderer = mineralObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("mineral/"+type);
        mineralObject.transform.rotation = RandomRotate();
        mineralObject.name = type + "";
        mineralObject.transform.position = RandomPosition();
    }

    public static Vector3 RandomPosition()
    {
        float xVaule = Random.Range(minX, maxX);
        float yVaule = Random.Range(minY, maxY);
        Vector3 tempPoint = new Vector3(xVaule, yVaule, 0);
        return tempPoint;
    }

    public Quaternion RandomRotate()
    {
        float angle = Random.Range(0, 360);
        Quaternion tempQuat = Quaternion.AngleAxis(angle, Vector3.forward);
        return tempQuat;
    }
}
