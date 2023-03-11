using STRlantian.Gameplay.Note;
using System.Collections.Generic;
using UnityEngine;

namespace STRlantian.Gameplay.Block.Pane
{
    /// <summary>
    /// Panes: 每个面对用的参数编号
    /// </summary>
    public enum Panes
    {
        A,
        AX,
        B,
        BX,
        C,
        CX
    }

    /// <summary>
    /// APane: 所有组成几何体的面的派生类
    /// </summary>
    public partial class APane : MonoBehaviour
    {
        [SerializeField]
        protected BoxCollider2D edge;                       //想作为判定线来着

        protected readonly int lineCount;                   //这个面有多少边
        //protected readonly int slotPerLine;               //每个边多少个note槽

        protected APane(int lineCount /*, int slotPerLine */)
        {
            this.lineCount = lineCount;
            //this.slotPerLine = slotPerLine;
        }

        void Start()
        {

        }

        void Update()
        {

        }

        /*
        private void ApplySlot()
        {
            Bounds bound = edge.bounds;
            Vector2 max = bound.max;
            Vector2 min = bound.min;
            
        }
        */
    }
}