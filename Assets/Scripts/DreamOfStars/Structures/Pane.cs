using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pane : AStoryBoard, IDeepCloneable<Pane>
{
    /// <summary>
    /// Pane 的名称
    /// </summary>
    public string Name = "New Pane";
    /// <summary>
    /// Pane 所属组，为空则不属于任何一个组
    /// </summary>
    public string Group;

    /// <summary>
    /// Pane 的位置
    /// </summary>
    public Vector3 Position;
    /// <summary>
    /// Pane 的旋转角度
    /// </summary>
    public Vector3 Rotation;

    /// <summary>
    /// Pane 的宽度
    /// 目前仅完成正方形，TODO：更多形状
    /// </summary>
    public float Width = 1.0f;
    /// <summary>
    /// Pane 的高度
    /// 目前仅完成正方形，TODO：更多形状
    /// </summary>
    public float Height = 1.0f;

    /// <summary>
    /// Pane 上拥有的判定线
    /// </summary>
    public List<JudgeLine> Lines = new List<JudgeLine>();

    public PaneShape Shape = PaneShape.Rectangle;

    /// <summary>
    /// 用于判断是否正在形变，避免不必要的计算
    /// </summary>
    public bool IsDeforming;

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
        },
        // 只是简单的拉长缩短pane
        // TODO：以后改成顶点控制
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
    Rectangle, // 长方形，含正方形
    Triangle   // 三角形，真的有必要搞这种谱面吗hhh
}
