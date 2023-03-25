using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 与事件关联的时间节点
/// </summary>
[System.Serializable]
public class TimeNode
{
    /// <summary>
    /// 时间节点通过ID与同ID的事件相关联
    /// </summary>
    public string ID;
    /// <summary>
    /// 在音乐中的偏移，可以理解为时间
    /// </summary>
    public float Offset;
    /// <summary>
    /// 该时间节点持续的时间
    /// </summary>
    public float Duration;
    /// <summary>
    /// 事件起始值（如缓动开始位置x，音符开始下落位置），可以为空。空时取当前状态
    /// </summary>
    public float From = float.NaN;
    /// <summary>
    /// 事件结束值（如缓动结束位置x）, 必填
    /// </summary>
    public float To ;
    /// <summary>
    /// 缓动函数，默认为线性
    /// </summary>
    public EaseFunction EaseFunc = EaseFunction.Linear;
    /// <summary>
    /// 缓动模式，默认为InOut
    /// </summary>
    public EaseMode EaseMode;

    public TimeNode DeepClone()
    {
        TimeNode clone = new TimeNode()
        {
            ID = ID,
            Offset = Offset,
            Duration = Duration,
            From=From,
            To=To,
            EaseFunc=EaseFunc,
            EaseMode=EaseMode
        };
        return clone;
    }
}



/// <summary>
/// 节拍数和采样数转换脚本
/// </summary>
public class SongTimer
{
    public AudioSource au{get;set;} // 音乐文件
    public float bpm{get;set;}     // 每分钟节拍数
    public int div{get;set;}       // 每拍 tick 数
    public int offset{get;set;}    // 音乐和游戏节拍的偏移量（单位：毫秒）

    // 将采样数转换为节拍数（rounded down to the nearest beat）
    // 参数：
    //     sample: 音频中的采样数
    // 返回：
    //     以 div 为单位的节拍数
    public int SampleToBeat(int sample){
        float secPerBeat = 60.0f / bpm;
        float secPerDiv = secPerBeat / div;
        float divs = (float)sample * au.clip.frequency / div / AudioSettings.outputSampleRate / secPerDiv; 
        return Mathf.FloorToInt(divs);
    } 
    
    // 将节拍数转换为采样数
    // 参数：
    //     beat: 以 div 为单位的节拍数
    // 返回：
    //     音频中的采样数
    public int BeatToSample(int beat){
        float secPerBeat = 60.0f / bpm;
        float secPerDiv = secPerBeat / div;
        float secs = (float)beat * secPerDiv;
        return Mathf.FloorToInt((secs + (float)offset / 1000.0f) * AudioSettings.outputSampleRate / au.clip.frequency);
    }
}
