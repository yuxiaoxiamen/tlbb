using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LiquorPowerMain : MonoBehaviour
{
    public bool[,] grids;
    public Dictionary<Vector2Int, GameObject> gridDataToObject = new Dictionary<Vector2Int, GameObject>();
    public Dictionary<GameObject, Vector2Int> gridObjectToData = new Dictionary<GameObject, Vector2Int>();
    public GameObject gridPrefab;
    public GameObject personPrefab;
    public GameObject flashObject;
    private Person player;
    public Person enemy;
    public static LiquorPowerMain instance;
    public int maxCol;
    public int maxRow;
    public Sequence sequence;
    public GameObject startPanel;
    public bool isGameStart;
    public Vector2Int startRc = new Vector2Int(2, 1);
    public Vector2Int endRc;
    private GameObject successPanel;
    public GameObject failPanel;
    private bool isShow = false;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        Vector2 wh = GetWidthHeight();
        float gridWidth = gridPrefab.GetComponent<Renderer>().bounds.size.x;
        float gridHeight = gridPrefab.GetComponent<Renderer>().bounds.size.y;
        maxCol = (int)(wh.x / gridWidth);
        maxRow = (int)(wh.y / gridHeight);

        CreateMaze();
        enemy = new Person
        {
            RowCol = endRc
        };
        enemy.PersonObject = Instantiate(personPrefab, gridDataToObject[enemy.RowCol].transform.position, Quaternion.identity);
        player = GameRunningData.GetRunningData().player;
        player.RowCol = startRc;
        player.PersonObject = Instantiate(personPrefab, gridDataToObject[player.RowCol].transform.position, Quaternion.identity);
        enemy.PersonObject.GetComponent<SpriteRenderer>().color = Color.red;

        isGameStart = false;
        Button startButton = startPanel.transform.Find("Button").GetComponent<Button>();
        startButton.onClick.AddListener(() =>
        {
            isGameStart = true;
            sequence = DOTween.Sequence();
            sequence.OnComplete(() =>
            {
                isGameStart = false;
                failPanel.SetActive(true);
            });
            StartCoroutine(ControlFlash());
            startPanel.SetActive(false);
        });
        SetFailAndSuccessPanel();
    }

    void SetFailAndSuccessPanel()
    {
        successPanel = GameObject.Find("successPanel");
        successPanel.SetActive(false);
        successPanel.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {

        });
        failPanel = GameObject.Find("failPanel");
        failPanel.SetActive(false);
        failPanel.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {

        });
    }

    Vector2 GetWidthHeight()
    {
        float leftBorder;
        float rightBorder;
        float topBorder;
        float downBorder;

        Vector3 cornerPos = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Mathf.Abs(Camera.main.transform.position.z)));

        leftBorder = Camera.main.transform.position.x - (cornerPos.x - Camera.main.transform.position.x);
        rightBorder = cornerPos.x;
        topBorder = cornerPos.y;
        downBorder = Camera.main.transform.position.y - (cornerPos.y - Camera.main.transform.position.y);

        return new Vector2(rightBorder - leftBorder, topBorder - downBorder);
    }

    IEnumerator ControlFlash()
    {
        if (isGameStart)
        {
            float time = Random.Range(3f, 5f);
            yield return new WaitForSeconds(time);
            flashObject.GetComponent<SpriteRenderer>().DOFade(1, 1f).OnComplete(() =>
            {
                flashObject.GetComponent<SpriteRenderer>().DOFade(0, 1f).OnComplete(() =>
                {
                    StartCoroutine(ControlFlash());
                });
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStart)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Vector2Int next = new Vector2Int(player.RowCol.x + 1, player.RowCol.y);
                MovePlayer(next);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                Vector2Int next = new Vector2Int(player.RowCol.x - 1, player.RowCol.y);
                MovePlayer(next);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Vector2Int next = new Vector2Int(player.RowCol.x, player.RowCol.y + 1);
                MovePlayer(next);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Vector2Int next = new Vector2Int(player.RowCol.x, player.RowCol.y - 1);
                MovePlayer(next);
            }
            if(player.RowCol == endRc)
            {
                isGameStart = false;
                if (!isShow)
                {
                    successPanel.SetActive(true);
                    sequence.Kill();
                    isShow = true;
                }
            }
        }
    }

    void MovePlayer(Vector2Int next)
    {
        if (grids[next.x, next.y])
        {
            player.PersonObject.transform.DOMove(gridDataToObject[next].transform.position, 0.2f).OnComplete(()=>
            {
                player.RowCol = next;
            });
        }
    }

    public void MoveEnemy(Vector2Int next)
    {
        sequence.Append(enemy.PersonObject.transform.DOMove(gridDataToObject[next].transform.position, 0.2f));
    }

    void CreateMaze()
    {
        grids = new bool[maxRow, maxCol];
        List<Vector2Int> walls = new List<Vector2Int>
        {
            new Vector2Int(2, 2)
        };

        for (int i = 0; i < maxRow; ++i)
        {
            grids[i, 0] = true;
            grids[i, maxCol - 1] = true;
        }
        for (int i = 0; i < maxCol; ++i)
        {
            grids[0, i] = true;
            grids[maxRow - 1, i] = true;
        }

        while (walls.Count > 0)
        {
            int randomIndex = Random.Range(0, walls.Count);
            Vector2Int randomWall = walls[randomIndex];
            int count = 0;
            for (int i = randomWall.x - 1; i < randomWall.x + 2; i++)
            {
                for (int j = randomWall.y - 1; j < randomWall.y + 2; j++)
                {
                    if (Mathf.Abs(randomWall.x - i) + Mathf.Abs(randomWall.y - j) == 1 && grids[i, j])
                    {
                        ++count;
                    }
                }
            }

            if (count <= 1)
            {
                grids[randomWall.x, randomWall.y] = true;
                for (int i = randomWall.x - 1; i < randomWall.x + 2; i++)
                {
                    for (int j = randomWall.y - 1; j < randomWall.y + 2; j++)
                    {
                        if (Mathf.Abs(randomWall.x - i) + Mathf.Abs(randomWall.y - j) == 1 && !grids[i, j])
                        {
                            walls.Add(new Vector2Int(i, j));
                        }
                    }
                }
            }
            walls.Remove(randomWall);
        }
        grids[startRc.x, startRc.y] = true;
        grids[startRc.x, startRc.y - 1] = false;
        for (int i = maxRow - 3; i >= 0; --i)
        {
            if (grids[i, maxCol - 3])
            {
                grids[i, maxCol - 2] = true;
                endRc = new Vector2Int(i, maxCol - 2);
                grids[i, maxCol - 1] = false;
                break;
            }
        }
        DrawMaze();
    }

    void DrawMaze()
    {
        float gridWidth = gridPrefab.GetComponent<Renderer>().bounds.size.x;
        float gridHeight = gridPrefab.GetComponent<Renderer>().bounds.size.y;
        for (int r = 0; r < maxRow; ++r)
        {
            for (int c = 0; c < maxCol; ++c)
            {
                GameObject newGrid = Instantiate(gridPrefab);
                newGrid.name = r + "_" + c;
                newGrid.transform.parent = transform;
                newGrid.transform.position += new Vector3(c * gridWidth, -r * gridHeight, 0);
                var rowAndCol = new Vector2Int(r, c);
                gridObjectToData.Add(newGrid, rowAndCol);
                gridDataToObject.Add(rowAndCol, newGrid);
                if (!grids[r, c])
                {
                    newGrid.GetComponent<SpriteRenderer>().color = Color.black;
                }
                if(r == 0 || c == 0 || r == maxRow - 1 || c == maxCol - 1)
                {
                    newGrid.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                }
            }
        }
    }
}
