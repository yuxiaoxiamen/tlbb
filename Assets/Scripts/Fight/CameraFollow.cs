using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    public Transform initTransform;
    private Vector3 offset;
    public static CameraFollow cameraFollowInstance;
    private Quaternion defaultQuaternion;
    public bool isMove;

    void Awake()
    {
        offset = initTransform.position - transform.position;
        cameraFollowInstance = this;
    }

    void Update()
    {
        if (target != null && !isMove)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                transform.RotateAround(target.transform.position, target.transform.up, 60 * Time.deltaTime);
                offset = target.position - transform.position;
            }
            if (Input.GetKey(KeyCode.E))
            {
                transform.RotateAround(target.transform.position, target.transform.up, -60 * Time.deltaTime);
                offset = target.position - transform.position;
            }
        }
    }

    public void SetCameraFollowTarget(Person person)
    {
        if(person != null)
        {
            target = person.PersonObject.transform;
            transform.DOMove(target.position - offset, FightMain.instance.speed);
        }
    }
}
