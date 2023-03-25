using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor;

namespace STRlantian.Gameplay.Note
{
    public class NoteHold : ANote
    {
        public int length;
        public NoteHold(List<XAttribute> attList) : base(attList) { }

        protected sealed override void InitAttributes(List<XAttribute> attList)
        {
            base.InitAttributes(attList);
            length = attList.
        }
    }
}
