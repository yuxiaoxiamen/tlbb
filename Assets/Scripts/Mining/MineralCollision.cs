using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Mineral" && !MineralControl.isOver)
        {
            collision.transform.position = MineralControl.RandomPosition();
        }
    }
}
