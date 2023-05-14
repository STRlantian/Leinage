using STRlantian.Gameplay.Charting;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace STRlantian.Gameplay.Note
{
    public class NoteHold : ANote
    {
        public BeatNode EndBeat { get; private set; }
        public NoteHold(XElement note, float posY) : base(note, posY) { }

        protected sealed override void Init(XElement note)
        {
            base.Init(note);
            string[] beatEnd = note.Attribute("beat").Value.Split('_');
            uint[] beat = Array.ConvertAll(beatEnd[0].Split(":"), uint.Parse);
            EndBeat = new BeatNode(beat[0], beat[1], new Signature(beat[2], beat[3]));
        }
        /*
        protected override void JudgeNote(Touch touch)
        {
            throw new System.NotImplementedException();
        } */
    }
}