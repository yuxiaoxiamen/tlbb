using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookOutTrigger : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "hook")
        {
            HookControl.isBack = true;
        }
    }
}
