using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PaneManager : MonoBehaviour
{
    public Pane CurrentPane;
    public PaneGroupController ParentGroup;

    //public MeshFilter PaneMeshSample;
    public List<Pane> PaneList;
    //public List<MeshFilter> PaneMeshes;

    //public List<float> ScrollPositions;


    void Awake() // Awake早于Start
    {
        
    }

    void Start()
    {
        // TODO: 分离出一个PaneGroupController来统一管理
        #region testCode
        float cubeSize = 200f;
        // TODO: 这里的常量可能有bug，还没来得及细看
        float[,] position3D = new float[,] { { -cubeSize / 2, 0, 0}, { cubeSize / 2, 0, 0 }, { 0, cubeSize / 2, 0 }, {  0,-cubeSize / 2,0 }, { 0, 0, -cubeSize / 2 }, { 0, 0, cubeSize / 2 } }; // 初始位置数组
        float[,] rotation3D = new float[,] { {  0, -90,0 }, { 0, 90,0 }, {  -90, 0,0 }, {  90, 0,0 }, {  180, 0, 0 }, {  0, 0, 0 } }; // 初始旋转方向
        string[] paneName = new string[] { "left", "right", "top", "bottom", "front", "back" }; // 各面名称

        // 硬核建面
        for(int i = 0; i < 6; i++)
        {
            PaneList.Add(new Pane()
            {
                Position = new Vector3(position3D[i, 0], position3D[i, 1], position3D[i, 2]),
                Rotation = new Vector3(rotation3D[i, 0], rotation3D[i, 1], rotation3D[i, 2]),
                Width=cubeSize,
                Height=cubeSize,
                Name= paneName[i],
            });
            ;
            PaneList[i].InitPane();

            // 要展示的动效(x
            PaneList[i].StoryBoard.Add(new TimeNode()
            {
                ID = "PanePos_Z",
                To = 0,
                Offset = 5,
                Duration = 5f,
                EaseFunc=EaseFunction.Cubic,
                EaseMode=EaseMode.InOut
            });
            PaneList[i].StoryBoard.Add(new TimeNode()
            {
                ID = "PaneRot_Z",
                To = 0,
                Offset = 5,
                Duration = 5f
            });
            PaneList[i].StoryBoard.Add(new TimeNode()
            {
                ID = "PaneRot_X",
                To = 0,
                Offset = 5,
                Duration = 5f
            });
            PaneList[i].StoryBoard.Add(new TimeNode()
            {
                ID = "PaneRot_Y",
                To = 0,
                Offset = 5,
                Duration = 5f
            });
        }
        #endregion

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.MainInstance.IsPlaying)
        {
            foreach (Pane p in PaneList)
            {
                p.Update(GameManager.MainInstance.CurrentTime);
                p.PaneObject.transform.localPosition = p.Position;
                p.PaneObject.transform.localEulerAngles = p.Rotation;
            }
        }
    }

}


public enum PaneShape
{
    Rectangle, // 长方形，含正方形
    Triangle   // 三角形，真的有必要搞这种谱面吗hhh
}

[System.Serializable]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Pane : AStoryBoard, IDeepCloneable<Pane>
{
    public GameObject PaneObject;
    public string Name="New Pane";

    public Vector3 Position;
    public Vector3 Rotation;
    public float Width=1.0f;
    public float Height=1.0f;

    public Mesh PaneMesh;
    private List<Vector3> Vertices;
    private List<Vector2> UVs;
    private List<int> Triangles;

    public Material PaneMaterial;

    public PaneShape Shape=PaneShape.Rectangle;
    
    public void InitPane()
    {
        if (PaneObject == null)
        {   
            PaneObject=new GameObject(Name);
            PaneObject.transform.parent = GameManager.MainInstance.PaneContainer.transform;
            MakeMesh();
            MakeMaterial();


            PaneObject.transform.localPosition = Position;
            PaneObject.transform.localEulerAngles = Rotation;
            
        }
    }

    public void MakeMesh()
    {
        if (PaneMesh == null)
        {
            PaneMesh = new Mesh();
            if (Shape == PaneShape.Rectangle)
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
                Vertices = new List<Vector3>
                {
                    // 下面这样的顶点会导致控制点在0 0 0处
                    //new Vector3(0, 0, 0),
                    //new Vector3(Width,0,0),
                    //new Vector3(Width,Height,0),
                    //new Vector3(0,Height,0)
                    // 把pivot放到几何中心
                    new Vector3(-Width/2, -Height/2),
                    new Vector3(Width/2, -Height/2),
                    new Vector3(Width/2, Height/2),
                    new Vector3(-Width/2,Height/2)
                };
                Triangles = new List<int>
                {
                    // 应该按顺时针连接
                    0,2,1,
                    0,3,2
                };
                // TODO: 完善UV
                UVs=new List<Vector2>() { 
                    Vector2.zero,
                    Vector2.zero,
                    Vector2.zero,
                    Vector2.zero
                };
                PaneMesh.Clear();
                PaneMesh.vertices = Vertices.ToArray();
                PaneMesh.triangles = Triangles.ToArray();
                PaneMesh.uv = UVs.ToArray();
                PaneMesh.RecalculateNormals();
            }
            PaneObject.AddComponent<MeshFilter>();
        }

        PaneObject.GetComponent<MeshFilter>().mesh = PaneMesh;
    }

    public void MakeMaterial()
    {
        if(PaneMaterial== null)
        {

            Material mat = new Material(Shader.Find("PaneStatic"));
            // TODO: 颜色设置
            mat.SetColor("_Color",Color.yellow);
            mat.SetColor("_SpecColor",Color.yellow);
            //mat.SetColor("_Emission", Color.blue); // 没加场景光源，所以搞个自发光先
            PaneMaterial = mat;
            PaneObject.AddComponent<MeshRenderer>();
        }
        PaneObject.GetComponent<MeshRenderer>().material = PaneMaterial;
    }

    /// <summary>
    /// 面闪烁
    /// </summary>
    public void StartFlash()
    {
        PaneMaterial = new Material(Shader.Find("PaneFlash"));
        PaneObject.GetComponent <MeshRenderer>().material = PaneMaterial;
    }
    
    public void EndFlash()
    {
        PaneMaterial = new Material(Shader.Find("PaneStatic"));
        PaneObject.GetComponent<MeshRenderer>().material = PaneMaterial;
    }

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
            StoryBoard=StoryBoard.DeepClone(),
        };
        // TODO: 还没写
        return this;
    }
}
