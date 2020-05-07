using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    public Transform initTransform;
    private Vector3 offset;
    public float speed = 0.1f;
    public static CameraFollow cameraFollowInstance;
    private Quaternion defaultQuaternion;

    void Awake()
    {
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
        if(person != null)
        {
            target = person.PersonObject.transform;
            transform.rotation = defaultQuaternion;
            transform.DOMove(target.position - offset, speed);
            //transform.position = target.position - offset;
        }
    }
}
