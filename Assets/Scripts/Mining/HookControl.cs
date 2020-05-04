using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HookControl : MonoBehaviour
{
    public Transform startTransform;
    private float maxY;
    LineRenderer lineRenderer;
    private bool isLeft = true;
    public float angleSpeed;
    public float moveSpeed;
    public int leftMaxAngle;
    public int rightMinAngle;
    public static bool isMove = false;
    public static bool isBack = false;
    private int[] speedLevel = new int[] { 4, 3, 2, 1 };
    private float defaultMoveSpeed;
    public static int copperNumber;
    public static int ironNumber;
    public static int silverNumber;
    public static int goldNumber;
    public GameObject mineralObject;

    void Start()
    {
        maxY = transform.position.y;
        defaultMoveSpeed = moveSpeed;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.08f;
        copperNumber = 0;
        ironNumber = 0;
        silverNumber = 0;
        goldNumber = 0;
    }

    void Update()
    {
        if (MineralControl.isGameStart && !MiningCountDown.isGameOver)
        {
            UpdataLine();
            if (!isMove && !isBack)
            {
                HookRotate();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                isMove = true;
                MineralControl.isOver = true;
            }
            if (isMove && !isBack)
            {
                HookMoveForward();
            }
            if (isBack)
            {
                HookBackMove();
            }
            if(copperNumber + ironNumber + silverNumber + goldNumber == MineralControl.sum)
            {
                MiningCountDown.GameOver();
            }
        }
    }

    public void UpdataLine()
    {
        lineRenderer.SetPosition(0, startTransform.position);
        lineRenderer.SetPosition(1, transform.position);
    }

    public void HookRotate()
    {
        float rightAngle = Vector3.Angle(transform.up * -1, Vector3.right);
        if (isLeft)
        {
            if (rightAngle < leftMaxAngle)
            {   
                transform.RotateAround(startTransform.position, Vector3.forward, -angleSpeed * Time.deltaTime);
            }
            else
            {
                isLeft = false;
            }
        }
        else
        {
            if (rightAngle > rightMinAngle)
            {
                transform.RotateAround(startTransform.position, Vector3.forward, angleSpeed * Time.deltaTime);
            }
            else
            {
                isLeft = true;
            }
        }
    }

    public void HookMoveForward()
    {
        transform.position += transform.up * -1 * moveSpeed * Time.deltaTime;
    }

    public void HookBackMove()
    {
        if (transform.position.y < maxY)
        {
            transform.position += transform.up * moveSpeed * Time.deltaTime;
        }
        else
        {
            isBack = false;
            isMove = false;
            moveSpeed = defaultMoveSpeed;
            for(int i = 0; i < transform.childCount; ++i)
            {
                MineWasDug(transform.GetChild(i).gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Mineral")
        {
            Transform mineralTransform = collision.transform;
            float tempDistance = Vector3.Distance(transform.position, mineralTransform.position);
            mineralTransform.position = transform.position + transform.up * -1 * tempDistance;
            mineralTransform.SetParent(transform);
            ComputeSpeed(speedLevel[int.Parse(collision.name)]);
            isBack = true;
        }
    }

    public void ComputeSpeed(int scaleLevel)
    {
        moveSpeed = moveSpeed - moveSpeed * 0.2f * scaleLevel;
    }

    private void MineWasDug(GameObject gameObject)
    {
        int type = int.Parse(gameObject.name);
        Transform titleTransform = mineralObject.transform.GetChild(type);
        Text textMesh = titleTransform.GetComponent<Text>();
        switch (type)
        {
            case 0:
                ++copperNumber;
                textMesh.text = copperNumber+"";
                break;
            case 1:
                ++ironNumber;
                textMesh.text = ironNumber + "";
                break;
            case 2:
                ++silverNumber;
                textMesh.text = silverNumber + "";
                break;
            case 3:
                ++goldNumber;
                textMesh.text = goldNumber + "";
                break;
        }
        Destroy(gameObject);
    }
}
