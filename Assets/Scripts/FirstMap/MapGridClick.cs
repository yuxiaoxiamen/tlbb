using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGridClick : MonoBehaviour
{
    public GameObject textPrefab;
    private GameObject textObject;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnMouseDown()
    {
        if (!ControlBottomPanel.isMouseInPane)
        {
            FirstPlace firstPlace = GridInPlace(FirstMapMain.gridObjectToData[gameObject]);
            var endRc = FirstMapMain.gridObjectToData[gameObject];
            if (firstPlace != null)
            {
                endRc = firstPlace.Entry;
            }
            DOTween.KillAll();
            FirstMapMain.pathIndex = 0;
            List<Vector2Int> movePath = PersonMoveTool.FindPath(FirstMapMain.player.RowCol, endRc, FirstMapMain.GetAllGrids(), GetMapObstacles(), false);
            FirstMapMain.MovePerson(movePath, FirstMapMain.player, 0.2f, firstPlace);
        }
    }

    private void OnMouseEnter()
    {
        if (!ControlBottomPanel.isMouseInPane)
        {
            FirstPlace place = GridInPlace(FirstMapMain.gridObjectToData[gameObject]);
            if (place != null)
            {
                textObject = Instantiate(textPrefab);
                textObject.transform.position = FirstMapMain.gridDataToObject[place.Entry].transform.position + new Vector3(0, -0.2f, 0);
                textObject.transform.Find("text").GetComponent<TextMesh>().text = place.Name;
            }
        }

    }

    private void OnMouseExit()
    {
        if(textObject != null)
        {
            Destroy(textObject);
        }
    }

    FirstPlace GridInPlace(Vector2Int grid)
    {
        foreach(var place in GlobalData.FirstPlaces)
        {
            if (place.IsGridInPlace(grid))
            {
                return place;
            }
        }
        return null;
    }

    HashSet<Vector2Int> GetMapObstacles()
    {
        HashSet<Vector2Int> obstacles = new HashSet<Vector2Int>();
        foreach(var place in GlobalData.FirstPlaces)
        {
            obstacles.UnionWith(place.Hold);
        }
        return obstacles;
    }
}
