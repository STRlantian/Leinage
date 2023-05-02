using System.Collections.Generic;
using System.Xml.Linq;

namespace STRlantian.Gameplay.Block
{
    public class PaneRenderer : ALMComponent
    {
        public BlockRenderer AttachedBlock { get; private set; }
        public Dictionary<string, LineRenderer> Lines { get; private set; }
        public int LineCount { get; private set; }
        public PaneRenderer(XElement ele) : base(ele) { }

        protected override void Init(XElement ele)
        {
            LineCount = 0;
            foreach(XElement line in ele.Elements())
            {
                Lines.Add(line.Value, new LineRenderer(line));
                LineCount++;
            }
        }
    }
}