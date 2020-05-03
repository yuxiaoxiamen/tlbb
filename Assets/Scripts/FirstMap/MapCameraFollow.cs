using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MapCameraFollow : MonoBehaviour
{
    Transform target;
    public GameObject map;

    // Update is called once per frame
    void LateUpdate()
    {
        if (target)
        {
            SetCamera();
        }
        else
        {
            SetTarget();
        }
    }

    // 设置相机
    void SetCamera()
    {
        Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
        Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
        Vector3 destination = transform.position + delta;
        float mapWidth = map.GetComponent<SpriteRenderer>().bounds.size.x;
        float mapHeight = map.GetComponent<SpriteRenderer>().bounds.size.y;
        float cameraWidth = GetComponent<Collider2D>().bounds.size.x;
        float cameraHeight = GetComponent<Collider2D>().bounds.size.y;
        if (destination.x  < -(mapWidth / 2 - cameraWidth / 2))
        {
            destination.x = -(mapWidth / 2 - cameraWidth / 2);
        }
        if(destination.x > mapWidth / 2 - cameraWidth / 2)
        {
            destination.x = mapWidth / 2 - cameraWidth / 2;
        }
        if (destination.y < -(mapHeight / 2 - cameraHeight / 2))
        {
            destination.y = -(mapHeight / 2 - cameraHeight / 2);
        }
        if (destination.y > mapHeight / 2 - cameraHeight / 2)
        {
            destination.y = mapHeight / 2 - cameraHeight / 2;
        }
        transform.DOMove(destination, 0.7f);
    }
    // 设置目标
    void SetTarget()
    {
        target = FirstMapMain.player.PersonObject.transform;
    }
}
