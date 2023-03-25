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
    /// 给StoryBoard加入一个时间节点
    /// </summary>
    /// <param name="timeNode"></param>
    public void Add(TimeNode timeNode)
    {
        TimeNodes.Add(timeNode);
        TimeNodes.Sort((x,y)=>x.Offset.CompareTo(y.Offset)); // 添加后重新排序
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

// TODO: 当两个相同类型事件有重合时间部分时，会出现bug（比如1-3s设置x到0，2-6s设置x到10），应该加入判断抛出异常

/// <summary>
/// StoryBoard的抽象类
/// 会根据每个类的EventNodes来处理StoryBoard
/// </summary>
public abstract class AStoryBoard
{
    public StoryBoard StoryBoard = new StoryBoard();
    public static EventNode[] EventNodes = { };

    private EventNode[] _eNode;

    /// <summary>
    /// 针对给出的时间time返回此时对应的AStoryBoard
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public AStoryBoard Get(float time)
    {
        if (_eNode == null) _eNode = (EventNode[])this.GetType().GetField("EventNodes").GetValue(null);
        AStoryBoard obj=(AStoryBoard)this.MemberwiseClone();   // 浅拷贝一波
        foreach(EventNode eNode in _eNode) try
        {
                List<TimeNode> storyBoardTimeNode=StoryBoard.FindAllByID(eNode.ID); // 找到EventNode对应的StoryBoard
                float value = eNode.Get(this);

                // 遍历寻找time时间之后的下一个时间节点
                foreach(TimeNode tNode in storyBoardTimeNode)
                {
                    if (time >= tNode.Offset + tNode.Duration) value = tNode.To; // time已经超出当前TimeNode指示时间范围，即该TimeNode的事件已结束，值设为结束时状态
                    else if (time > tNode.Offset)
                    {
                        if (!float.IsNaN(tNode.From)) value = (float)tNode.From; // 有From则从From开始计算插值
                        // 设置缓动
                        Ease ease = Ease.EasesFuncDict[tNode.EaseFunc];
                        Func<float, float> func = ease.InOut; // 默认
                        if (tNode.EaseMode==EaseMode.In) func = ease.In;
                        else if(tNode.EaseMode==EaseMode.Out) func = ease.Out;
                        // 用插值计算当前值
                        value = Mathf.LerpUnclamped(value, tNode.To, func((time - tNode.Offset) / tNode.Duration));
                        break;
                    }
                    else break;
                }

                eNode.Set(obj, value);
                    
        }catch(System.Exception e)
            {
                Debug.LogError(e.Message); //TODO: 更详细的异常说明
            }
        return obj;
    }

    /// <summary>
    /// 根据当前时间currentTime和指定time更新StoryBoard
    /// </summary>
    float currentTime;
    Dictionary<string, float> currentValues;
    public void Update(float time)
    {
        if (_eNode == null) _eNode = (EventNode[])this.GetType().GetField("EventNodes").GetValue(null);
        if (currentValues == null)
        {
            // 深拷贝所有Event
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
                    if (tNode == null || (time < tNode.Offset && currentTime < tNode.Offset)) break; // 遍历到还未发生的时间节点，直接跳出
                    else if (time < tNode.Offset + tNode.Duration)
                    {
                        // 正好处于一个时间节点的事件过程中
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
                        // 处理已经经过的时间节点
                        currentValues[eNode.ID] = value = tNode.To;
                        StoryBoard.TimeNodes.Remove(tNode);
                    }
                }
                eNode.Set(this, value);
            }catch (Exception e)
            {
                Debug.LogError(e.Message); // TODO：更详细的异常说明
            }
        currentTime = time; // 将现在时间移至已处理的时间，便于继续递归调用
    }


}
