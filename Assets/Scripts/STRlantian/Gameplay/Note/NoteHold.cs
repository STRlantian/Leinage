using System.Collections.Generic;
using System.Xml.Linq;

namespace STRlantian.Gameplay.Note
{
    public class NoteHold : ANote
    {
        public ushort[] length;
        public NoteHold(List<XAttribute> attList) : base(attList) { }

        protected sealed override void InitAttributes(List<XAttribute> attList)
        {
            base.InitAttributes(attList);
        }
        /*
        protected override void JudgeNote(Touch touch)
        {
            throw new System.NotImplementedException();
        } */
    }
}