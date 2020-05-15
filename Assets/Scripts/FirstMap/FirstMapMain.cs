using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstMapMain : MonoBehaviour
{
    public GameObject gridPrefab;
    public GameObject personPrefab;
    public static Dictionary<Vector2Int, GameObject> gridDataToObject = new Dictionary<Vector2Int, GameObject>();
    public static Dictionary<GameObject, Vector2Int> gridObjectToData = new Dictionary<GameObject, Vector2Int>();
    public static Person player;
    private Direction direction;
    public GameObject arrowObject;
    public float moveSpeed = 0.7f;
    private bool lastMoveOver = true;
    private bool isInConversation = false;

    // Start is called before the first frame update
    void Start()
    {
        gridDataToObject.Clear();
        gridObjectToData.Clear();
        Time.timeScale = 1;
        float mapWidth = gameObject.GetComponent<Renderer>().bounds.size.x;
        float mapHeight = gameObject.GetComponent<Renderer>().bounds.size.y;

        float gridWidth = gridPrefab.GetComponent<Renderer>().bounds.size.x;
        float gridHeight = gridPrefab.GetComponent<Renderer>().bounds.size.y;

        int maxRow = (int)(mapHeight / gridHeight);
        int maxCol = (int)(mapWidth / gridWidth);

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
            }
        }

        player = GameRunningData.GetRunningData().player;

        player.PersonObject = Instantiate(personPrefab);
        player.PersonObject.transform.position = gridDataToObject[player.RowCol].transform.position;

        ControlDialogue.instance.HideDialogue();
    }

    private void Update()
    {
        if (!ControlBottomPanel.isMouseInPane && !isInConversation)
        {
            HideCursor();
            SetDirection();
            if (Input.GetMouseButton(0))
            {
                StartCoroutine(Move());
            }
            if (Input.GetMouseButtonUp(0))
            {
                StopAllCoroutines();
                player.PersonObject.GetComponent<Animator>().SetBool("IsMoving", false);
            }

            FirstPlace place = GridInPlaceEnter(player.RowCol);
            if (place != null)
            {
                Cursor.visible = true;
                GameRunningData.GetRunningData().currentPlace = place;
                GameRunningData.GetRunningData().SavePlayerMapRc();
                if (place.Sites == null)
                {
                    SceneManager.LoadScene("ThridMap");
                }
                else
                {
                    SceneManager.LoadScene("SecondMap");
                }
            }
        }
        else
        {
            ShowCursor();
            player.PersonObject.GetComponent<Animator>().SetBool("IsMoving", false);
        }
    }

    FirstPlace GridInPlaceEnter(Vector2Int grid)
    {
        foreach (var place in GlobalData.FirstPlaces)
        {
            if (place.IsGridInEnter(grid))
            {
                return place;
            }
        }
        return null;
    }

    void SetDirection()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var position = new Vector3(mousePosition.x, mousePosition.y, 0);
        var vector = position - player.PersonObject.transform.position;
        float upAngle = Vector3.Angle(player.PersonObject.transform.up, vector);
        float rightAngle = Vector3.Angle(player.PersonObject.transform.right, vector);
        if (upAngle <= 10)
        {
            direction = Direction.Up;
            arrowObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ui/上");
        }
        if (upAngle <= 55 && upAngle >= 35)
        {
            if(rightAngle < 90)
            {
                direction = Direction.UpRight;
                arrowObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ui/右上");
            }
            else
            {
                direction = Direction.UpLeft;
                arrowObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ui/左上");
            }
        }
        if (upAngle >= 170)
        {
            direction = Direction.Down;
            arrowObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ui/下");
        }
        if (upAngle <= 145 && upAngle >= 125)
        {
            if (rightAngle < 90)
            {
                direction = Direction.DownRight;
                arrowObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ui/右下");
            }
            else
            {
                direction = Direction.DownLeft;
                arrowObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ui/左下");
            }
        }
        if (rightAngle <= 10)
        {
            direction = Direction.Right;
            arrowObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ui/右");
        }
        if (rightAngle >= 170)
        {
            direction = Direction.Left;
            arrowObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ui/左");
        }
        arrowObject.transform.position = position;
    }

    public static HashSet<Vector2Int> GetAllGrids()
    {
        HashSet<Vector2Int> grids = new HashSet<Vector2Int>();
        foreach (var key in gridDataToObject.Keys)
        {
            grids.Add(key);
        }
        return grids;
    }

    HashSet<Vector2Int> GetMapObstacles()
    {
        HashSet<Vector2Int> obstacles = new HashSet<Vector2Int>();
        foreach (var place in GlobalData.FirstPlaces)
        {
            obstacles.UnionWith(place.Hold);
        }
        obstacles.UnionWith(GlobalData.MapObstacle);
        return obstacles;
    }

    private IEnumerator Move()
    {
        yield return new WaitUntil(() => lastMoveOver);
        lastMoveOver = false;
        GameObject nextGridObject = GetNextGridObject();
        if (nextGridObject != null)
        {
            player.PersonObject.GetComponent<Animator>().SetInteger("Direction", GetDirectionNumber());
            player.PersonObject.GetComponent<Animator>().SetBool("IsMoving",true);
            SoundEffectControl.instance.PlaySoundEffect(0);
            StopAllCoroutines();
            player.PersonObject.transform.DOMove(nextGridObject.transform.position, moveSpeed).OnComplete(() =>
            {
                player.RowCol = gridObjectToData[nextGridObject];
                RandomEvent();
            });
        }
        else
        {
            lastMoveOver = true;
        }
    }

    void RandomEvent()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        int x = Random.Range(1, 101);
        if (x <= GameConfig.MapEventProbability)
        {
            isInConversation = true;
            GameRunningData.GetRunningData().playerPreRc = player.RowCol;
            ControlDialogue.instance.StartConversation(GameConfig.MapEventConversation, ()=>
            {
                FightMain.source = FightSource.Encounter;
                SceneManager.LoadScene("Fight");
            });
        }
        else
        {
            x = Random.Range(1, 101);
            if (x <= 50)
            {
                isInConversation = true;
                GameRunningData.GetRunningData().playerPreRc = player.RowCol;
                int randomI = Random.Range(1, 6);
                switch (randomI)
                {
                    case 1:
                        ControlDialogue.instance.StartConversation(GameConfig.JiuEventConversation, () =>
                        {
                            SceneManager.LoadScene("LiquorPower");
                        });
                        break;
                    case 2:
                        ControlDialogue.instance.StartConversation(GameConfig.YiEventConversation, () =>
                        {
                            SceneManager.LoadScene("MedicalSkill");
                        });
                        break;
                    case 3:
                        ControlDialogue.instance.StartConversation(GameConfig.DanEventConversation, () =>
                        {
                            SceneManager.LoadScene("Alchemy");
                        });
                        break;
                    case 4:
                        ControlDialogue.instance.StartConversation(GameConfig.QieEventConversation, () =>
                        {
                            SceneManager.LoadScene("CutUp");
                        });
                        break;
                    case 5:
                        ControlDialogue.instance.StartConversation(GameConfig.DuanEventConversation, () =>
                        {
                            SceneManager.LoadScene("Mining");
                        });
                        break;
                }
            }
            else
            {
                lastMoveOver = true;
            }
        }
    }

    void ShowCursor()
    {
        Cursor.visible = true;
        arrowObject.SetActive(false);
    }

    void HideCursor()
    {
        Cursor.visible = false;
        arrowObject.SetActive(true);
    }

    GameObject GetNextGridObject()
    {
        Vector2Int nextRc = player.RowCol;
        switch (direction)
        {
            case Direction.Up:
                nextRc.x -= 1;
                break;
            case Direction.Down:
                nextRc.x += 1;
                break;
            case Direction.Left:
                nextRc.y -= 1;
                break;
            case Direction.Right:
                nextRc.y += 1;
                break;
            case Direction.UpRight:
                nextRc.x -= 1;
                nextRc.y += 1;
                break;
            case Direction.DownRight:
                nextRc.x += 1;
                nextRc.y += 1;
                break;
            case Direction.UpLeft:
                nextRc.x -= 1;
                nextRc.y -= 1;
                break;
            case Direction.DownLeft:
                nextRc.x += 1;
                nextRc.y -= 1;
                break;
        }
        if (!GetMapObstacles().Contains(nextRc) && GetAllGrids().Contains(nextRc))
        {
            return gridDataToObject[nextRc];
        }
        else
        {
            return null;
        }
    }

    int GetDirectionNumber()
    {
        switch (direction)
        {
            case Direction.Up:
                return 1;
            case Direction.Down:
                return 0;
            case Direction.Left:
                return 3;
            case Direction.Right:
                return 2;
            case Direction.UpRight:
                return 4;
            case Direction.UpLeft:
                return 5;
            case Direction.DownRight:
                return 6;
            case Direction.DownLeft:
                return 7;
        }
        return 0;
    }
}

enum Direction { Up, Right, Down, Left, UpRight, UpLeft, DownRight, DownLeft}