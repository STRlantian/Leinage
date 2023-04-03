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
    /// 在音乐中的偏移，对应的是beat
    /// </summary>
    public float Offset;
    /// <summary>
    /// 该时间节点持续的时间，单位为beat
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
/// 游戏中BPM、时间、节拍的相关类
/// </summary>
public class SongTimer
{
    public List<BPMNode> BPMNodes=new List<BPMNode>();

    public SongTimer(float bpm)
    {
        BPMNodes.Add(new BPMNode(bpm,0));
    }

    public SongTimer(float bpm,float offset) { 
        BPMNodes.Add(new BPMNode(bpm,offset));
    }

    /// <summary>
    /// 根据给出的时间返回当前所处节拍
    /// </summary>
    /// <param name="sec"></param>
    /// <returns></returns>
    public float SecToBeat(float sec)
    {
        if (BPMNodes.Count == 0) return float.NaN;
        float beat = 0;
        for(int i = 0; i < BPMNodes.Count; i++)
        {
            float totalBeats = (sec - BPMNodes[i].Offset) / 60 * BPMNodes[i].BPM;
            if(i+1 < BPMNodes.Count)
            {
                float curBeats= (BPMNodes[i+1].Offset - BPMNodes[i].Offset) / 60 * BPMNodes[i].BPM;
                if (totalBeats <= curBeats) return beat + totalBeats;
                beat += curBeats;
            }
            else
            {
                return beat + totalBeats;
            }
        }
        return beat;
    }

    /// <summary>
    /// 根据给出的节拍数返回对应的时间
    /// </summary>
    /// <param name="beat"></param>
    /// <returns></returns>
    public float BeatToSec(float beat) {
        if (BPMNodes.Count == 0) return float.NaN;
        for(int i = 0;i < BPMNodes.Count; i++)
        {
            BPMNode bn= BPMNodes[i];
            float totalSec = beat * (60 / BPMNodes[i].BPM)+BPMNodes[i].Offset;
            if (i + 1 < BPMNodes.Count)
            {
                float curBeat = (BPMNodes[i + 1].Offset - BPMNodes[i].Offset) / 60 * BPMNodes[i].BPM;
                if (beat <= curBeat) return totalSec;
                beat -= curBeat;
            }
            else return totalSec;
        }
        return 0;
    }

    // 将采样数转换为节拍数（rounded down to the nearest beat）
    // 参数：
    //     sample: 音频中的采样数
    // 返回：
    //     以 div 为单位的节拍数
    //public int SampleToBeat(int sample){
    //    float secPerBeat = 60.0f / bpm;
    //    float secPerDiv = secPerBeat / div;
    //    float divs = (float)sample * au.clip.frequency / div / AudioSettings.outputSampleRate / secPerDiv; 
    //    return Mathf.FloorToInt(divs);
    //} 
    
    //// 将节拍数转换为采样数
    //// 参数：
    ////     beat: 以 div 为单位的节拍数
    //// 返回：
    ////     音频中的采样数
    //public int BeatToSample(int beat){
    //    float secPerBeat = 60.0f / bpm;
    //    float secPerDiv = secPerBeat / div;
    //    float secs = (float)beat * secPerDiv;
    //    return Mathf.FloorToInt((secs + (float)offset / 1000.0f) * AudioSettings.outputSampleRate / au.clip.frequency);
    //}
}

/// <summary>
/// BPM变换的节点
/// </summary>
[System.Serializable]
public class BPMNode
{
    public float Offset;
    public float BPM;
    public int Div = 4;

    public BPMNode(float bpm,float offset)
    {
        Offset = offset;
        BPM = bpm;
    }
}
