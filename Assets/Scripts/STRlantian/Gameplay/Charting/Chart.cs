using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace STRlantian.Gameplay.Charting
{
    /// <summary>
    /// Chart: 谱面对象 包含一堆东西
    /// </summary>
    public class Chart
    {
        private ChartBasicInfo info;                  //谱面基本信息
        public ChartBasicInfo BasicInfo
        {
            get { return info; }
        }

        /// <summary>
        /// 构造方法: 从一个文件读取 读取过程发生在类ChartIO 解析过程在此类
        /// </summary>
        /// <param name="chartFile">谱面xml文件</param>
        /// 话说用xml文件存谱面 好像还是第一次见(? 都用json了 可是我偏爱xml和yml
        public Chart(XDocument chartFile)
        {
        }

        private void LoadChartFile()
        {

        }
    }

    public struct ChartBasicInfo
    {
        public string name;                     //曲名
        public int level;                       //级别 0, 1, 2, 3
        public string difficulty;               //定数 0 - zeta
        public int seconds;                     //时间 秒
        public float offset;                    //延迟 秒 可带小数点
        public float bpm;                       //bpm
        public int beat;                        //总拍数
    }
}
