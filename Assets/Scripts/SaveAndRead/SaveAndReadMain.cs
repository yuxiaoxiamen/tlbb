using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveAndReadMain : MonoBehaviour
{
    public GameObject scrollContent;
    public GameObject saveItemPrefab;
    public GameObject noTextPrefab;
    public GameObject yesTextPrefab;
    public Dictionary<int, SaveData> saveDatas;
    public static SaveAndReadMain instance;
    private string savePath;
    public Button returnButton;
    public static bool isStartPre;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        savePath = Application.persistentDataPath + "/";
        ReadAllSaveDatas();
        for(int i = 1; i < 100; ++i)
        {
            GameObject saveItemObject = Instantiate(saveItemPrefab);
            RectTransform saveItemTransform = saveItemObject.GetComponent<RectTransform>();
            saveItemTransform.SetParent(scrollContent.GetComponent<RectTransform>());
            saveItemTransform.localPosition = Vector3.zero;
            saveItemTransform.localRotation = Quaternion.identity;
            saveItemTransform.localScale = Vector3.one;
            saveItemObject.name = i + "";
            if (saveDatas.ContainsKey(i))
            {
                SetText(saveItemTransform, saveDatas[i]);
            }
            else
            {
                SetText(saveItemTransform, null);
            }
        }
        returnButton.onClick.AddListener(() =>
        {
            if (isStartPre)
            {
                SceneManager.LoadScene("Start");
            }
            else
            {
                GameRunningData.GetRunningData().ReturnToMap();
            }
        });
    }

    void ReadAllSaveDatas()
    {
        saveDatas = new Dictionary<int, SaveData>();
        var files = Directory.GetFiles(savePath, "*.bin");
        foreach (string file in files)
        {
            string numberString = file.Substring(file.Length - 6);
            if (numberString.Contains("/"))
            {
                numberString = numberString.Substring(1).Split('.')[0];
            }
            else
            {
                numberString = numberString.Split('.')[0];
            }
            int index = int.Parse(numberString);
            saveDatas.Add(index, ReadSaveFile(index));
        }
    }

    private SaveData ReadSaveFile(int index)
    {
        IFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(savePath + index + ".bin",
            FileMode.Open);
        SaveData save = (SaveData)formatter.Deserialize(stream);
        stream.Close();
        return save;
    }

    public void WriteSaveFile(int number)
    {
        IFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(savePath + number + ".bin",
            FileMode.Create, FileAccess.Write);
        SaveData save = new SaveData(number);
        saveDatas[number] = save;
        formatter.Serialize(stream, save);
        var saveItemTransform = scrollContent.transform.Find(save.Number + "").GetComponent<RectTransform>();
        SetText(saveItemTransform, save);
        stream.Close();
    }

    void SetText(RectTransform parent, SaveData saveData)
    {
        GameObject textObject = null;
        if (saveData != null)
        {
            textObject = Instantiate(yesTextPrefab);
            SetYesText(textObject, saveData);
        }
        else
        {
            textObject = Instantiate(noTextPrefab);
        }
        RectTransform textTransform = textObject.GetComponent<RectTransform>();
        if(parent.childCount != 0)
        {
            Destroy(parent.GetChild(0).gameObject);
        }
        textTransform.SetParent(parent);
        textTransform.localPosition = Vector3.zero;
        textTransform.localRotation = Quaternion.identity;
        textTransform.localScale = Vector3.one;
    }

    void SetYesText(GameObject yesTextObject, SaveData saveData)
    {
        Text numberText = yesTextObject.transform.Find("number").GetComponent<Text>();
        Text realTimeText = yesTextObject.transform.Find("realTime").GetComponent<Text>();
        Text nameText = yesTextObject.transform.Find("name").GetComponent<Text>();
        Text placeText = yesTextObject.transform.Find("place").GetComponent<Text>();
        Text gameTimeText = yesTextObject.transform.Find("gameTime").GetComponent<Text>();

        string nText = saveData.Number + "";
        if(saveData.Number / 10 == 0)
        {
            nText = "0" + saveData.Number;
        }
        numberText.text = nText;

        realTimeText.text = saveData.RealTime;

        nameText.text = saveData.Persons[0].BaseData.Name;

        placeText.text = GetCurrentPlaceString(saveData.GetPlace());

        gameTimeText.text = saveData.Date.GetDateText();
    }

    public static string GetCurrentPlaceString(Place currentPlace)
    {
        if (currentPlace != null)
        {
            if (currentPlace is SecondPlace)
            {
                SecondPlace place = (SecondPlace)currentPlace;
                return place.PrePlace.Name + " " + place.Name;
            }
            else
            {
                FirstPlace place = (FirstPlace)currentPlace;
                return place.Name;
            }
        }
        else
        {
            return "大地图";
        }
    }
}
