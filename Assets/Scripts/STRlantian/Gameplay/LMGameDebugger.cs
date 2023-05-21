using STRlantian.Gameplay.Block;
using STRlantian.Gameplay.Note;
using System.Collections.Generic;
using UnityEngine;

namespace STRlantian.Gameplay
{
    public abstract class LMGameDebugger : LMGameManager
    {
        [SerializeField]
        protected List<ANote> debuggerNotes = new(10);
        [SerializeField]
        protected List<ALMComponent> debuggerComponents = new(10);

        [SerializeField]
        protected bool start = false;

        void Start()
        {
            Init();
        }
        void Update()
        {
            if(start)
            {
                start = false;
                Debug();
            }
        }

        protected new virtual void Init() { }
        protected abstract void Debug();
    }
}
