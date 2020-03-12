using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondMapMain : MonoBehaviour
{
    public GameObject sitePrefab;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        bool hasDialogue = ControlDialogue.instance.CheckMainConversation(() =>
        {
            var key = GameRunningData.GetRunningData().GetPlaceDateKey();
            if (GlobalData.MainLineConflicts[key].ConflictForm == ConflictKind.Battle)
            {
                FightMain.source = FightSource.MainLine;
                SceneManager.LoadScene("Fight");
            }
        });
        if (!hasDialogue)
        {
            SetSites();
        }
    }

    void SetSites()
    {
        var secondPlaces = ((FirstPlace)GameRunningData.GetRunningData().currentPlace).Sites;
        float width = sitePrefab.GetComponent<Renderer>().bounds.size.x;
        for (int i = 0; i < secondPlaces.Count; ++i)
        {
            SecondPlace secondPlace = secondPlaces[i];
            GameObject siteObject = Instantiate(sitePrefab);
            siteObject.name = secondPlace.Id + "";
            siteObject.transform.Find("siteName").GetComponent<TextMesh>().text = GetVerticalString(secondPlace.Name);
            siteObject.transform.position = sitePrefab.transform.position + new Vector3((-0.5f - width) * i, 0, 0);
        }
    }

    string GetVerticalString(string text)
    {
        string result = text[0]+"";
        for(int i = 1; i < text.Length; ++i)
        {
            result = result + System.Environment.NewLine + text[i];
        }
        return result;
    }
}
