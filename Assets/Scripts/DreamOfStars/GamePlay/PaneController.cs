using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PaneController : MonoBehaviour
{
    public Pane currentPane;
    public PaneGroupController parentGroup;
    

    void Awake() // Awake早于Start
    {

    }

    public void InitPane(Pane p)
    {
        transform.AddComponent<MeshFilter>().mesh = MakePaneMesh(p);
        transform.AddComponent<MeshRenderer>().material = MakePaneMaterial(Color.white,Color.yellow);

        currentPane = p;
    }

    // Update is called once per frame
    void Update()
    {
        var main = GameManager.MainInstance;
        if (main.IsPlaying)
        {
            currentPane.Update(main.CurrentTime + main.VisualOffset);
            transform.localPosition = currentPane.Position;
            transform.localEulerAngles = currentPane.Rotation;
            if (currentPane.IsDeforming) // TODO: 怎么判断这个deforming
            {
                transform.GetComponent<MeshFilter>().mesh = MakePaneMesh(currentPane);
            }
        }
    }

    public Mesh MakePaneMesh(Pane p)
    {
        Mesh PaneMesh = new Mesh();

        if (p.Shape == PaneShape.Rectangle)
        {
            //TODO: 只写了长方形的
            // 有四个顶点就能划分成两个三角形了
            // v3 ----- v2
            // |      / |
            // |    /   |
            // |  /     |
            // v0 ----- v1
            // 注意顺序，不然法线可能会出错
            // TODO: 如果一个面只有两个三角形，那么没多少形变的种类
            List<Vector3> vertices = new List<Vector3>
                {
                    // 下面这样的顶点会导致控制点在0 0 0处
                    //new Vector3(0, 0, 0),
                    //new Vector3(Width,0,0),
                    //new Vector3(Width,Height,0),
                    //new Vector3(0,Height,0)
                    // 把pivot放到几何中心
                    new Vector3(-p.Width/2, -p.Height/2),
                    new Vector3(p.Width/2, -p.Height/2),
                    new Vector3(p.Width/2, p.Height/2),
                    new Vector3(-p.Width/2,p.Height/2)
                };
            List<int> triangles = new List<int>
                {
                    // 按顺时针连接
                    0,2,1,
                    0,3,2
                };
            // TODO: 完善UV
            List<Vector2> uv = new List<Vector2>() {
                    Vector2.zero,
                    Vector2.zero,
                    Vector2.zero,
                    Vector2.zero
                };
            PaneMesh.Clear();
            PaneMesh.vertices = vertices.ToArray();
            PaneMesh.triangles = triangles.ToArray();
            PaneMesh.uv = uv.ToArray();
            PaneMesh.RecalculateNormals();
            PaneMesh.RecalculateBounds();
        }

        return PaneMesh;
    }

    /// <summary>
    /// 生成面的材质
    /// </summary>
    /// <param name="mainColor">主要颜色</param>
    /// <param name="specColor">反射光的颜色</param>
    /// <param name="emissionColor">自发光颜色</param>
    /// <returns></returns>
    public Material MakePaneMaterial(Color mainColor, Color specColor, Color? emissionColor = null)
    {
        Material paneMaterial = new Material(Shader.Find("PaneStatic"));

        paneMaterial.SetColor("_Color", mainColor);
        paneMaterial.SetColor("_SpecColor", specColor);
        if(emissionColor != null) paneMaterial.SetColor("_Emission", (Color)emissionColor);

        return paneMaterial;
    }

}


public class PaneManager
{
    public Pane CurrentPane;
    public Mesh CurrentMesh = new Mesh();

    public float CurrentSpeed;
    public float CurrentScrollPosition;

    /// <summary>
    /// 销毁资源
    /// </summary>
    public void Dispose()
    {
        if(CurrentMesh!=null) MonoBehaviour.DestroyImmediate(CurrentMesh);
    }
}




///// <summary>
///// 用来控制Pane的形变
///// </summary>
//[System.Serializable]
//public class Pane???? : AStoryBoard, IDeepCloneable<Pane????> {
//    public float Offset;

//    // 初步设置六个锚点，p为Pane的Position，其他点的位置均由Vector2表示
//    // v4 ----- v5
//    // |        |
//    // v2   p   v3
//    // |        |
//    // v0 ----- v1
//    public Vector2 v0;
//    public Vector2 v1;

   

//}