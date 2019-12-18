using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GongClick : MonoBehaviour
{
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            float num = float.Parse(gameObject.name);
            int num1 = (int)num;
            string nm = GongList.gong[num1].Name;

            Debug.Log("切换成功");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
