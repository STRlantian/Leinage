using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[System.Serializable]
public class StoryBoard : IDeepCloneable<StoryBoard>
{
    public List<TimeNode> TimeNodes = new List<TimeNode>();

    /// <summary>
    /// ��StoryBoard����һ��ʱ��ڵ�
    /// </summary>
    /// <param name="timeNode"></param>
    public void Add(TimeNode timeNode)
    {
        TimeNodes.Add(timeNode);
        TimeNodes.Sort((x,y)=>x.Offset.CompareTo(y.Offset)); // ��Ӻ���������
    }

    public List<TimeNode> FindAllByID(string id)
    {
        return TimeNodes.FindAll(x=>x.ID==id);
    }

    public TimeNode FindByID(string id)
    {
        return TimeNodes.Find(x=>x.ID==id);
    }

    public StoryBoard DeepClone()
    {
        StoryBoard clone=new StoryBoard();
        foreach(TimeNode tn in TimeNodes) clone.TimeNodes.Add(tn.DeepClone());
        return clone;
    }
   
}

// TODO: ��������ͬ�����¼����غ�ʱ�䲿��ʱ�������bug������1-3s����x��0��2-6s����x��10����Ӧ�ü����ж��׳��쳣

/// <summary>
/// StoryBoard�ĳ�����
/// �����ÿ�����EventNodes������StoryBoard
/// </summary>
public abstract class AStoryBoard
{
    public StoryBoard StoryBoard = new StoryBoard();
    public static EventNode[] EventNodes = { };

    private EventNode[] _eNode;

    /// <summary>
    /// ��Ը�����ʱ��time���ش�ʱ��Ӧ��AStoryBoard
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public AStoryBoard Get(float time)
    {
        if (_eNode == null) _eNode = (EventNode[])this.GetType().GetField("EventNodes").GetValue(null);
        AStoryBoard obj=(AStoryBoard)this.MemberwiseClone();   // ǳ����һ��
        foreach(EventNode eNode in _eNode) try
        {
                List<TimeNode> storyBoardTimeNode=StoryBoard.FindAllByID(eNode.ID); // �ҵ�EventNode��Ӧ��StoryBoard
                float value = eNode.Get(this);

                // ����Ѱ��timeʱ��֮�����һ��ʱ��ڵ�
                foreach(TimeNode tNode in storyBoardTimeNode)
                {
                    if (time >= tNode.Offset + tNode.Duration) value = tNode.To; // time�Ѿ�������ǰTimeNodeָʾʱ�䷶Χ������TimeNode���¼��ѽ�����ֵ��Ϊ����ʱ״̬
                    else if (time > tNode.Offset)
                    {
                        if (!float.IsNaN(tNode.From)) value = (float)tNode.From; // ��From���From��ʼ�����ֵ
                        // ���û���
                        Ease ease = Ease.EasesFuncDict[tNode.EaseFunc];
                        Func<float, float> func = ease.InOut; // Ĭ��
                        if (tNode.EaseMode==EaseMode.In) func = ease.In;
                        else if(tNode.EaseMode==EaseMode.Out) func = ease.Out;
                        // �ò�ֵ���㵱ǰֵ
                        value = Mathf.LerpUnclamped(value, tNode.To, func((time - tNode.Offset) / tNode.Duration));
                        break;
                    }
                    else break;
                }

                eNode.Set(obj, value);
                    
        }catch(System.Exception e)
            {
                Debug.LogError(e.Message); //TODO: ����ϸ���쳣˵��
            }
        return obj;
    }

    /// <summary>
    /// ���ݵ�ǰʱ��currentTime��ָ��time����StoryBoard
    /// </summary>
    float currentTime;
    Dictionary<string, float> currentValues;
    public void Update(float time)
    {
        if (_eNode == null) _eNode = (EventNode[])this.GetType().GetField("EventNodes").GetValue(null);
        if (currentValues == null)
        {
            // �������Event
            currentValues = new Dictionary<string, float>();
            foreach(EventNode eNode in _eNode)
            {
                currentValues.Add(eNode.ID, eNode.Get(this));
            }
        }
        foreach(EventNode eNode in _eNode) try
            {
                float value = currentValues[eNode.ID];
                while (true)
                {
                    TimeNode tNode = StoryBoard.FindByID(eNode.ID);
                    if (tNode == null || (time < tNode.Offset && currentTime < tNode.Offset)) break; // ��������δ������ʱ��ڵ㣬ֱ������
                    else if (time < tNode.Offset + tNode.Duration)
                    {
                        // ���ô���һ��ʱ��ڵ���¼�������
                        if (!float.IsNaN(tNode.From)) currentValues[eNode.ID]=value=(float)tNode.From;
                        Ease ease = Ease.EasesFuncDict[tNode.EaseFunc];
                        Func<float, float> func = ease.InOut;
                        if (tNode.EaseMode == EaseMode.In) func = ease.In;
                        else if (tNode.EaseMode == EaseMode.Out) func = ease.Out;
                        value = Mathf.LerpUnclamped(value, tNode.To, func((time - tNode.Offset) / tNode.Duration));
                        break;
                    }
                    else
                    {
                        // �����Ѿ�������ʱ��ڵ�
                        currentValues[eNode.ID] = value = tNode.To;
                        StoryBoard.TimeNodes.Remove(tNode);
                    }
                }
                eNode.Set(this, value);
            }catch (Exception e)
            {
                Debug.LogError(e.Message); // TODO������ϸ���쳣˵��
            }
        currentTime = time; // ������ʱ�������Ѵ����ʱ�䣬���ڼ����ݹ����
    }


}
