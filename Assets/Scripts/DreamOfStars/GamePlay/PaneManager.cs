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


    void Awake() // Awake����Start
    {
        
    }

    void Start()
    {
        // TODO: �����һ��PaneGroupController��ͳһ����
        #region testCode
        float cubeSize = 200f;
        // TODO: ����ĳ���������bug����û���ü�ϸ��
        float[,] position3D = new float[,] { { -cubeSize / 2, 0, 0}, { cubeSize / 2, 0, 0 }, { 0, cubeSize / 2, 0 }, {  0,-cubeSize / 2,0 }, { 0, 0, -cubeSize / 2 }, { 0, 0, cubeSize / 2 } }; // ��ʼλ������
        float[,] rotation3D = new float[,] { {  0, -90,0 }, { 0, 90,0 }, {  -90, 0,0 }, {  90, 0,0 }, {  180, 0, 0 }, {  0, 0, 0 } }; // ��ʼ��ת����
        string[] paneName = new string[] { "left", "right", "top", "bottom", "front", "back" }; // ��������

        // Ӳ�˽���
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

            // Ҫչʾ�Ķ�Ч(x
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
    Rectangle, // �����Σ���������
    Triangle   // �����Σ�����б�Ҫ������������hhh
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
                //TODO: ֻд�˳����ε�
                // ���ĸ�������ܻ��ֳ�������������
                // v3 ----- v2
                // |      / |
                // |    /   |
                // |  /     |
                // v0 ----- v1
                // ע��˳�򣬲�Ȼ���߿��ܻ����
                // TODO: ���һ����ֻ�����������Σ���ôû�����α������
                Vertices = new List<Vector3>
                {
                    // ���������Ķ���ᵼ�¿��Ƶ���0 0 0��
                    //new Vector3(0, 0, 0),
                    //new Vector3(Width,0,0),
                    //new Vector3(Width,Height,0),
                    //new Vector3(0,Height,0)
                    // ��pivot�ŵ���������
                    new Vector3(-Width/2, -Height/2),
                    new Vector3(Width/2, -Height/2),
                    new Vector3(Width/2, Height/2),
                    new Vector3(-Width/2,Height/2)
                };
                Triangles = new List<int>
                {
                    // Ӧ�ð�˳ʱ������
                    0,2,1,
                    0,3,2
                };
                // TODO: ����UV
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
            // TODO: ��ɫ����
            mat.SetColor("_Color",Color.yellow);
            mat.SetColor("_SpecColor",Color.yellow);
            //mat.SetColor("_Emission", Color.blue); // û�ӳ�����Դ�����Ը���Է�����
            PaneMaterial = mat;
            PaneObject.AddComponent<MeshRenderer>();
        }
        PaneObject.GetComponent<MeshRenderer>().material = PaneMaterial;
    }

    /// <summary>
    /// ����˸
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
        // TODO: ��ûд
        return this;
    }
}
