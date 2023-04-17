using STRlantian.Gameplay.Note;
using UnityEngine;

[System.Serializable]
public class Note : AStoryBoard, IDeepCloneable<Note>
{
    public NoteType noteType;
    /// <summary>
    /// Note ��Ӧ�� Beat
    /// </summary>
    public float offset = 0;
    /// <summary>
    /// ��Ϊ�����ж�������˶���λ�ã���һ��
    /// </summary>
    public float position;
    /// <summary>
    /// Note ���ȣ���һ����1Ϊ�ж��߳���
    /// </summary>
    public float length;
    /// <summary>
    /// �ƶ��ķ���
    /// </summary>
    public Vector3 direction;
    /// <summary>
    /// �����ĳ���
    /// </summary>
    public float holdLength = 0;
    /// <summary>
    /// �����ķ���
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
