using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;

namespace STRlantian.Gameplay.Note
{
    public class NoteHold : ANote
    {
        public ushort[] length;
        public NoteHold(List<XAttribute> attList) : base(attList) { }

        protected sealed override void InitAttributes(List<XAttribute> attList)
        {
            base.InitAttributes(attList);
            length = new ushort[2] { (ushort)(beat[2] - beat[0]), (ushort)(beat[3] - beat[1]) };
        }

        protected override void JudgeNote(Touch touch)
        {
            throw new System.NotImplementedException();
        }
    }
}