using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PaneController))]
public class JudgeLineController : MonoBehaviour
{
    public JudgeLine CurrentLine;
    public LineRenderer CurrentLineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var main = GameManager.MainInstance;
        if (main.IsPlaying)
        {
            for (int i = 0; i < CurrentLine.Vertices.Count; i++)
            {
                CurrentLineRenderer.SetPosition(i, transform.TransformPoint(transform.GetComponent<MeshFilter>().mesh.vertices[CurrentLine.Vertices[i]]));
            }
        }
    }

    public void InitJudgeLine(JudgeLine l)
    {
        CurrentLine = l;
        CurrentLineRenderer = new GameObject(l.Name).AddComponent<LineRenderer>();
        CurrentLineRenderer.transform.parent = transform;
        CurrentLineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        CurrentLineRenderer.positionCount = CurrentLine.Vertices.Count;
        CurrentLineRenderer.startColor = Color.white;
        CurrentLineRenderer.endColor = Color.white;
        CurrentLineRenderer.startWidth = 10.0f;
        CurrentLineRenderer.endWidth = 10.0f;
        for(int i = 0; i < CurrentLine.Vertices.Count; i++) {
            CurrentLineRenderer.SetPosition(i, transform.GetComponent<MeshFilter>().mesh.vertices[CurrentLine.Vertices[i]]);
        }

    }
}


/// <summary>
/// �ж��ߣ���������Ϊ�ж��ߺ�note��
/// </summary>
[System.Serializable]
public class JudgeLine : AStoryBoard, IDeepCloneable<JudgeLine>
{
    public string Name = "Judge Line";

    public List<NoteObject> Notes = new List<NoteObject>();

    // ���ж���ѡȡ����Ķ�������
    // TODO: �Ӹ�����Խ���жϣ���һ����ʦ��д��
    public List<int> Vertices = new List<int>();


    public JudgeLine DeepClone()
    {
        // TODO: ��ûд
        return this;
    }
}
