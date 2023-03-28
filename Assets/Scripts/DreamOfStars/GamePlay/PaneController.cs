using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PaneController : MonoBehaviour
{
    public Pane CurrentPane;
    public PaneGroupController ParentGroup;


    void Awake() // Awake早于Start
    {

    }

    public void InitPane(Pane p)
    {
        transform.AddComponent<MeshFilter>().mesh=MakeMesh(p);
        transform.AddComponent<MeshRenderer>().material = MakeMaterial(Color.white,Color.yellow);
        foreach (JudgeLine line in p.Lines)
        {
            MakeJudgeLine(line);
        }

        CurrentPane = p;
    }

    // Update is called once per frame
    void Update()
    {
        var main = GameManager.MainInstance;
        if (main.IsPlaying)
        {
            CurrentPane.Update(main.CurrentTime+main.VisualOffset);
            transform.localPosition = CurrentPane.Position;
            transform.localEulerAngles = CurrentPane.Rotation;
        }
    }

    public Mesh MakeMesh(Pane p)
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
    public Material MakeMaterial(Color mainColor, Color specColor, Color? emissionColor = null)
    {
        Material paneMaterial = new Material(Shader.Find("PaneStatic"));

        paneMaterial.SetColor("_Color", mainColor);
        paneMaterial.SetColor("_SpecColor", specColor);
        if(emissionColor != null) paneMaterial.SetColor("_Emission", (Color)emissionColor);

        return paneMaterial;
    }

    public void MakeJudgeLine(JudgeLine line)
    {
        JudgeLineController jc = transform.AddComponent<JudgeLineController>();
        jc.InitJudgeLine(line);
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

[System.Serializable]
public enum PaneShape
{
    Rectangle, // 长方形，含正方形
    Triangle   // 三角形，真的有必要搞这种谱面吗hhh
}

[System.Serializable]
public class Pane : AStoryBoard, IDeepCloneable<Pane>
{
    public string Name = "New Pane";
    public string Group;

    public Vector3 Position;
    public Vector3 Rotation;

    public float Width=1.0f;
    public float Height=1.0f;

    public List<JudgeLine> Lines = new List<JudgeLine>();

    public PaneShape Shape = PaneShape.Rectangle;
    
    // 3.28 更新，Pane只是作为数据结构，不在此new GameObject并绑定了，Object和Mesh的管理交给PaneController


    public new static EventNode[] EventNodes =
    {
        new EventNode()
        {
            ID="PanePos_X",
            Name="Pane Position_X",
            Get=(p)=>((Pane)p).Position.x,
            Set=(p,a)=>{((Pane)p).Position.x=a; }
        },
        new EventNode()
        {
            ID="PanePos_Y",
            Name="Pane Position_Y",
            Get=(p)=>((Pane)p).Position.y,
            Set=(p,a)=>{((Pane)p).Position.y=a; }
        },
        new EventNode()
        {
            ID="PanePos_Z",
            Name="Pane Position_Z",
            Get=(p)=>((Pane)p).Position.z,
            Set=(p,a)=>{((Pane)p).Position.z=a; }
        },
        new EventNode()
        {
            ID="PaneRot_X",
            Name="Pane Rotation_X",
            Get=(p)=>((Pane)p).Rotation.x,
            Set=(p,a)=>{((Pane)p).Rotation.x=a; }
        },
        new EventNode()
        {
            ID="PaneRot_Y",
            Name="Pane Rotation_Y",
            Get=(p)=>((Pane)p).Rotation.y,
            Set=(p,a)=>{((Pane)p).Rotation.y=a; }
        },
        new EventNode()
        {
            ID="PaneRot_Z",
            Name="Pane Rotation_Z",
            Get=(p)=>((Pane)p).Rotation.z,
            Set=(p,a)=>{((Pane)p).Rotation.z=a; }
        }
    };

    public Pane DeepClone()
    {
        Pane clone = new Pane() { 
            Name=Name,
            Group=Group,
            Position=new Vector3(Position.x,Position.y, Position.z),
            Rotation=new Vector3(Rotation.x,Rotation.y,Rotation.z),
            Width=Width,
            Height=Height,
            Shape=Shape,
            StoryBoard=StoryBoard.DeepClone(),
        };
        foreach(JudgeLine line in Lines) clone.Lines.Add(line.DeepClone());
       
        return clone;
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