using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThridMapMain : MonoBehaviour
{
    public GameObject headPrefab;
    public Transform leftPosition;
    public Transform downPosition;
    public GameObject peoplePrefab;
    public static GameObject peopleObject;
    private static List<GameObject> headObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        ControlDialogue.instance.CheckMainConversation(() =>
        {
            GeneratePersonHead(FindPerson());
        });
        peopleObject = Instantiate(peoplePrefab);
        HidePeople();
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
            SetPersonObject(persons[i], 
                Instantiate(headPrefab, new Vector3(x, y, 0), Quaternion.identity));
            y -= heightInterval;
            SetPersonObject(persons[i + 1], 
                Instantiate(headPrefab, new Vector3(x, y, 0), Quaternion.identity));
        }
        if (count % 2 != 0)
        {
            float x = headPrefab.transform.position.x - count / 2 * widthInterval;
            float y = headPrefab.transform.position.y;
            SetPersonObject(persons[count / 2 + 1], 
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
        List<Person> persons = new List<Person>
        {
            GlobalData.Persons[1],
            GlobalData.Persons[2],
            GlobalData.Persons[3]
        };
        //foreach(Person person in GlobalData.Persons)
        //{
        //    if(person.CurrentPlaceString == GameRunningData.GetRunningData().currentPlace.GetPlaceString())
        //    {
        //        persons.Add(person);
        //    }
        //}
        return persons;
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
}
