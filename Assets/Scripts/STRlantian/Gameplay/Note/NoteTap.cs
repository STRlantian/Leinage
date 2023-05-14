using STRlantian.Gameplay.Note;
using STRlantian.Util;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace STRlantian.Gameplay.Note
{
    public class NoteTap : ANote
    {
        public NoteTap(XElement note, float posY) : base(note, posY) { }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
        }

        public override void TriggerNote()
        {

            base.TriggerNote();
        }
        /*
        protected override void JudgeNote(Touch touch)
        {  
            throw new System.NotImplementedException();
        }    
        */     
    }
}