using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreMain : MonoBehaviour
{
    public static string storetype;                               //商店类型
    public static string state;                                  //当前买卖状态

    List<Good> itemtemp = new List<Good>();

    static StoreMain()
    {
        storetype = "Food";
        state = "Buy";
    }


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
