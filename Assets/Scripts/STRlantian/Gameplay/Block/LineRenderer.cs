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
        public Queue<ANote> Notes { get; private set; }

        public LineRenderer(XElement ele) : base(ele) { }

        public void SummonNote(float x, bool isOut)
        {

        }

        protected override void Init(XElement ele)
        {
            Height = float.Parse(ele.Attribute("height").Value);
            ANote tar;
            NoteType tp;
            foreach(XElement note in ele.Elements())
            {
                tp = System.Enum.Parse<NoteType>(note.Value);
                tar = tp == NoteType.TAP ? new NoteTap(note, Height)
                    : tp == NoteType.FLICK ? new NoteFlick(note, Height)
                    : tp == NoteType.DRAG ? new NoteDrag(note, Height)
                    : tp == NoteType.HOLD ? new NoteHold(note, Height)
                    : throw new System.Exception("Invalid NoteType");
                Notes.Enqueue(tar);
            }
        }
    }
}