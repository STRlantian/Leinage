using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaManager : MonoBehaviour
{
    /// <summary>
    /// 管理设备安全区域，对UI进行适配
    /// </summary>
    void Awake()
    {
        RectTransform rt=GetComponent<RectTransform>();
        Rect screen = new Rect(0, 0, Screen.width, Screen.height);
        Rect safeArea = Screen.safeArea;
        float scale = Screen.width / 800; // Canvas 初始设定 800 x 600 后续可能再改动
        rt.anchoredPosition = new Vector2(0, 0);
        rt.sizeDelta = new Vector2(-Mathf.Max(screen.xMax-safeArea.xMax,safeArea.xMin-screen.xMin)*2/scale,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
