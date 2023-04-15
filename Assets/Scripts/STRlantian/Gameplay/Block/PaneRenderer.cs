using STRlantian.Gameplay.Note;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace STRlantian.Gameplay.Block
{
    public class PaneRenderer : ALMComponent
    {
        List<LineRenderer> lines;
        public PaneRenderer(XElement ele) : base(ele) { }

        public override void Init(XElement ele)
        {
            
        }
    }
}