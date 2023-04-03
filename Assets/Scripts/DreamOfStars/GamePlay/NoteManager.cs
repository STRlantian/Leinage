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
    
    // λ�úʹ�С����
    /// <summary>
    /// ��Ϊ�����ж�������˶���λ�ã���һ��
    /// </summary>
    public float Position;
    /// <summary>
    /// Note ���ȣ���һ����1Ϊ�ж��߳���
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
        // TODO����ûд
        return this;
    }
}
