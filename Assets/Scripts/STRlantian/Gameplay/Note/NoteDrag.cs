using STRlantian.Gameplay.Note;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace STRlantian.Gameplay.Note
{
    public class NoteDrag : ANote
    {
        public NoteDrag(XElement note) : base(note) { }

        /*
        protected sealed override void JudgeNote()
        {
            //Touch touch = 
            throw NotImplementedException;
        } */
    }
}
