using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


//1.当将TestTriangle的代码挂在GameObject上的时候，会自动添加这两个组件。
//2.当要移除MeshRenderer或MeshFilter的时候，编辑器会提示不能移除。//如果要移除，先移除TestTriangle脚本之后，才能移除。
[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]

public class MainAttributes : MonoBehaviour
{

    void Start()
    {
        int characterId = Para_Pass.characterId;
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        //gameObject.GetComponent<MeshRenderer>().material = mat;

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();



        float biPercent = (float)((GlobalData.Persons[characterId].BaseData.Bi) / 100.00);   //臂力
        float genPercent = (float)(GlobalData.Persons[characterId].BaseData.Gen / 100.00);   //根骨
        float wuPercent = (float)(GlobalData.Persons[characterId].BaseData.Wu / 100.00);     //悟性
        float shenPercent = (float)(GlobalData.Persons[characterId].BaseData.Shen / 100.00); //身法
        float jinPercent = (float)(GlobalData.Persons[characterId].BaseData.Jin/ 100.00);    //筋骨


        switch (gameObject.name) {
            case "GenGu_WuXing":
                //设置顶点
                mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0*genPercent, 1*genPercent, 0), new Vector3((float)System.Math.Cos(System.Math.PI / 10)*wuPercent, (float)System.Math.Sin(System.Math.PI / 10)*wuPercent, 0) };
                //设置三角形顶点顺序，顺时针设置
                mesh.triangles = new int[] { 0, 1, 2 };
                break;

            case "WuXing_ShenFa":
                mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3((float)System.Math.Cos(System.Math.PI / 10) * wuPercent, (float)System.Math.Sin(System.Math.PI / 10) * wuPercent, 0), new Vector3((float)System.Math.Sin(System.Math.PI / 5)*shenPercent, -(float)System.Math.Cos(System.Math.PI / 5) * shenPercent, 0) };
                mesh.triangles = new int[] { 0, 1, 2 };
                break;

            case "ShenFa_JinGu":
                mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3((float)System.Math.Sin(System.Math.PI / 5) * shenPercent, -(float)System.Math.Cos(System.Math.PI / 5) * shenPercent, 0), new Vector3(-(float)System.Math.Sin(System.Math.PI / 5)*jinPercent, -(float)System.Math.Cos(System.Math.PI / 5) * jinPercent, 0) };
                mesh.triangles = new int[] { 0, 1, 2 };
                break;

            case "JinGu_BiLi":
                mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(-(float)System.Math.Sin(System.Math.PI / 5) * jinPercent, -(float)System.Math.Cos(System.Math.PI / 5) * jinPercent, 0), new Vector3(-(float)System.Math.Cos(System.Math.PI / 10)*(float)biPercent, (float)System.Math.Sin(System.Math.PI / 10) * (float)biPercent, 0) };
                mesh.triangles = new int[] { 0, 1, 2 };
                break;

            case "BiLi_GenGu":
                mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(-(float)System.Math.Cos(System.Math.PI / 10) * (float)biPercent, (float)System.Math.Sin(System.Math.PI / 10) * (float)biPercent, 0), new Vector3( 0*genPercent, 1*genPercent, 0) };
                mesh.triangles = new int[] { 0, 1, 2 };
                break;
        }
        GetComponent<MeshRenderer>().material.color = Color.cyan;
    }
}