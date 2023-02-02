using System.Collections.Generic;

namespace STRlantian.Gameplay.Charting
{
    public class Chart
    {
        public ChartBasicInfo info
        {
            get;
        }

        public Chart(List<string> chartFile)
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
        public System.Numerics.Vector2 beat;    //拍号 不是Unity的V2 x表示每小节拍数 y表示什么音符 如3/4写为x = 3 y = 4
    }
}
