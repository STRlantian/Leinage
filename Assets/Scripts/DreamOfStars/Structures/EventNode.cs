using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 事件节点
/// </summary>
public class EventNode 
{
    /// <summary>
    /// 事件种类
    /// </summary>
    public string ID;
    /// <summary>
    /// 事件名称，后续可以用来做制谱器的备注
    /// </summary>
    public string Name;
    /// <summary>
    /// 委托，返回一个float
    /// </summary>
    public Func<AStoryBoard, float> Get;
    /// <summary>
    /// 委托，设置一个float
    /// </summary>
    public Action<AStoryBoard, float> Set;
    /// <summary>
    /// 委托，处理事件发生时的事情
    /// </summary>
    public Action<AStoryBoard> OnProc;
    /// <summary>
    /// 委托，处理事件完成之后的事情
    /// </summary>
    public Action<AStoryBoard> OnFinish;
}
