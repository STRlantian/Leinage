using STRlantian.Gameplay.Note;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;

namespace STRlantian.Gameplay.Charting
{
    /// <summary>
    /// Chart: 谱面对象 包含一堆东西
    /// </summary>
    public partial class Chart
    {
        private ChartBasicInfo info;                  //谱面基本信息
        private Queue<ANote> noteList = new(50);

        public Queue<ANote> NoteList
        {
            get { return noteList; }
        }
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
            List<XElement> notes = new(chartFile.Elements("notes"));
            ProcessNote(notes);
        }

        private void ProcessNote(List<XElement> notes)
        {
            List<XAttribute> attList = new(5);
            foreach (XElement el in notes)
            {
                ANote note;
                attList = new(el.Attributes());
                switch(attList[0].Value)
                {
                    case "tap":
                        note = new NoteTap(attList);
                        break;
                    case "flick":
                        note = new NoteFlick(attList);
                        break;
                    case "drag":
                        note = new NoteDrag(attList);
                        break;
                    case "hold":
                        note = new NoteHold(attList);
                        
                        break;
                    default:
                        throw new System.Exception("Invalid note type value!");
                }
            }
        }
    }

    public struct ChartBasicInfo
    {
        public string name;                     //曲名
        public string level;                    //级别 RL MD SP CL ?
        public string diff;                     //定数 0 - zeta
        public Vector2 rhythm;                   //拍号 eg. 2:4 代表四二拍
        public float bpm;                       //bpm
        public float speed;
        public int time;                        //时间 秒
        public float offset;                    //延迟 ms 可带小数点
        public int beat;                        //总拍数

        public ChartBasicInfo(List<XAttribute> list) 
        {
            name = list[0].Value;
            level = list[1].Value;
            diff = list[2].Value;
            string[] rhy = list[3].Value.Split(':');
            rhythm = new Vector2(float.Parse(rhy[0]), float.Parse(rhy[1]));
            bpm = float.Parse(list[4].Value);
            speed = float.Parse(list[5].Value);
            time = int.Parse(list[6].Value);
            offset = float.Parse(list[7].Value);
            beat = (int)(time * bpm / 60);
        }
    }
}
