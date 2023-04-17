using System.Collections.Generic;
/// <summary>
/// 判定线，初步定义为判定线和note绑定
/// </summary>
[System.Serializable]
public class JudgeLine : AStoryBoard, IDeepCloneable<JudgeLine>
{
    /// <summary>
    /// 判定线名称
    /// </summary>
    public string name = "Judge Line";

    /// <summary>
    /// 判定线上的所有 note
    /// </summary>
    public List<Note> notes = new List<Note>();

    // 该判定线选取的面的顶点索引
    // TODO: 加个数组越界判断，万一有谱师乱写？
    public List<int> vertices = new List<int>();

    public float lineLength;

    // TODO: 控制判定线显隐
    // 是否显示该判定线
    public bool isActive;

    public new static EventNode[] EventNodes = { };

    public JudgeLine DeepClone()
    {
        JudgeLine clone = new JudgeLine()
        {
            name = name,
            lineLength = lineLength,
            StoryBoard = StoryBoard.DeepClone(),
        };
        foreach (int i in vertices) clone.vertices.Add(i);
        foreach (Note note in notes) clone.notes.Add(note);

        return clone;
    }
}