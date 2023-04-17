using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �¼��ڵ�
/// </summary>
public class EventNode 
{
    /// <summary>
    /// �¼�����
    /// </summary>
    public string ID;
    /// <summary>
    /// �¼����ƣ����������������������ı�ע
    /// </summary>
    public string Name;
    /// <summary>
    /// ί�У�����һ��float
    /// </summary>
    public Func<AStoryBoard, float> Get;
    /// <summary>
    /// ί�У�����һ��float
    /// </summary>
    public Action<AStoryBoard, float> Set;
    /// <summary>
    /// ί�У������¼�����ʱ������
    /// </summary>
    public Action<AStoryBoard> OnProc;
    /// <summary>
    /// ί�У������¼����֮�������
    /// </summary>
    public Action<AStoryBoard> OnFinish;
}
