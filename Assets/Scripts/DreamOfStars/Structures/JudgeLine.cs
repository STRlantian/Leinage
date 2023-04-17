using System.Collections.Generic;
/// <summary>
/// �ж��ߣ���������Ϊ�ж��ߺ�note��
/// </summary>
[System.Serializable]
public class JudgeLine : AStoryBoard, IDeepCloneable<JudgeLine>
{
    /// <summary>
    /// �ж�������
    /// </summary>
    public string name = "Judge Line";

    /// <summary>
    /// �ж����ϵ����� note
    /// </summary>
    public List<Note> notes = new List<Note>();

    // ���ж���ѡȡ����Ķ�������
    // TODO: �Ӹ�����Խ���жϣ���һ����ʦ��д��
    public List<int> vertices = new List<int>();

    public float lineLength;

    // TODO: �����ж�������
    // �Ƿ���ʾ���ж���
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