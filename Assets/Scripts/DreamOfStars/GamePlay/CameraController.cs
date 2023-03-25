using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[System.Serializable]
public class CameraController : AStoryBoard, IDeepCloneable<CameraController>
{
    /// <summary>
    /// 相机中心点
    /// </summary>
    public Vector3 CameraPivot;
    /// <summary>
    /// 相机旋转角度
    /// </summary>
    public Vector3 CameraRotation;
    /// <summary>
    /// 中心点距离，正数为向后移动，负数为向前移动
    /// </summary>
    public float PivotDistance = 0f;


    public new static EventNode[] EventNodes =
    {
        new EventNode{
            ID="CameraPivot_X",
            Name="Camera Pivot X",
            Get=(x)=>((CameraController)x).CameraPivot.x,
            Set=(x,a)=>{((CameraController)x).CameraPivot.x=a; },
            },
        new EventNode{
            ID="CameraPivot_Y",
            Name="Camera Pivot Y",
            Get=(x)=>((CameraController)x).CameraPivot.y,
            Set=(x,a)=>{((CameraController)x).CameraPivot.y=a; },
        },
        new EventNode
        {
            ID="CameraPivot_Z",
            Name="Camera Pivot Z",
            Get=(x)=>((CameraController)x).CameraPivot.z,
            Set=(x,a)=>{((CameraController)x).CameraPivot.z=a; },
        },
        new EventNode
        {
            ID="CameraPivotDistance",
            Name="Camera Pivot Distance",
            Get=(x)=>((CameraController)x).PivotDistance,
            Set=(x,a)=>{((CameraController)x).PivotDistance=a; },
        },
        new EventNode
        {
            ID = "CameraRotation_X",
            Name = "Camera Rotation X",
            Get = (x) => ((CameraController)x).CameraRotation.x,
            Set = (x, a) => { ((CameraController)x).CameraRotation.x = a; },
        },
        new EventNode {
            ID = "CameraRotation_Y",
            Name = "Camera Rotation Y",
            Get = (x) => ((CameraController)x).CameraRotation.y,
            Set = (x, a) => { ((CameraController)x).CameraRotation.y = a; },
        },
        new EventNode
        {
            ID = "CameraRotation_Z",
            Name = "Camera Rotation Z",
            Get = (x) => ((CameraController)x).CameraRotation.z,
            Set = (x, a) => { ((CameraController)x).CameraRotation.z = a; },
        }


    };

    // TODO: 更多的内置缓动类型，可用上面的方法实现，如ShakeCamera等

    // TODO: 保持所选物体始终处于相机视野，需要更多计算，好难

    // TODO: 更多简单的相机控制，如相机置中等
    // BUG: 置中可能有点小问题晚点修，Canvas中心和世界坐标的转化！
    public void SetCameraCenter(float time,float duration,float pivotDistance,EaseFunction easeFunc=EaseFunction.Linear,EaseMode easeMode=EaseMode.InOut)
    {
        List<TimeNode> nodes = new List<TimeNode>()
        {
            new TimeNode()
            {
                ID="CameraPivot_X",
                Offset = time,
                Duration=duration,
                To=0,
                EaseFunc=easeFunc,
                EaseMode=easeMode,
            },
            new TimeNode()
            {
                ID="CameraPivot_Y",
                Offset = time,
                Duration=duration,
                To=0,
                EaseFunc=easeFunc,
                EaseMode=easeMode,
            },
            new TimeNode()
            {
                ID="CameraPivot_Z",
                Offset = time,
                Duration=duration,
                To=0,
                EaseFunc=easeFunc,
                EaseMode=easeMode,
            },
            new TimeNode()
            {
                ID="CameraRotation_X",
                Offset = time,
                Duration=duration,
                To=0,
                EaseFunc=easeFunc,
                EaseMode=easeMode,
            },
            new TimeNode()
            {
                ID="CameraRotation_Y",
                Offset = time,
                Duration=duration,
                To=0,
                EaseFunc=easeFunc,
                EaseMode=easeMode,
            },
            new TimeNode()
            {
                ID="CameraRotation_Z",
                Offset = time,
                Duration=duration,
                To=0,
                EaseFunc=easeFunc,
                EaseMode=easeMode,
            },
            new TimeNode()
            {
                ID="CameraPivotDistance",
                Offset = time,
                Duration=duration,
                To=pivotDistance,
                EaseFunc=easeFunc,
                EaseMode=easeMode,
            },

        };
        foreach(TimeNode node in nodes) this.StoryBoard.Add(node);

    }

    public CameraController DeepClone()
    {
        CameraController clone = new CameraController()
        {
            CameraPivot = new Vector3(CameraPivot.x, CameraPivot.y, CameraPivot.z),
            CameraRotation = new Vector3(CameraRotation.x, CameraRotation.y, CameraRotation.z),
            PivotDistance = PivotDistance,
            StoryBoard = StoryBoard.DeepClone(),
        };
        return clone;
    }

}
