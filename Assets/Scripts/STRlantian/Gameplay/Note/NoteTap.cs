﻿using STRlantian.Gameplay.Note;
using System.Collections;
using UnityEngine;

namespace STRlantian.Gameplay.Note
{
    public sealed partial class NoteTap : ANote
    {
        public NoteTap() : base(NoteTypes.TAP) {}

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