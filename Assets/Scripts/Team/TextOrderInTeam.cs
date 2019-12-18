using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextOrderInTeam : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        MeshRenderer myMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        myMeshRenderer.sortingLayerName = "float";
        myMeshRenderer.sortingOrder = 10;
    }
}