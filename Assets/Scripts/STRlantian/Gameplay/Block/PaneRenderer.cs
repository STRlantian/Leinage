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

        public PaneRenderer(string id, List<XAttribute> attList) : base(id, attList) {}

        void Start()
        {

        }

        void Update()
        {

        }

        public override void Init(List<XAttribute> attList)
        {
            
        }
    }
}