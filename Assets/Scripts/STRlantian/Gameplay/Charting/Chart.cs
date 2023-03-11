using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace STRlantian.Gameplay.Charting
{
    /// <summary>
    /// Chart: 谱面对象 包含一堆东西
    /// </summary>
    public partial class Chart
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
            LoadChartFile(chartFile);
        }

        private void LoadChartFile(XDocument chartFile)
        {
            List<XAttribute> xInfo = new(chartFile.Element("info").Attributes());
            info = new(xInfo);
        }
    }

    public struct ChartBasicInfo
    {
        public string name;                     //曲名
        public string level;                    //级别 RL MD SP CL ?
        public string diff;                     //定数 0 - zeta
        public float bpm;                       //bpm
        public int time;                        //时间 秒
        public float offset;                    //延迟 ms 可带小数点
        public int beat;                        //总拍数

        public ChartBasicInfo(List<XAttribute> list) 
        {
            name = list[0].Value;
            level = list[1].Value;
            diff = list[2].Value;
            bpm = float.Parse(list[3].Value);
            time = int.Parse(list[4].Value);
            offset = float.Parse(list[5].Value);
            beat = (int)(time * bpm / 60);
        }
    }
}
