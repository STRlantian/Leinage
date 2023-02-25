using STRlantian.Gameplay.Note;
using System.Collections.Generic;
using UnityEngine;

namespace STRlantian.Gameplay.Block.Pane
{
    public enum Panes
    {
        A,
        AX,
        B,
        BX,
        C,
        CX
    }
    public class APane : MonoBehaviour
    {
        [SerializeField]
        protected BoxCollider2D edge;

        protected readonly int lineCount;
        protected readonly int slotPerLine;
        List<NoteReceiver> slots = new(8);

        protected APane(int lineCount, int slotPerLine)
        {
            this.lineCount = lineCount;
            this.slotPerLine = slotPerLine;
        }

        void Start()
        {

        }

        void Update()
        {

        }

        private void ApplySlot()
        {
            Bounds bound = edge.bounds;
            Vector2 max = bound.max;
            Vector2 min = bound.min;
            
        }
    }
}