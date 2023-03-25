using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum EaseMode
{
    In,Out,InOut
}

[Serializable]
public enum EaseFunction
{
    Linear,       // 线性
    Sine,         // 正弦
    Quadratic,    // 二次
    Cubic,        // 三次
    Quartic,      // 四次
    Quintic,      // 五次（会不会太多了）
    Exponential,  // 指数
    Circle        // 圆形
}

/// <summary>
/// 缓动类
/// </summary>
[Serializable]
public class Ease
{

    public Func<float, float> In;
    public Func<float, float> Out;
    public Func<float, float> InOut;


    /// <summary>
    /// 缓动函数具体实现，查询
    /// <param name="EaseFunction">缓动方法名</param>
    /// 得到
    /// <param name="Ease">缓动函数</param>
    /// </summary>
    public static Dictionary<EaseFunction, Ease> EasesFuncDict = new Dictionary<EaseFunction, Ease>()
    {
        {EaseFunction.Linear, new Ease {
            In = (x) => x,
            Out = (x) => x,
            InOut = (x) => x,
        }},
        {EaseFunction.Sine, new Ease {
            In = (x) => 1 - Mathf.Cos((x * Mathf.PI) / 2),
            Out = (x) => Mathf.Sin((x * Mathf.PI) / 2),
            InOut = (x) => (1 - Mathf.Cos(x * Mathf.PI)) / 2,
        }},
        {EaseFunction.Quadratic, new Ease {
            In = (x) => x * x,
            Out = (x) => 1 - Mathf.Pow(1 - x, 2),
            InOut = (x) => x < 0.5f ? 2 * x * x : 1 - Mathf.Pow(-2 * x + 2, 2) / 2,
        }},
        {EaseFunction.Cubic, new Ease {
            In = (x) => x * x * x,
            Out = (x) => 1 - Mathf.Pow(1 - x, 3),
            InOut = (x) => x < 0.5f ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2,
        }},
        {EaseFunction.Quartic, new Ease {
            In = (x) => x * x * x * x,
            Out = (x) => 1 - Mathf.Pow(1 - x, 4),
            InOut = (x) => x < 0.5f ? 8 * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 4) / 2,
        }},
        {EaseFunction.Quintic, new Ease {
            In = (x) => x * x * x * x * x,
            Out = (x) => 1 - Mathf.Pow(1 - x, 5),
            InOut = (x) => x < 0.5f ? 16 * x * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 5) / 2,
        }},
        {EaseFunction.Exponential, new Ease {
            In = (x) => x == 0 ? 0 : Mathf.Pow(2, 10 * x - 10),
            Out = (x) => x == 1 ? 1 : 1 - Mathf.Pow(2, -10 * x),
            InOut = (x) => x == 0 ? 0 : x == 1 ? 1 : x < 0.5 ? Mathf.Pow(2, 20 * x - 10) / 2 : (2 - Mathf.Pow(2, -20 * x + 10)) / 2,
        }},
        {EaseFunction.Circle, new Ease {
            In = (x) => 1 - Mathf.Sqrt(1 - Mathf.Pow(x, 2)),
            Out = (x) => Mathf.Sqrt(1 - Mathf.Pow(x - 1, 2)),
            InOut = (x) => x < 0.5 ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * x, 2))) / 2 : (Mathf.Sqrt(1 - Mathf.Pow(-2 * x + 2, 2)) + 1) / 2,
        }},
    };

    public static float Get(float x , EaseFunction ease , EaseMode mode)
    {
        Ease _ease = EasesFuncDict[ease];
        var func = _ease.InOut; // 默认
        if (mode == EaseMode.In) func=_ease.In;
        if (mode == EaseMode.Out) func=_ease.Out;
        return func(Mathf.Clamp01(x)); // x 要在0-1之间
    }

}
