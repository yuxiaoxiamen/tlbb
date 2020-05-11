using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightMain : MonoBehaviour
{
    public GameObject gridPrefab; //格子的预制体
    public float heightGrap = 0.85f;
    public Dictionary<GameObject, Vector2Int> gridObjectToData; //通过格子对象获取格子的行列
    public Dictionary<Vector2Int, GameObject> gridDataToObject; //通过格子的行列获取格子对象
    public Dictionary<Vector2Int, Person> positionToPerson; //通过人物在战斗场景中的位置获取人物实例
    public List<Person> persons;
    public List<Person> friendQueue;//友方战斗的人
    public List<Person> enemyQueue;//敌方战斗的人
    public int mapWidth = 19; //地图的最大列数
    public int mapHeight = 15; //地图的最大行数
    public readonly float speed = 0.2f;
    public int finished = 0;
    public bool isSuccess;
    public bool isFail;
    private bool isStartEndOperation;
    public static FightSource source;
    public static Person contestEnemy;
    public static FightMain instance;

    // Start is called before the first frame update
    void Start()
    {
        isFail = false;
        isSuccess = false;
        instance = this;
        isStartEndOperation = false;
        finished = 0;

        CreateMap();

        GetFriendsAndEnemys();
        SetPersonRowCol(friendQueue, false);
        SetPersonRowCol(enemyQueue, true);

        //TestData();
        persons = new List<Person>();
        positionToPerson = new Dictionary<Vector2Int, Person>();
        SetEnemysHPMPEnergy();
        SetFightPerson(friendQueue, 0);
        SetFightPerson(enemyQueue, persons.Count);
        
        RotateEnemys();
        FightPersonClick.prePerson = null;
        FightPersonClick.currentPerson = null;
        FightPersonClick.SelectAPerson(GameRunningData.GetRunningData().player);
    }

    void SetEnemysHPMPEnergy()
    {
        foreach(Person enemy in enemyQueue)
        {
            enemy.ChangeHP(enemy.BaseData.HP, true);
            enemy.ChangeMP(enemy.BaseData.MP, true);
            enemy.ChangeEnergy(enemy.BaseData.Energy, true);
        }
    }

    void GetFriendsAndEnemys()
    {
        friendQueue = new List<Person>();
        enemyQueue = new List<Person>();
        if (source == FightSource.MainLine)
        {
            var key = GameRunningData.GetRunningData().GetPlaceDateKey();
            var conflict = GlobalData.MainLineConflicts[key];
            if (conflict.IsZ)
            {
                foreach(Person person in conflict.ZFriends)
                {
                    friendQueue.Add(person);
                    if (!GameRunningData.GetRunningData().teammates.Contains(person))
                    {
                        person.CurrentHP = person.BaseData.HP;
                        person.CurrentMP = person.BaseData.MP;
                    }
                    person.ChangeLikability(30, true);
                }
                foreach (Person person in conflict.ZEnemys)
                {
                    enemyQueue.Add(person);
                    person.ChangeLikability(30, false);
                }
            }
            else
            {
                foreach (Person person in conflict.FFriends)
                {
                    friendQueue.Add(person);
                    if (!GameRunningData.GetRunningData().teammates.Contains(person))
                    {
                        person.CurrentHP = person.BaseData.HP;
                        person.CurrentMP = person.BaseData.MP;
                    }
                    person.ChangeLikability(30, true);
                }
                foreach (Person person in conflict.FEnemys)
                {
                    enemyQueue.Add(person);
                    person.ChangeLikability(30, false);
                }
            }
            var teammates = GameRunningData.GetRunningData().teammates;
            List<Person> removeList = new List<Person>();
            foreach (Person teammate in teammates)
            {
                if (enemyQueue.Contains(teammate))
                {
                    removeList.Add(teammate);
                }
                else
                {
                    if (!friendQueue.Contains(teammate))
                    {
                        friendQueue.Add(teammate);
                    }
                }
            }
            foreach(Person p in removeList)
            {
                teammates.Remove(p);
            }
            friendQueue.Add(GameRunningData.GetRunningData().player);
        }
        else if(source == FightSource.Contest)
        {
            enemyQueue = new List<Person>
            {
                contestEnemy
            };
            friendQueue.Add(GameRunningData.GetRunningData().player);
        }
        else if(source == FightSource.Encounter)
        {
            friendQueue.AddRange(GameRunningData.GetRunningData().teammates);
            friendQueue.Add(GameRunningData.GetRunningData().player);
            int biCount = 0;
            int genCount = 0;
            int shenCount = 0;
            int jinCount = 0;
            int hpCount = 0;
            int mpCount = 0;
            foreach (Person p in friendQueue)
            {
                biCount += p.BaseData.Bi;
                genCount += p.BaseData.Gen;
                shenCount += p.BaseData.Shen;
                jinCount += p.BaseData.Jin;
                hpCount += p.BaseData.HP;
                mpCount += p.BaseData.MP;
            }

            for (int i = 1; i <= friendQueue.Count; ++i)
            {
                var enemy = (Person)GlobalData.Persons[94].Clone();
                enemy.BaseData.Bi = (int)(biCount * 1.2f  / friendQueue.Count);
                enemy.BaseData.Gen = (int)(genCount * 1.2f / friendQueue.Count);
                enemy.BaseData.Shen = (int)(shenCount * 1.2f / friendQueue.Count);
                enemy.BaseData.Jin = (int)(genCount * 1.2f / friendQueue.Count);
                enemy.BaseData.HP = (int)(hpCount * 1.2f / friendQueue.Count);
                enemy.BaseData.MP = (int)(mpCount * 1.2f / friendQueue.Count);
                enemy.BaseData.AttackStyles[0].Rank = 10;
                enemyQueue.Add(enemy);
            }
            
        }
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
                string rc = j + "_" + i;
                GameObject newGrid = Instantiate(gridPrefab);
                newGrid.name = rc;
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
        AttackBuffTool.ReduceHPMP(person);
        AttackBuffTool.ReduceBuffDuration(person);
        GongBuffTool.instance.EffectDefaultBuff(person);
        GongBuffTool.instance.GongBuffRevertHPMP(person);
        GongBuffTool.instance.EffectRecoverHalo(person);
    }

    public void PlayerFinished()
    {
        CountPlayerOver();
        FightPersonClick.currentPerson.ControlState = BattleControlState.End;
        FightGridClick.SwitchGridColor(gridDataToObject[FightPersonClick.currentPerson.RowCol], FightGridClick.defaultColor);
        FightGridClick.ClearPathAndRange();
        AttackTool.instance.ClearAttackRange();
        AttackTool.instance.ClearAttackDistance();
        FightGUI.HideBattlePane();

        StartCoroutine(SelectNextPerson());
    }

    public static void SetPersonHPSplider(Person person)
    {
        HPSplider hPSpliderScript = person.PersonObject.GetComponent<HPSplider>();
        RectTransform hpTransform = hPSpliderScript.HPObjectClone.transform.Find("HP").gameObject.GetComponent<RectTransform>();
        hpTransform.DOScale(new Vector3(person.CurrentHP * 1.0f / person.BaseData.HP,
            1, 1), 0.5f);
    }

    public void HideAllHPSplider()
    {
        foreach(Person p in friendQueue)
        {
            HPSplider hPSpliderScript = p.PersonObject.GetComponent<HPSplider>();
            hPSpliderScript.HPObjectClone.SetActive(false);
        }
        foreach (Person p in enemyQueue)
        {
            HPSplider hPSpliderScript = p.PersonObject.GetComponent<HPSplider>();
            hPSpliderScript.HPObjectClone.SetActive(false);
        }
    }

    public void ShowHPSplider()
    {
        foreach (Person p in friendQueue)
        {
            HPSplider hPSpliderScript = p.PersonObject.GetComponent<HPSplider>();
            hPSpliderScript.HPObjectClone.SetActive(true);
        }
        foreach (Person p in enemyQueue)
        {
            HPSplider hPSpliderScript = p.PersonObject.GetComponent<HPSplider>();
            hPSpliderScript.HPObjectClone.SetActive(true);
        }
    }

    private void SetHPSpliderColor(Person person)
    {
        if (friendQueue.Contains(person))
        {
            HPSplider hPSpliderScript = person.PersonObject.GetComponent<HPSplider>();
            hPSpliderScript.SetSliderColor();
        }
    }

    public void DestoryHPSplider(Person person)
    {
        HPSplider hPSpliderScript = person.PersonObject.GetComponent<HPSplider>();
        Destroy(hPSpliderScript.HPObjectClone);
    }

    void SetFightPerson(List<Person> personQueue, int i)
    {
        foreach(var person in personQueue)
        {
            Vector3 personPosition = gridDataToObject[person.RowCol].transform.position;
            GameObject personPrefab = Resources.Load<GameObject>("model/" + person.BaseData.ModelId);
            GameObject personObject = Instantiate(personPrefab, personPosition, Quaternion.identity);
            personObject.name = i++ + "";
            persons.Add(person);
            person.PersonObject = personObject;
            positionToPerson.Add(person.RowCol, person);
            person.InitAttribute();
            SetPersonHPSplider(person);
            SetHPSpliderColor(person);
        }
    }

    public HashSet<Vector2Int> GetGrids()
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
        if (isSuccess)
        {
            if (!isStartEndOperation)
            {
                isStartEndOperation = true;
                StopGame();
                StartCoroutine(FightGUI.ShowSuccessPanel());
                if (source != FightSource.Contest)
                {
                    CreateReward();
                }
            }
        }
        if(isFail)
        {
            if (!isStartEndOperation)
            {
                isStartEndOperation = true;
                StopGame();
                StartCoroutine(FightGUI.ShowFailPanel());
            }
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

    void StopGame()
    {
        foreach (Person person in friendQueue)
        {
            person.PersonObject.GetComponent<FightPersonClick>().enabled = false;
        }
        FightGUI.HideBattlePane();
        FightGridClick.ClearPathAndRange();
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
            else
            {
                OneRoundOver(person);
                CountPlayerOver();
            }
        }
        FightPersonClick.currentPerson = null;
    }

    public void CountPlayerOver()
    {
        ++finished;
    }

    IEnumerator EnemyRound()
    {
        foreach (Person enemy in enemyQueue)
        {
            yield return new WaitForSeconds(0.2f);
            CameraFollow.cameraFollowInstance.SetCameraFollowTarget(enemy);
            FightAI.AIEnd = false;
            FightAI.NPCAI(enemy, friendQueue);
            yield return new WaitUntil(()=>FightAI.AIEnd);
        }
        ResumePersonState();
        StartCoroutine(SelectNextPerson());
    }

    public IEnumerator SelectNextPerson()
    {
        yield return new WaitForSeconds(0.2f);
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
        foreach (Good reward in rewards)
        {
            GameRunningData.GetRunningData().AddItem(reward);
        }
        int money = Random.Range(100, 201);
        int experspance = 5 + Random.Range(-2, 3);
        if (source == FightSource.MainLine)
        {
            experspance = 20 + Random.Range(-5, 5);
        }
        GameRunningData.GetRunningData().experspance += experspance;
        GameRunningData.GetRunningData().money += money;
        FightGUI.SetSettlement(rewards, experspance, money);
    }

    void TestData()
    {
        source = FightSource.Encounter;
        friendQueue = new List<Person>();
        enemyQueue = new List<Person>();
        Person player = GameRunningData.GetRunningData().player;
        player.RowCol = new Vector2Int(12, 3);
        Person friend1 = GlobalData.Persons[2];
        friend1.RowCol = new Vector2Int(12, 6);
        Person enemy1 = GlobalData.Persons[3];
        enemy1.RowCol = new Vector2Int(9, 3);
        Person enemy2 = GlobalData.Persons[4];
        enemy2.RowCol = new Vector2Int(0, 9);
        player.BaseData.ModelId = 34;
        friend1.BaseData.ModelId = 35;
        enemy1.BaseData.ModelId = 31;
        enemy2.BaseData.ModelId = 33;
        player.CurrentHP = player.BaseData.HP = 10000;
        friend1.CurrentHP = friend1.BaseData.HP = 1000;
        enemy1.CurrentHP = enemy1.BaseData.HP = 10000;
        enemy2.CurrentHP = enemy2.BaseData.HP = 1000;

        //player.SelectedInnerGong = player.BaseData.InnerGongs[1];

        friendQueue.Add(player);
        
        friendQueue.Add(friend1);
        enemyQueue.Add(enemy1);
        
        enemyQueue.Add(enemy2);
    }
}

public enum FightSource
{
    MainLine, Contest, Encounter
}
