using System.Collections.Generic;
using System.Xml.Linq;

namespace STRlantian.Gameplay.Note
{
    public class NoteHold : ANote
    {
        public int length;
        public NoteHold(List<XAttribute> attList) : base(attList) { }
    }
}
