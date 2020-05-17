using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ThridMapMain : MonoBehaviour
{
    public GameObject headPrefab;
    public Transform leftPosition;
    public Transform downPosition;
    public GameObject peoplePrefab;
    public static GameObject peopleObject;
    public static GameObject manualUI;
    public static GameObject storeUI;
    private static List<GameObject> headObjects = new List<GameObject>();
    public static ThridMapMain instance;
    public List<Person> persons;

    // Start is called before the first frame update
    void Start()
    {
        persons = new List<Person>();
        instance = this;
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
            GeneratePersonHead(FindPerson());
        }
        if(GameRunningData.GetRunningData().currentPlace is FirstPlace)
        {
            transform.GetComponent<SpriteRenderer>().sprite =
            Resources.Load<Sprite>("secondMap/" + GameRunningData.GetRunningData().currentPlace.Id);
        }
        else
        {
            transform.GetComponent<SpriteRenderer>().sprite =
                        Resources.Load<Sprite>("mapBg/" + GameRunningData.GetRunningData().currentPlace.Id);
        }
        manualUI = GameObject.Find("manualUI");
        manualUI.SetActive(false);
        storeUI = GameObject.Find("store");
        storeUI.SetActive(false);
        peopleObject = Instantiate(peoplePrefab);
        HidePeople();
    }

    void OnDestroy()
    {
        headObjects.Clear();
    }

    public static void ShowPeople(Person person)
    {
        peopleObject.SetActive(true);
        peopleObject.GetComponent<SpriteRenderer>().sprite = 
            Resources.Load<Sprite>("head/" + person.BaseData.HeadPortrait);
    }
    public static void HidePeople()
    {
        peopleObject.SetActive(false);
    }

    void GeneratePersonHead(List<Person> persons)
    {
        int count = persons.Count;
        float widthInterval = headPrefab.transform.position.x - leftPosition.position.x;
        float heightInterval = headPrefab.transform.position.y - downPosition.position.y;
        for (int i = 0; i < count / 2; ++i)
        {
            float x = headPrefab.transform.position.x - i * widthInterval;
            float y = headPrefab.transform.position.y;
            SetPersonObject(persons[2 * i], 
                Instantiate(headPrefab, new Vector3(x, y, 0), Quaternion.identity));
            y -= heightInterval;
            SetPersonObject(persons[2 * i + 1], 
                Instantiate(headPrefab, new Vector3(x, y, 0), Quaternion.identity));
        }
        if (count % 2 != 0)
        {
            float x = headPrefab.transform.position.x - count / 2 * widthInterval;
            float y = headPrefab.transform.position.y;
            SetPersonObject(persons[count - 1], 
                Instantiate(headPrefab, new Vector3(x, y, 0), Quaternion.identity));
        }
    }

    public static void HideAllHeads()
    {
        foreach(var headObject in headObjects)
        {
            headObject.SetActive(false);
        }
    }

    public static void ShowAllHeads()
    {
        foreach (var headObject in headObjects)
        {
            headObject.SetActive(true);
        }
    }

    List<Person> FindPerson()
    {

        if (!IsShaoLin())
        {
            int randomCount = Random.Range(1, 4);
            for (int i = 0; i < randomCount; ++i)
            {
                Person p = GlobalData.Persons[Random.Range(89, 95)];
                if (!persons.Contains(p))
                {
                    if (!GameRunningData.GetRunningData().teammates.Contains(p))
                    {
                        persons.Add(p);
                    }
                }
                else
                {
                    --i;
                }
            }
        }
        foreach (Person person in GlobalData.Persons)
        {
            if (!GameRunningData.GetRunningData().teammates.Contains(person)  
                && person != GameRunningData.GetRunningData().player)
            {
                if (person.CurrentPlaceString == GameRunningData.GetRunningData().currentPlace.GetPlaceString())
                {
                    persons.Add(person);
                }
            }
        }
        return persons;
    }

    bool IsShaoLin()
    {
        var place = GameRunningData.GetRunningData().currentPlace;
        if(place is SecondPlace)
        {
            var splace = (SecondPlace)place;
            if(splace.PrePlace.Id == 1)
            {
                return true;
            }
        }
        return false;
    }

    void SetPersonObject(Person person, GameObject headObject)
    {
        headObject.name = person.BaseData.Id+"";
        headObject.GetComponent<SpriteRenderer>().sprite = 
            Resources.Load<Sprite>("head/" + person.BaseData.HeadPortrait);
        TextMesh textMesh = headObject.transform.Find("name").GetComponent<TextMesh>();
        textMesh.text = person.BaseData.Name;
        headObjects.Add(headObject);
    }

    public void ReAddHead()
    {
        foreach(GameObject headObject in headObjects)
        {
            Destroy(headObject);
        }
        headObjects.Clear();
        GeneratePersonHead(persons);
    }
}
