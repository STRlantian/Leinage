using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace STRlantian.Gameplay.Note
{
    public class NoteDrag : ANote
    {
        public NoteDrag(List<XAttribute> attList) : base(attList) { }

        protected override void JudgeNote(Touch touch)
        {
            if(touch.phase == TouchPhase.Stationary
                || touch.phase == TouchPhase.Moved
                || touch.phase == TouchPhase.Began)
            {
                //然后判断线上
                PlayHitEffect();
            }
        }
    }
}
