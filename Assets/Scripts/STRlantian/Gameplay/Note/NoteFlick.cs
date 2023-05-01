using STRlantian.Gameplay.Note;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace STRlantian.Gameplay.Note
{
    public class NoteFlick : ANote
    {
        public NoteFlick(XElement note) : base(note) { }
        /*
        protected override void JudgeNote(Touch touch)
        {
            throw new System.NotImplementedException();
        } */
    }
}
