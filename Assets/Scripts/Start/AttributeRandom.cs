using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AttributeRandom : MonoBehaviour
{
    public TextMesh BiTextMesh;
    public TextMesh GenTextMesh;
    public TextMesh WuTextMesh;
    public TextMesh ShenTextMesh;
    public TextMesh JinTextMesh;
    public TextMesh HpTextMesh;
    public TextMesh MpTextMesh;
    private readonly int attributeSum = 50 * 5;
    private readonly int HPMPSum = 1500 * 2;

    public Button button;
    public InputField inputField;

    int bi = 0;
    int gen = 0;
    int wu = 0;
    int shen = 0;
    int jin = 0;
    int hp = 0;
    int mp = 0;

    // Start is called before the first frame update
    void Start()
    {
        RandomAttribute();
        RandomHPMP();
        button.onClick.AddListener(() =>
        {
            if(inputField.text != "")
            {
                var playerBaseData = GameRunningData.GetRunningData().player.BaseData;
                playerBaseData.Name = inputField.text;
                playerBaseData.Bi = bi;
                playerBaseData.Gen = gen;
                playerBaseData.Wu = wu;
                playerBaseData.Shen = shen;
                playerBaseData.Jin = jin;
                playerBaseData.HP = hp;
                playerBaseData.MP = mp;

                SceneManager.LoadScene("Begin");
            }
            else
            {
                TipControl.instance.SetTip("名字还没填写");
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RandomAttribute();
            RandomHPMP();
        }
    }

    void RandomHPMP()
    {
        hp = Random.Range(1000, 2000);
        mp = HPMPSum - hp;
        HpTextMesh.text = hp + "";
        MpTextMesh.text = mp + "";
    }

    void RandomAttribute()
    {
        do
        {
            bi = Random.Range(1, 101);
            gen = Random.Range(1, 101);
            wu = Random.Range(1, 101);
            shen = Random.Range(1, 101);
            jin = attributeSum - bi - gen - wu - shen;
        } while (jin > 100 || jin <= 0);
        BiTextMesh.text = bi + "";
        GenTextMesh.text = gen + "";
        WuTextMesh.text = wu + "";
        ShenTextMesh.text = shen + "";
        JinTextMesh.text = jin + "";
    }
}
