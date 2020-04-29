using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowOpen : MonoBehaviour
{
    public GameObject shadow;
    // Start is called before the first frame update
    void Start()
    {
        //获取shadow 材质
        var shadowMat = shadow.GetComponent<SpriteRenderer>().material;
        //获取人物纹理
        var heroTex = GetComponent<SpriteRenderer>().sprite.texture;
        //传递给shadow shader
        shadowMat.SetTexture("_HeroTex", heroTex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
