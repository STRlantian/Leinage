using STRlantian.Gameplay.Charting;
using System.Collections.Generic;
using System.Xml.Linq;

namespace STRlantian.Gameplay.Note
{
    public class NoteHold : ANote
    {
        public BeatNode EndBeat { get; private set; }
        public NoteHold(XElement note) : base(note) { }

        protected sealed override void Init(XElement note)
        {
            base.Init(note);
        }
        /*
        protected override void JudgeNote(Touch touch)
        {
            throw new System.NotImplementedException();
        } */
    }
}