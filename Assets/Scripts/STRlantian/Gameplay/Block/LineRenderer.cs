using STRlantian.Gameplay.Block;
using STRlantian.Gameplay.Note;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace STRlantian.Gameplay.Block
{
    public class LineRenderer : ALMComponent
    {
        public const int Length = 10;                           //长度 代表Note横向位置
        public float Height { get; private set; }
        public Queue<ANote> notes { get; private set; }

        public LineRenderer(XElement ele) : base(ele) { }

        public void SummonNote(float x, bool isOut)
        {

        }

        public override void Init(XElement ele)
        {
            foreach(XElement note in ele.Elements())
            { 
            }
        }
    }
}