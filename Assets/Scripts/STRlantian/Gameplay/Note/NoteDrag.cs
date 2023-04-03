<<<<<<< HEAD
﻿using System.Collections.Generic;
=======
﻿using STRlantian.Gameplay.Note;
using System;
using System.Collections.Generic;
>>>>>>> 1aef71bb3d4366297ade331e71fff7322ee4d1fc
using System.Xml.Linq;
using UnityEngine;

namespace STRlantian.Gameplay.Note
{
    public class NoteDrag : ANote
    {
        public NoteDrag(List<XAttribute> attList) : base(attList) { }

<<<<<<< HEAD
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
=======
        //protected sealed override void JudgeNote()
        //{
        //    //Touch touch = 
        //    throw NotImplementedException;
        //}
>>>>>>> 1aef71bb3d4366297ade331e71fff7322ee4d1fc
    }
}
