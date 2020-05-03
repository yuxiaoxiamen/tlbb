using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignMapMain : MonoBehaviour
{
    public GameObject gridPrefab;
    // Start is called before the first frame update
    void Start()
    {
        float mapWidth = gameObject.GetComponent<Renderer>().bounds.size.x;
        float mapHeight = gameObject.GetComponent<Renderer>().bounds.size.y;

        float gridWidth = gridPrefab.GetComponent<Renderer>().bounds.size.x;
        float gridHeight = gridPrefab.GetComponent<Renderer>().bounds.size.y;

        int maxRow = (int)(mapHeight / gridHeight);
        int maxCol = (int)(mapWidth / gridWidth);
        var o = GetMapObstacles();
        var e = GetEntry();
        for (int r = 0; r < maxRow; ++r)
        {
            for (int c = 0; c < maxCol; ++c)
            {
                GameObject newGrid = Instantiate(gridPrefab);
                newGrid.name = r + "_" + c;
                newGrid.transform.parent = transform;
                newGrid.transform.position += new Vector3(c * gridWidth, -r * gridHeight, 0);
                var rowAndCol = new Vector2Int(r, c);
                if (o.Contains(rowAndCol))
                {
                    newGrid.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.4f);
                }
                if (e.Contains(rowAndCol))
                {
                    newGrid.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.4f);
                }
            }
        }
    }

    HashSet<Vector2Int> GetEntry()
    {
        HashSet<Vector2Int> entrys = new HashSet<Vector2Int>();
        foreach (var place in GlobalData.FirstPlaces)
        {
            entrys.Add(place.Entry);
        }
        return entrys;
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
}
