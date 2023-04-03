using STRlantian.Gameplay.Note;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum NoteType
{
    Tap,
    Drag,
    Hold,
    Flick
}

[System.Serializable]
public class NoteObject:AStoryBoard, IDeepCloneable<NoteObject>
{
    public NoteType noteType;
    public float Offset = 0;
    
    // 位置和大小控制
    /// <summary>
    /// 视为距离判定线最左端顶点位置，归一化
    /// </summary>
    public float Position;
    /// <summary>
    /// Note 长度，归一化，1为判定线长度
    /// </summary>
    public float Length;


    public new static EventNode[] EventNodes =
    {
        new EventNode
        {
            ID = "NotePosition",
            Name = "Position",
            Get = (n) => ((NoteObject)n).Position,
            Set = (n,a) => {((NoteObject)n).Position=a; }
        },
         new EventNode
        {
            ID = "NoteLength",
            Name = "Length",
            Get = (n) => ((NoteObject)n).Length,
            Set = (n,a) => {((NoteObject)n).Length=a; }
        }
    };

        
    public NoteObject DeepClone() {
        // TODO：还没写
        return this;
    }
}
