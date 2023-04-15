using DreamOfStars.GamePlay;
using STRlantian.Gameplay.Block;
using STRlantian.Gameplay.Note;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using UnityEngine;

namespace STRlantian.Gameplay.Charting
{
    /// <summary>
    /// Chart: 谱面对象 包含一堆东西
    /// </summary>
    public partial class Chart
    {
        public Queue<ANote> NoteList
        { get; private set; }

        public List<BlockRenderer> BlockList { get; private set; }
        public ChartBasicInfo BasicInfo { get; private set; }

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
            XElement chart = chartFile.Element("chart");
            BasicInfo = new ChartBasicInfo(new List<XAttribute>(chart.Element("info").Attributes()));
            foreach(XElement block in chart.Element("blocks").Elements())
            {
                BlockList.Add(new BlockRenderer(block));
            }
        }

        /*
        private void ProcessNotes(List<XElement> notes)
        {
            List<XAttribute> attList = new(9);
            foreach (XElement el in notes)
            {
                ANote note;
                attList = new(el.Attributes());
                NoteType tp = Enum.Parse<NoteType>(attList[0].Value.ToUpper());
                note = tp == NoteType.TAP ? new NoteTap(attList)
                    : tp == NoteType.FLICK ? new NoteFlick(attList)
                    : tp == NoteType.DRAG ? new NoteDrag(attList)
                    : tp == NoteType.HOLD ? new NoteHold(attList)
                    : throw new Exception("Invalid Note Type");
                noteList.Enqueue(note);
            }
        }

        private void ProcessBlocks(List<XElement> blocks)
        {
            List<XAttribute> attList = new(3);
            foreach (XElement el in blocks)
            {
                BlockRenderer block;
                attList = new(el.Attributes());
                block = new(el.Value, attList);
            } 
        }
        

        private void ProcessPanes(BlockRenderer block, List<XElement> panes)
        {
            List<XAttribute> atList = new(3);
        }
        */
    }

    public struct ChartBasicInfo
    {
        public string Name { get; private set; }                     //曲名
        public string Level { get; private set; }                    //级别 RL MD SP CL ?
        public string Diff { get; private set; }                     //定数 0 - ζ
        public Signature Rhythm { get; private set; }                //拍号(struct Signature)
        public float Bpm { get; private set; }                       //bpm
        public float Speed { get; private set; }                     //全局速度
        public int Time { get; private set; }                        //总时间 秒
        public float Offset { get; private set; }                    //延迟 ms 可带小数点
        public string Composer { get; private set; }                 //曲师
        public string Author { get; private set; }                   //谱师
        public string Illust { get; private set; }                   //画师

        public ChartBasicInfo(List<XAttribute> list) 
        {
            Name = list[0].Value;
            Level = list[1].Value;
            Diff = list[2].Value;
            string[] rhy = list[3].Value.Split(':');
            Rhythm = new Signature(beatsPerBar: byte.Parse(rhy[0]), timePerBeat: byte.Parse(rhy[1]));
            Bpm = float.Parse(list[4].Value);
            Speed = float.Parse(list[5].Value);
            Time = int.Parse(list[6].Value);
            Offset = float.Parse(list[7].Value);
            Composer = list[8].Value;
            Author = list[9].Value;
            Illust = list[10].Value;
        }
    }
}
