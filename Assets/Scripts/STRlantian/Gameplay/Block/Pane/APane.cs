using STRlantian.Gameplay.Note;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace STRlantian.Gameplay.Block.Pane
{
    /// <summary>
    /// APane: 所有组成几何体的面的派生类
    /// </summary>
    public class Pane : MonoBehaviour
    {
        //要求派生类中必须提供一个枚举 代表面枚举

        [SerializeField]
        protected BoxCollider2D edge;                       //想作为判定线来着

        protected readonly int lineCount;                   //这个面有多少边

        protected Pane(int lineCount)
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