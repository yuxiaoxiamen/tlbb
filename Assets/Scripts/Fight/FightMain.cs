using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightMain : MonoBehaviour
{
    public GameObject gridPrefab; //格子的预制体
    public GameObject personPrefab; //人物的预制体
    public float heightGrap = 0.85f;
    public static Dictionary<GameObject, Vector2Int> gridObjectToData; //通过格子对象获取格子的行列
    public static Dictionary<Vector2Int, GameObject> gridDataToObject; //通过格子的行列获取格子对象
    public static Dictionary<Vector2Int, Person> positionToPerson; //通过人物在战斗场景中的位置获取人物实例
    public static List<Person> friendQueue = new List<Person>();//友方战斗的人
    public static List<Person> enemyQueue = new List<Person>(); //敌方战斗的人
    public static int mapWidth = 21; //地图的最大列数
    public static int mapHeight = 13; //地图的最大行数
    public static bool isEnemyRound = false;
    public static readonly float speed = 0.1f;
    public static int finished = 0;


    // Start is called before the first frame update
    void Start()
    {
        GlobalData.Init();
        CreateMap();
        Person player = GlobalData.Persons[0];
        player.RowCol = new Vector2Int(12, 3);
        Person friend1 = GlobalData.Persons[1];
        friend1.RowCol = new Vector2Int(12, 6);
        Person enemy1 = GlobalData.Persons[2];
        enemy1.RowCol = new Vector2Int(0, 6);
        Person enemy2 = GlobalData.Persons[3];
        enemy2.RowCol = new Vector2Int(0, 9);

        friendQueue.Add(player);
        friendQueue.Add(friend1);
        enemyQueue.Add(enemy1);
        enemyQueue.Add(enemy2);
        positionToPerson = new Dictionary<Vector2Int, Person>();
        SetFightPerson(friendQueue);
        SetFightPerson(enemyQueue);
        RotateEnemys();
        FightGUI.SetBattleControlPanel();
        FightGridClick.SetColor();
        FightPersonClick.SelectAPerson(GameRunningData.GetRunningData().player);
    }

    void RotateEnemys()
    {
        foreach(var enemy in enemyQueue)
        {
            RotatePerson(enemy, 180);
        }
    }

    public static void RotatePerson(Person person, float angle)
    {
        person.PersonObject.transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    void CreateMap()
    {
        float width = gridPrefab.GetComponent<Renderer>().bounds.size.x;
        float height = gridPrefab.GetComponent<Renderer>().bounds.size.z;
        gridObjectToData = new Dictionary<GameObject, Vector2Int>();
        gridDataToObject = new Dictionary<Vector2Int, GameObject>();
        for (int j = 0; j < mapHeight; ++j)
        {
            int realMapWidth = j % 2 == 0 ? mapWidth : mapWidth - 1;
            for (int i = 0; i < realMapWidth; ++i)
            {
                GameObject newGrid = Instantiate(gridPrefab);
                newGrid.name = j + "_" + i;
                newGrid.transform.parent = transform;
                float x = j % 2 == 0 ? i : i + 0.5f;
                newGrid.transform.position += new Vector3(x * width, 0, -j * height * heightGrap);
                var rowAndCol = new Vector2Int(j, i);
                gridObjectToData.Add(newGrid, rowAndCol);
                gridDataToObject.Add(rowAndCol, newGrid);
            }
        }
    }

    public static void PlayerFinished()
    {
        CountPlayerOver();
        FightPersonClick.currentPerson.ControlState = BattleControlState.End;
        FightGridClick.SwitchGridColor(gridDataToObject[FightPersonClick.currentPerson.RowCol], FightGridClick.defaultColor);
        FightGridClick.ClearPathAndRange();
        AttackTool.ClearAttackRange();
        AttackTool.ClearAttackDistance();
        FightGUI.HideBattlePane();

        SelectNextPerson();
    }

    public static void SetPersonHPSplider(Person person)
    {
        HPSplider hPSpliderScript = person.PersonObject.GetComponent<HPSplider>();
        RectTransform hpTransform = hPSpliderScript.HPObjectClone.transform.Find("HP").gameObject.GetComponent<RectTransform>();
        hpTransform.DOScale(new Vector3(person.CurrentHP * 1.0f / person.BaseData.HP,
            1, 1), 0.5f);
    }

    void SetFightPerson(List<Person> personQueue)
    {
        foreach(var person in personQueue)
        {
            Vector3 personPosition = gridDataToObject[person.RowCol].transform.position;
            GameObject personObject = Instantiate(personPrefab, personPosition, Quaternion.identity);
            personObject.name = person.BaseData.Id + "";
            person.PersonObject = personObject;
            positionToPerson.Add(person.RowCol, person);
            SetPersonHPSplider(person);
        }
        
    }

    public static HashSet<Vector2Int> GetGrids()
    {
        HashSet<Vector2Int> grids = new HashSet<Vector2Int>();
        foreach (var key in gridDataToObject.Keys)
        {
            grids.Add(key);
        }
        return grids;
    }

    // Update is called once per frame
    void Update()
    {
        if (finished == friendQueue.Count)
        {
            finished = 0;
            StartCoroutine(EnemyRound());
        }
    }

    void ResumePersonState()
    {
        foreach(var person in friendQueue)
        {
            person.ControlState = BattleControlState.Moving;
            person.IsMoved = false;
        }
        FightPersonClick.currentPerson = null;
    }

    public static void CountPlayerOver()
    {
        ++finished;
    }

    IEnumerator EnemyRound()
    {
        foreach (Person enemy in enemyQueue)
        {
            CameraFollow.cameraFollowInstance.SetCameraFollowTarget(enemy);
            FightAI.NPCAI(enemy, friendQueue);
            yield return new WaitForSeconds(0.5f);
        }
        ResumePersonState();
        SelectNextPerson();
    }

    public static void SelectNextPerson()
    {
        foreach (var person in friendQueue)
        {
            if (person.ControlState != BattleControlState.End)
            {
                FightPersonClick.SelectAPerson(person);
                break;
            }
        }

    }
}
