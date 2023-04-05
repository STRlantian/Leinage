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
        private Queue<ANote> noteList = new(50);
        public Queue<ANote> NoteList
        {
            get { return noteList; }
        }

        private List<BlockRenderer> blockList = new(10);
        public List<BlockRenderer> BlockList
        {
            get { return blockList; }
        }

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
            info = new(new(chartFile.Element("info").Attributes()));
            ProcessBlocks(new(chartFile.Elements("blocks")));
        }

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
    }

    public struct ChartBasicInfo
    {
        public string name;                     //曲名
        public string level;                    //级别 RL MD SP CL ?
        public string diff;                     //定数 0 - zeta
        public Vector2 rhythm;                   //拍号 eg. 2:4 代表四二拍
        public float bpm;                       //bpm
        public float speed;                     //全局速度
        public int time;                        //时间 秒
        public float offset;                    //延迟 ms 可带小数点
        public int beat;                        //总拍数
        public string author;
        public string illust;

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
            author = list[8].Value;
            illust = list[9].Value;
        }
    }
}
