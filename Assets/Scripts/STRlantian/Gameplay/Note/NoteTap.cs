using STRlantian.Gameplay.Note;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace STRlantian.Gameplay.Note
{
    public sealed partial class NoteTap : ANote
    {
        public NoteTap(List<XAttribute> attList) : base(attList) { }

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
    }
}