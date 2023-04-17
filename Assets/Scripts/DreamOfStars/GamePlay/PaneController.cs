using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PaneController : MonoBehaviour
{
    public Pane currentPane;
    public PaneGroupController parentGroup;
    

    void Awake() // Awake����Start
    {

    }

    public void InitPane(Pane p)
    {
        transform.AddComponent<MeshFilter>().mesh = MakePaneMesh(p);
        transform.AddComponent<MeshRenderer>().material = MakePaneMaterial(Color.white,Color.yellow);

        currentPane = p;
    }

    // Update is called once per frame
    void Update()
    {
        var main = GameManager.MainInstance;
        if (main.IsPlaying)
        {
            currentPane.Update(main.CurrentTime + main.VisualOffset);
            transform.localPosition = currentPane.Position;
            transform.localEulerAngles = currentPane.Rotation;
            if (currentPane.IsDeforming) // TODO: ��ô�ж����deforming
            {
                transform.GetComponent<MeshFilter>().mesh = MakePaneMesh(currentPane);
            }
        }
    }

    public Mesh MakePaneMesh(Pane p)
    {
        Mesh PaneMesh = new Mesh();

        if (p.Shape == PaneShape.Rectangle)
        {
            //TODO: ֻд�˳����ε�
            // ���ĸ�������ܻ��ֳ�������������
            // v3 ----- v2
            // |      / |
            // |    /   |
            // |  /     |
            // v0 ----- v1
            // ע��˳�򣬲�Ȼ���߿��ܻ����
            // TODO: ���һ����ֻ�����������Σ���ôû�����α������
            List<Vector3> vertices = new List<Vector3>
                {
                    // ���������Ķ���ᵼ�¿��Ƶ���0 0 0��
                    //new Vector3(0, 0, 0),
                    //new Vector3(Width,0,0),
                    //new Vector3(Width,Height,0),
                    //new Vector3(0,Height,0)
                    // ��pivot�ŵ���������
                    new Vector3(-p.Width/2, -p.Height/2),
                    new Vector3(p.Width/2, -p.Height/2),
                    new Vector3(p.Width/2, p.Height/2),
                    new Vector3(-p.Width/2,p.Height/2)
                };
            List<int> triangles = new List<int>
                {
                    // ��˳ʱ������
                    0,2,1,
                    0,3,2
                };
            // TODO: ����UV
            List<Vector2> uv = new List<Vector2>() {
                    Vector2.zero,
                    Vector2.zero,
                    Vector2.zero,
                    Vector2.zero
                };
            PaneMesh.Clear();
            PaneMesh.vertices = vertices.ToArray();
            PaneMesh.triangles = triangles.ToArray();
            PaneMesh.uv = uv.ToArray();
            PaneMesh.RecalculateNormals();
            PaneMesh.RecalculateBounds();
        }

        return PaneMesh;
    }

    /// <summary>
    /// ������Ĳ���
    /// </summary>
    /// <param name="mainColor">��Ҫ��ɫ</param>
    /// <param name="specColor">��������ɫ</param>
    /// <param name="emissionColor">�Է�����ɫ</param>
    /// <returns></returns>
    public Material MakePaneMaterial(Color mainColor, Color specColor, Color? emissionColor = null)
    {
        Material paneMaterial = new Material(Shader.Find("PaneStatic"));

        paneMaterial.SetColor("_Color", mainColor);
        paneMaterial.SetColor("_SpecColor", specColor);
        if(emissionColor != null) paneMaterial.SetColor("_Emission", (Color)emissionColor);

        return paneMaterial;
    }

}


public class PaneManager
{
    public Pane CurrentPane;
    public Mesh CurrentMesh = new Mesh();

    public float CurrentSpeed;
    public float CurrentScrollPosition;

    /// <summary>
    /// ������Դ
    /// </summary>
    public void Dispose()
    {
        if(CurrentMesh!=null) MonoBehaviour.DestroyImmediate(CurrentMesh);
    }
}




///// <summary>
///// ��������Pane���α�
///// </summary>
//[System.Serializable]
//public class Pane???? : AStoryBoard, IDeepCloneable<Pane????> {
//    public float Offset;

//    // ������������ê�㣬pΪPane��Position���������λ�þ���Vector2��ʾ
//    // v4 ----- v5
//    // |        |
//    // v2   p   v3
//    // |        |
//    // v0 ----- v1
//    public Vector2 v0;
//    public Vector2 v1;

   

//}