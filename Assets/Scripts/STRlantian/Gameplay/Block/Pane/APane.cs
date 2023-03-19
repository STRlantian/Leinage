using STRlantian.Gameplay.Note;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace STRlantian.Gameplay.Block.Pane
{
    /// <summary>
    /// APane: 所有组成几何体的面的派生类
    /// </summary>
    public abstract class APane : MonoBehaviour
    {
        //要求派生类中必须提供一个枚举 代表面枚举

        [SerializeField]
        protected BoxCollider2D edge;                       //想作为判定线来着

        protected readonly int lineCount;                   //这个面有多少边
        //protected readonly int slotPerLine;               //每个边多少个note槽

        protected APane(int lineCount)
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
    }
}