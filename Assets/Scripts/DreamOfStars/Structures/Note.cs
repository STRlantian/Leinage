using STRlantian.Gameplay.Note;
using UnityEngine;

[System.Serializable]
public class Note : AStoryBoard, IDeepCloneable<Note>
{
    public NoteType noteType;
    /// <summary>
    /// Note 对应的 Beat
    /// </summary>
    public float offset = 0;
    /// <summary>
    /// 视为距离判定线最左端顶点位置，归一化
    /// </summary>
    public float position;
    /// <summary>
    /// Note 长度，归一化，1为判定线长度
    /// </summary>
    public float length;
    /// <summary>
    /// 移动的方向
    /// </summary>
    public Vector3 direction;
    /// <summary>
    /// 长条的长度
    /// </summary>
    public float holdLength = 0;
    /// <summary>
    /// 滑动的方向
    /// </summary>
    public float flickDirection = -1;


    public new static EventNode[] EventNodes =
    {
        new EventNode
        {
            ID = "NotePosition",
            Name = "Position",
            Get = (n) => ((Note)n).position,
            Set = (n,a) => {((Note)n).position=a; }
        },
         new EventNode
        {
            ID = "NoteLength",
            Name = "Length",
            Get = (n) => ((Note)n).length,
            Set = (n,a) => {((Note)n).length=a; }
        }
    };


    public Note DeepClone()
    {
        Note clone = new Note()
        {
            noteType = noteType,
            offset = offset,
            position = position,
            direction = direction,
            length = length,
            holdLength = holdLength,
            flickDirection = flickDirection,
            StoryBoard = StoryBoard.DeepClone()
        };
        return clone;
    }
}
