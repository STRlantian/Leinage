using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace STRlantian.Gameplay.Block
{
    /// <summary>
    /// 分别是正四 六 八 十二 二十面体
    /// </summary>
    public enum BlockType      
    {           
        TETRA = 4,
        HEX = 6,
        OCTA = 8,
        DODE = 12,
        ICO = 20
    }

    /// <summary>
    /// 抽象类Block 所有方块的继承对象
    /// </summary>
    public class BlockRenderer : ALMComponent
    {
        public bool isBack;                                         //isBack: 是否仅用于表演 是的话就省点占用
        public BlockType Type { get; private set; }                                     //type: 如上枚举
        public Dictionary<string, PaneRenderer> Panes { get; private set; }

        public BlockRenderer(XElement ele) : base(ele) {}
        protected override void Init(XElement ele)
        {
            Type = Enum.Parse<BlockType>(ele.Attribute("type").Value.ToUpper());
            isBack = bool.Parse(ele.Attribute("back").Value.ToLower());
            foreach (XElement pane in ele.Elements())
            {
                Panes.Add(pane.Value, new PaneRenderer(pane));
            }
        }
    }
}