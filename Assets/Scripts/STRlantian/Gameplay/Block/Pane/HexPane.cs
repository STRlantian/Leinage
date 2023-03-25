using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STRlantian.Gameplay.Block.Pane
{
    public class HexPane : APane
    {
        public enum HexPanes
        {
            A,
            AX,
            B,
            BX,
            C,
            CX
        }

        public HexPanes dire;
        protected HexPane() : base(4) {}

        public bool Equals(HexPanes pane)
        {
            return dire == pane;
        }
    }
}
