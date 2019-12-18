﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    public Transform initTransform;
    private Vector3 offset;
    public float speed = 0.3f;
    public static CameraFollow cameraFollowInstance;
    private Quaternion defaultQuaternion;

    void Awake()
    {
        //设置相对偏移
        offset = initTransform.position - transform.position;
        cameraFollowInstance = this;
        defaultQuaternion = transform.rotation;
    }

    void Update()
    {
        if(target != null)
        {
            
            if (Input.GetKey(KeyCode.Q))
            {
                transform.RotateAround(target.transform.position, target.transform.up, 60 * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.E))
            {
                transform.RotateAround(target.transform.position, target.transform.up, -60 * Time.deltaTime);
            }
        }
    }

    public void SetCameraFollowTarget(Person person)
    {
        target = person.PersonObject.transform;
        transform.rotation = defaultQuaternion;
        transform.DOMove(target.position - offset, speed);
    }
}
