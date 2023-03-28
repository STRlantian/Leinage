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


    void Awake() // Awake����Start
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
            //TODO: ֻд�˳����ε�
            // ���ĸ�������ܻ��ֳ�������������
            // v3 ----- v2
            // |      / |
            // |    /   |
            // |  /     |
            // v0 ----- v1
            // ע��˳�򣬲�Ȼ���߿��ܻ����
            // TODO: ���һ����ֻ�����������Σ���ôû�����α������
            List<Vector3> vertices = new List<Vector3>
                {
                    // ���������Ķ���ᵼ�¿��Ƶ���0 0 0��
                    //new Vector3(0, 0, 0),
                    //new Vector3(Width,0,0),
                    //new Vector3(Width,Height,0),
                    //new Vector3(0,Height,0)
                    // ��pivot�ŵ���������
                    new Vector3(-p.Width/2, -p.Height/2),
                    new Vector3(p.Width/2, -p.Height/2),
                    new Vector3(p.Width/2, p.Height/2),
                    new Vector3(-p.Width/2,p.Height/2)
                };
            List<int> triangles = new List<int>
                {
                    // ��˳ʱ������
                    0,2,1,
                    0,3,2
                };
            // TODO: ����UV
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
    /// ������Ĳ���
    /// </summary>
    /// <param name="mainColor">��Ҫ��ɫ</param>
    /// <param name="specColor">��������ɫ</param>
    /// <param name="emissionColor">�Է�����ɫ</param>
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
    /// ������Դ
    /// </summary>
    public void Dispose()
    {
        if(CurrentMesh!=null) MonoBehaviour.DestroyImmediate(CurrentMesh);
    }
}

[System.Serializable]
public enum PaneShape
{
    Rectangle, // �����Σ���������
    Triangle   // �����Σ�����б�Ҫ������������hhh
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
    
    // 3.28 ���£�Paneֻ����Ϊ���ݽṹ�����ڴ�new GameObject�����ˣ�Object��Mesh�Ĺ�����PaneController


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
///// ��������Pane���α�
///// </summary>
//[System.Serializable]
//public class Pane???? : AStoryBoard, IDeepCloneable<Pane????> {
//    public float Offset;

//    // ������������ê�㣬pΪPane��Position���������λ�þ���Vector2��ʾ
//    // v4 ----- v5
//    // |        |
//    // v2   p   v3
//    // |        |
//    // v0 ----- v1
//    public Vector2 v0;
//    public Vector2 v1;

   

//}