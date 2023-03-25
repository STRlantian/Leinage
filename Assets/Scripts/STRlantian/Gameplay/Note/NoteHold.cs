using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor;

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
    }
}