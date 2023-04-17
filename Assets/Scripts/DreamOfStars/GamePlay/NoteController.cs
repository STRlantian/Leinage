using STRlantian.Gameplay.Note;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    public Note CurrentNote;
    public JudgeLineController CurrentJudgeLine;

    [Header("Hold")]
    public List<float> ticks = new List<float>(); // 长条判定
    public float currentHoldOffset;
    public float currentHoldPos;

    [HideInInspector]
    public bool isHit, isPreHit, isFlicked;
    public float NoteWeight; // 分数权重

    public void InitNote(JudgeLineController line, Note note)
    {
        Metronome timer = GameManager.MainInstance.Song.Timer;

        // Hold 判定
        if(note.noteType==NoteType.HOLD && note.holdLength > 0)
        {
            for(float t=.5f; t< note.holdLength; t += .5f) {
                // 每半拍一个长条判定
                ticks.Add(timer.BeatToSec(note.offset + t));
            }
            ticks.Add(timer.BeatToSec(note.offset + note.holdLength)); // 尾判

            float noteSec = timer.BeatToSec(note.offset);
            //for(int i =1; i<)
        }

        // 如果不是 hold 则将所有对应的 beat 转换成 second
        foreach (TimeNode tn in note.StoryBoard.TimeNodes)
        {
            tn.Duration = timer.BeatToSec(tn.Offset + tn.Duration);
            tn.Offset = timer.BeatToSec(tn.Offset);
            tn.Duration -= tn.Offset;
        }
        note.offset = timer.BeatToSec(note.offset);

        // Flick 判定
        if (note.noteType == NoteType.FLICK)
        {
            // TODO: 没写完
        }

        // 乱写的testcode
        if(note.noteType==NoteType.TAP)
        {
            transform.AddComponent<MeshFilter>().mesh=makeTAPMesh(line.CurrentLine);
            transform.AddComponent<MeshRenderer>().material = makeTAPMaterial(Color.white, Color.yellow, Color.blue);
            // TODO: 根据 offset 计算位置
            // TODO：根据 pane 的法向量和 line 的方向向量 计算下落方向
            //transform.localPosition = new Vector3
        }


        isFlicked = note.noteType == NoteType.FLICK;


        CurrentJudgeLine = line;
        CurrentNote = note;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.MainInstance.IsPlaying)
        {
            if (isHit && isFlicked) { }
            // TODO: Judgeline 形变的时候 一起形变
        }
    }

    private Mesh makeTAPMesh(JudgeLine l)
    {
        Mesh tapMesh = new Mesh();
        // 默认 note 宽度 TODO：以后可以自定义
        float height = .2f;
        List<Vector3> vertices = new List<Vector3>
        {
             // 把pivot放到几何中心
            new Vector3(-l.lineLength/2, -height/2),
            new Vector3(l.lineLength/2, -height/2),
            new Vector3(l.lineLength/2, height/2),
            new Vector3(-l.lineLength/2,height/2)
        };
        List<int> triangles = new List<int>
        {
                0,2,1,
                0,3,2
        };
        // TODO: 完善UV
        List<Vector2> uv = new List<Vector2>() {
                    Vector2.zero,
                    Vector2.zero,
                    Vector2.zero,
                    Vector2.zero
        };
        tapMesh.Clear();
        tapMesh.vertices = vertices.ToArray();
        tapMesh.triangles = triangles.ToArray();
        tapMesh.uv = uv.ToArray();
        tapMesh.RecalculateNormals();
        tapMesh.RecalculateBounds();

        return tapMesh;
    }

    private Material makeTAPMaterial(Color mainColor, Color specColor, Color? emissionColor = null)
    {
        Material tapMaterial = new Material(Shader.Find("PaneStatic"));

        tapMaterial.SetColor("_Color", mainColor);
        tapMaterial.SetColor("_SpecColor", specColor);
        if (emissionColor != null) tapMaterial.SetColor("_Emission", (Color)emissionColor);

        return tapMaterial;
    }

    public static float GetAccuracy(float offset)
    {
        float perfect = GameManager.MainInstance.PerfectWindow;
        float good = GameManager.MainInstance.GoodWindow;
        float absDist=Mathf.Abs(offset)*1000;
        if (absDist < perfect) return 0;
        return (absDist - perfect) / (good - perfect) * Mathf.Sign(offset);
    }
}

