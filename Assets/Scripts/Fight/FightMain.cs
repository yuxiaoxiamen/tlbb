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
    public static List<Person> friendQueue;//友方战斗的人
    public static List<Person> enemyQueue;//敌方战斗的人
    public static int mapWidth = 21; //地图的最大列数
    public static int mapHeight = 13; //地图的最大行数
    public static bool isEnemyRound = false;
    public static readonly float speed = 0.1f;
    public static int finished = 0;
    public static bool isSuccess;
    public static bool isFail;
    private static bool isCreateReward;
    public static FightSource source;
    public static Person contestEnemy;

    // Start is called before the first frame update
    void Start()
    {
        CreateMap();
        isFail = false;
        isSuccess = false;
        isCreateReward = false;

        //GetFriendsAndEnemys();
        //SetPersonRowCol(friendQueue, false);
        //SetPersonRowCol(enemyQueue, true);

        TestData();

        positionToPerson = new Dictionary<Vector2Int, Person>();
        SetFightPerson(friendQueue);
        SetFightPerson(enemyQueue);
        RotateEnemys();
        FightPersonClick.SelectAPerson(GameRunningData.GetRunningData().player);
        foreach (Person person in friendQueue)
        {
            GongBuffTool.EffectValueBuff(person);
            GongBuffTool.HPBuffTrigger(person);
            GongBuffTool.SevenTen(person);
        }
        foreach (Person person in enemyQueue)
        {
            GongBuffTool.EffectValueBuff(person);
            GongBuffTool.HPBuffTrigger(person);
            GongBuffTool.SevenTen(person);
        }
        GongBuffTool.EightennTen(friendQueue, enemyQueue, false);
        GongBuffTool.EightennTen(enemyQueue, friendQueue, true);
        GongBuffTool.CreateAllHalo(friendQueue, enemyQueue);
        GongBuffTool.CreateAllHalo(enemyQueue, friendQueue);
    }

    void GetFriendsAndEnemys()
    {
        if(source == FightSource.MainLine)
        {
            var key = GameRunningData.GetRunningData().GetPlaceDateKey();
            var conflict = GlobalData.MainLineConflicts[key];
            if (conflict.IsZ)
            {
                friendQueue = conflict.ZFriends;
                enemyQueue = conflict.ZEnemys;
            }
            else
            {
                friendQueue = conflict.FFriends;
                enemyQueue = conflict.FEnemys;
            }
            var teammates = GameRunningData.GetRunningData().teammates;
            foreach (Person teammate in teammates)
            {
                if (enemyQueue.Contains(teammate))
                {
                    teammates.Remove(teammate);
                }
                else
                {
                    if (!friendQueue.Contains(teammate))
                    {
                        friendQueue.Add(teammate);
                    }
                }
            }
        }
        else if(source == FightSource.Contest)
        {
            friendQueue = new List<Person>();
            enemyQueue = new List<Person>
            {
                contestEnemy
            };
        }
        else if(source == FightSource.Encounter)
        {
            friendQueue = GameRunningData.GetRunningData().teammates;
            enemyQueue = new List<Person>();
            for(int i = 1; i <= (friendQueue.Count + 1) * 2; ++i)
            {
                enemyQueue.Add((Person)GlobalData.Persons[2].Clone());
            }
        }
        
        friendQueue.Add(GameRunningData.GetRunningData().player);
    }

    void SetPersonRowCol(List<Person> persons, bool isEnemy)
    {
        int maxPersonNum = persons.Count + 4;
        int maxColNum = maxPersonNum / 2 + (maxPersonNum % 2);
        int minCol = (mapWidth - maxColNum) / 2;
        int maxCol = minCol + maxColNum;
        HashSet<Vector2Int> rcSet = new HashSet<Vector2Int>();
        for(int i = 0; i < persons.Count; ++i)
        {
            int row = 0;
            if (isEnemy)
            {
                row = Random.Range(0, 2);
            }
            else
            {
                row = Random.Range(mapHeight - 2, mapHeight); 
            }
            int col = Random.Range(minCol, maxCol);
            var rc = new Vector2Int(row, col);
            if (!rcSet.Contains(rc))
            {
                persons[i].RowCol = rc;
                rcSet.Add(rc);
            }
            else
            {
                --i;
            }
        }
        
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

    public static void OneRoundOver(Person person)
    {
       // AttackBuffTool.TriggerValueBuff(FightPersonClick.currentPerson);
        AttackBuffTool.ReduceBuffDuration(person);
        GongBuffTool.EffectDefaultBuff(person);
        GongBuffTool.GongBuffRevertHPMP(person);
        GongBuffTool.EffectRecoverHalo(person);
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

    public static void DestoryHPSplider(Person person)
    {
        HPSplider hPSpliderScript = person.PersonObject.GetComponent<HPSplider>();
        Destroy(hPSpliderScript.HPObjectClone);
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
            person.InitAttribute();
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
        if (isFail || isSuccess)
        {
            foreach(Person person in friendQueue)
            {
                person.PersonObject.GetComponent<FightPersonClick>().enabled = false;
            }
            FightGUI.HideBattlePane();
            FightGridClick.ClearPathAndRange();
        }
        if (isSuccess)
        {
            StartCoroutine(FightGUI.ShowSuccessPanel());
            if(source != FightSource.Contest)
            {
                CreateReward();
            }
        }
        if(isFail)
        {
            
        }
        else
        {
            if (finished == friendQueue.Count)
            {
                finished = 0;
                FightPersonClick.currentPerson = null;
                StartCoroutine(EnemyRound());
            }
        }
    }

    void ResumePersonState()
    {
        foreach(var person in friendQueue)
        {
            if (!AttackBuffTool.IsPersonHasSkipBuff(person))
            {
                person.ControlState = BattleControlState.Moving;
                person.IsMoved = false;
            }
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
            FightAI.AIEnd = false;
            FightAI.NPCAI(enemy, friendQueue);
            yield return new WaitUntil(()=>FightAI.AIEnd);
        }
        ResumePersonState();
        DOTween.Clear();
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

    public static void CreateReward()
    {
        if (!isCreateReward)
        {
            isCreateReward = true;
            Random.InitState((int)System.DateTime.Now.Ticks);
            int x = Random.Range(1, 101);
            List<Good> rewards = new List<Good>();
            if (x > 50)
            {
                int i = Random.Range(0, GameConfig.RewardMaxIndex + 1);
                rewards.Add(GlobalData.Items[i]);
                x = Random.Range(1, 101);
                if (x >= 30)
                {
                    i = Random.Range(0, GameConfig.RewardMaxIndex + 1);
                    rewards.Add(GlobalData.Items[i]);
                }
                else
                {
                    i = Random.Range(0, GameConfig.RewardMaxIndex + 1);
                    rewards.Add(GlobalData.Items[i]);
                }
            }
            GameRunningData.GetRunningData().belongings.AddRange(rewards);
            int money = Random.Range(100, 201);
            int experspance = 5 + Random.Range(-2, 3);
            if(source == FightSource.MainLine)
            {
                experspance = 20 + Random.Range(-5, 5);
            }
            FightGUI.SetSettlement(rewards, experspance, money);
        }
    }

    void TestData()
    {
        source = FightSource.Encounter;
        friendQueue = new List<Person>();
        enemyQueue = new List<Person>();
        Person player = GameRunningData.GetRunningData().player;
        player.RowCol = new Vector2Int(12, 3);
        Person friend1 = GlobalData.Persons[1];
        friend1.RowCol = new Vector2Int(12, 6);
        Person enemy1 = GlobalData.Persons[2];
        enemy1.RowCol = new Vector2Int(0, 6);
        enemy1.CurrentMP = 10000;
        Person enemy2 = GlobalData.Persons[3];
        enemy2.RowCol = new Vector2Int(0, 9);
        enemy2.CurrentMP = 10000;

        friendQueue.Add(player);
        friendQueue.Add(friend1);
        enemyQueue.Add(enemy1);
        //enemyQueue.Add(enemy2);
    }
}

public enum FightSource
{
    MainLine, Contest, Encounter
}
