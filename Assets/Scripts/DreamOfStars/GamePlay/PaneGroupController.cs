using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PaneGroupController : MonoBehaviour
{
    public PaneGroup CurrentGroup;
    public PaneGroupController ParentGroup; // Ϊ�����ޣ������ж��parent

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
        var main = GameManager.MainInstance;
        if (main.IsPlaying)
        {
            CurrentGroup.Update(main.CurrentTime + main.VisualOffset);
            transform.localPosition = CurrentGroup.Position;
            transform.localEulerAngles = CurrentGroup.Rotation;
            
        }
    }

    public void SetGroup(PaneGroup group)
    {
        var mainTimer = GameManager.MainInstance.Song.Timer;
        foreach (TimeNode tn in group.StoryBoard.TimeNodes)
        {
            tn.Duration =mainTimer.BeatToSec(tn.Offset+tn.Duration);
            tn.Offset = mainTimer.BeatToSec(tn.Offset);
            tn.Duration -= tn.Offset;
        }
        CurrentGroup = group;
    }
    
}

/// <summary>
/// ���ƽ�����һ��ƽ���飬��ƽ����������Խ��б仯
/// </summary>
[System.Serializable]
public class PaneGroup : AStoryBoard, IDeepCloneable<PaneGroup>
{
    public string Name; // TODO�� ��ֹ������ͬ��Name������������parent��ʱ���������
    public string Group; // ��������
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
        PaneGroup clone = new PaneGroup() {
            Name = Name,
            Group = Group,
            Position = new Vector3(Position.x, Position.y, Position.z),
            Rotation = new Vector3(Rotation.x, Rotation.y, Rotation.z),
            StoryBoard = StoryBoard.DeepClone()
        };

        return clone;
    }
}

public class PaneGroupManager
{
    public PaneGroup CurrentGroup;
    public Vector3 FinalPosition;
    public Quaternion FinalRotation;
    public bool isDirty;

    public PaneGroupManager(PaneGroup group)
    {
        CurrentGroup = group;
        isDirty = true;
    }

    public void Get(ref Vector3 pos, ref Quaternion rot)
    {
        pos = FinalRotation * pos + FinalPosition;
        rot = FinalRotation * rot;
    }

    public void Update(ChartManager chartMan,string original = null)
    {
        FinalPosition=CurrentGroup.Position;
        FinalRotation = Quaternion.Euler(CurrentGroup.Rotation);
        if (original == null) original = CurrentGroup.Name; // Ĭ��Ϊ��ǰ��
        if(!string.IsNullOrEmpty(CurrentGroup.Name) ) {
            PaneGroupManager group = chartMan.Groups[CurrentGroup.Name];
        }

    }
}
