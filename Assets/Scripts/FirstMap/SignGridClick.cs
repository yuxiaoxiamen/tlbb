using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignGridClick : MonoBehaviour
{
    public static HashSet<string> grids = new HashSet<string>();
    private SpriteRenderer sp;
    private static bool isWrite = false;
    // Start is called before the first frame update
    void Start()
    {
        sp = transform.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isWrite)
            {
                string text = "";
                foreach (string s in grids)
                {
                    text += s + ",";
                }
                Debug.Log(text);
                isWrite = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isWrite = false;
        }
    }

    private void OnMouseDown()
    {
        if (grids.Contains(name))
        {
            grids.Remove(name);
            sp.color = new Color(1, 1, 1, 0.4f);
        }
        else
        {
            grids.Add(name);
            sp.color = new Color(1, 0, 0,0.4f);
        }
    }
}
