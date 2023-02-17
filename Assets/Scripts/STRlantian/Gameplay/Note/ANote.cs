using System.Collections;
using UnityEngine;

namespace STRlantian.Gameplay.Note
{
    public enum NoteType
    {
        TAP,
        HOLD,
        DRAG,
        FLICK,
        SPIN
    }
    public abstract class ANote : MonoBehaviour
    {
        public NoteType type;

        protected readonly bool isOut;

        protected ANote(NoteType type, bool isOut)
        {
            this.type = type;
            this.isOut = isOut;
        }
        void Start()
        {

        }

        void Update()
        {

        }
    }
}