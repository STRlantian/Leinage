using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pane : AStoryBoard, IDeepCloneable<Pane>
{
    /// <summary>
    /// Pane ������
    /// </summary>
    public string Name = "New Pane";
    /// <summary>
    /// Pane �����飬Ϊ���������κ�һ����
    /// </summary>
    public string Group;

    /// <summary>
    /// Pane ��λ��
    /// </summary>
    public Vector3 Position;
    /// <summary>
    /// Pane ����ת�Ƕ�
    /// </summary>
    public Vector3 Rotation;

    /// <summary>
    /// Pane �Ŀ��
    /// Ŀǰ����������Σ�TODO��������״
    /// </summary>
    public float Width = 1.0f;
    /// <summary>
    /// Pane �ĸ߶�
    /// Ŀǰ����������Σ�TODO��������״
    /// </summary>
    public float Height = 1.0f;

    /// <summary>
    /// Pane ��ӵ�е��ж���
    /// </summary>
    public List<JudgeLine> Lines = new List<JudgeLine>();

    public PaneShape Shape = PaneShape.Rectangle;

    /// <summary>
    /// �����ж��Ƿ������α䣬���ⲻ��Ҫ�ļ���
    /// </summary>
    public bool IsDeforming;

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
        },
        // ֻ�Ǽ򵥵���������pane
        // TODO���Ժ�ĳɶ������
        new EventNode()
        {
            ID="PaneWidth",
            Name="Pane Width",
            Get=(p)=>((Pane)p).Width,
            Set=(p,a)=>{((Pane)p).Width=a;},
            OnProc=(p)=>{((Pane)p).IsDeforming=true; },
            OnFinish=(p)=>{((Pane)p).IsDeforming=false;}
        },
        new EventNode()
        {
            ID="PaneHeight",
            Name="Pane Height",
            Get=(p)=>((Pane)p).Height,
            Set=(p,a)=>{((Pane)p).Height=a;},
            OnProc=(p)=>{((Pane)p).IsDeforming=true; },
            OnFinish=(p)=>{((Pane)p).IsDeforming=false;}
        },

    };

    public Pane DeepClone()
    {
        Pane clone = new Pane()
        {
            Name = Name,
            Group = Group,
            Position = new Vector3(Position.x, Position.y, Position.z),
            Rotation = new Vector3(Rotation.x, Rotation.y, Rotation.z),
            Width = Width,
            Height = Height,
            Shape = Shape,
            IsDeforming = IsDeforming,
            StoryBoard = StoryBoard.DeepClone(),
        };
        foreach (JudgeLine line in Lines) clone.Lines.Add(line.DeepClone());

        return clone;
    }
}

[System.Serializable]
public enum PaneShape
{
    Rectangle, // �����Σ���������
    Triangle   // �����Σ�����б�Ҫ������������hhh
}
