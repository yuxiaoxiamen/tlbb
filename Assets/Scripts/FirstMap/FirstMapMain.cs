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

    static public void MovePerson(List<Vector2Int> movePath, Person person, float speed, FirstPlace place)
    {
        List<GameObject> realPath = new List<GameObject>();
        foreach (var point in movePath)
        {
            var gridObject = gridDataToObject[point];
            realPath.Add(gridObject);
        }
        Move(person, realPath, speed, place);
        if (movePath.Count > 0)
        {
            person.RowCol = movePath[movePath.Count - 1];
        }
    }

    public static int pathIndex = 0;
    static private void Move(Person person, List<GameObject> path, float speed, FirstPlace place)
    {
        if (pathIndex < path.Count)
        {
            person.PersonObject.GetComponent<Animator>().SetInteger("Direction", 
                GetDirectionNumber(person.PersonObject.transform.position, path[pathIndex].transform.position));
            person.PersonObject.GetComponent<Animator>().SetBool("IsMoving",true);
            person.PersonObject.transform.DOMove(path[pathIndex].transform.position, speed).OnComplete(() =>
            {
                person.RowCol = gridObjectToData[path[pathIndex]];
                ++pathIndex;
                Move(person, path, speed, place);
            });
        }
        else
        {
            pathIndex = 0;
            person.PersonObject.GetComponent<Animator>().SetBool("IsMoving", false);

            if (place != null)
            {
                DOTween.Clear(true);
                GameRunningData.GetRunningData().currentPlace = place;
                if(place.Sites == null)
                {
                    SceneManager.LoadScene("ThridMap");
                }
                else{
                    SceneManager.LoadScene("SecondMap");
                }
            }
        }
    }

    static int GetDirectionNumber(Vector3 current, Vector3 next)
    {
        if(current.y > next.y)
        {
            return 0;
        }
        else if(current.y < next.y)
        {
            return 1;
        }
        else if(current.x > next.x)
        {
            return 3;
        }
        else
        {
            return 2;
        }
    }
}