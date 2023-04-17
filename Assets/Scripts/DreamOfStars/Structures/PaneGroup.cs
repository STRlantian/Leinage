using UnityEngine;
/// <summary>
/// 多个平面组成一个平面组，该平面组整体可以进行变化
/// </summary>
[System.Serializable]
public class PaneGroup : AStoryBoard, IDeepCloneable<PaneGroup>
{
    public string Name; // TODO： 禁止出现相同的Name，否则在设置parent的时候会有问题
    public string Group; // 用于套娃
    public Vector3 Position;
    public Vector3 Rotation;


    public new static EventNode[] EventNodes =
    {
        new EventNode
        {
            ID="GroupPos_X",
            Name="Group Position X",
            Get=(g)=>((PaneGroup)g).Position.x,
            Set=(g,a)=>{((PaneGroup)g).Position.x=a; }
        },
        new EventNode
        {
            ID="GroupPos_Y",
            Name="Group Position Y",
            Get=(g)=>((PaneGroup)g).Position.y,
            Set=(g,a)=>{((PaneGroup)g).Position.y=a; }
        },
        new EventNode
        {
            ID="GroupPos_Z",
            Name="Group Position Z",
            Get=(g)=>((PaneGroup)g).Position.z,
            Set=(g,a)=>{((PaneGroup)g).Position.z=a; }
        },
        new EventNode
        {
            ID="GroupRot_X",
            Name="Group Rotation X",
            Get=(g)=>((PaneGroup)g).Rotation.x,
            Set=(g,a)=>{((PaneGroup)g).Rotation.x=a; }
        },
        new EventNode
        {
            ID="GroupRot_Y",
            Name="Group Rotation Y",
            Get=(g)=>((PaneGroup)g).Rotation.y,
            Set=(g,a)=>{((PaneGroup)g).Rotation.y=a; }
        },
        new EventNode
        {
            ID="GroupRot_Z",
            Name="Group Rotation Z",
            Get=(g)=>((PaneGroup)g).Rotation.z,
            Set=(g,a)=>{((PaneGroup)g).Rotation.z=a; }
        }
    };

    public PaneGroup DeepClone()
    {
        PaneGroup clone = new PaneGroup()
        {
            Name = Name,
            Group = Group,
            Position = new Vector3(Position.x, Position.y, Position.z),
            Rotation = new Vector3(Rotation.x, Rotation.y, Rotation.z),
            StoryBoard = StoryBoard.DeepClone()
        };

        return clone;
    }
}