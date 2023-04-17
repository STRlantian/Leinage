using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class JudgeLineController : MonoBehaviour
{
    public JudgeLine CurrentLine;
    public LineRenderer CurrentLineRenderer;

    public bool isReady=false;

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
            for (int i = 0; i < CurrentLine.vertices.Count; i++)
            {
                CurrentLineRenderer.SetPosition(i, transform.TransformPoint(transform.parent.GetComponent<MeshFilter>().mesh.vertices[CurrentLine.vertices[i]]));
            }
        }
    }

    public void InitJudgeLine(JudgeLine l)
    {
        MakeLine(l);
        StartCoroutine(AddNote());
    }
    private void MakeLine(JudgeLine l)
    {
        CurrentLine = l;
        // ���� linerenderer ������
        CurrentLineRenderer = transform.gameObject.AddComponent<LineRenderer>();
        CurrentLineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        CurrentLineRenderer.positionCount = CurrentLine.vertices.Count;
        CurrentLineRenderer.startColor = Color.white;
        CurrentLineRenderer.endColor = Color.white;
        CurrentLineRenderer.startWidth = .1f;
        CurrentLineRenderer.endWidth = .1f;
        for (int i = 0; i < CurrentLine.vertices.Count; i++)
        {
            CurrentLineRenderer.SetPosition(i, transform.parent.GetComponent<MeshFilter>().mesh.vertices[CurrentLine.vertices[i]]);
        }

        // �����ж��߳��ȣ�ֱ��ȡ��β����Ӧ��û����ɣ���
        CurrentLine.lineLength = Vector3.Distance(CurrentLineRenderer.GetPosition(1),CurrentLineRenderer.GetPosition(0));
       
    }

    private IEnumerator AddNote()
    {
        foreach(Note note in CurrentLine.notes)
        {
            NoteController nc = new GameObject(note.noteType.ToString()).AddComponent<NoteController>();
            nc.transform.parent = CurrentLineRenderer.transform;
            nc.InitNote(this, note);
        }
        yield return null;

        // TODO: ����д����һ��������ȫ�� Note���Ժ�ʱ��д���μ��صĺ������Ż�

        isReady = true;
    }
}



