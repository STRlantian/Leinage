using STRlantian.Gameplay.Block.Pane;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace STRlantian.Gameplay.Block.Block
{
    /// <summary>
    /// 分别是正四 六 八 十二 二十面体
    /// </summary>
    public enum BlockTypes      
    {           
        TETRA,
        HEX,
        OCTA,
        DODE,
        ICO
    }

    /// <summary>
    /// 抽象类Block 所有方块的继承对象
    /// </summary>
    public class ABlock : MonoBehaviour
    {
        public bool isBack;                                         //isBack: 是否仅用于表演 是的话就省点占用
        public BlockTypes type;                                     //type: 如上枚举
        public List<Pane.Pane> panes;

        private string blockName;                                   //blockName: 方块标识符名字
        public string Name
        {
            get { return blockName; }
        }
        protected ABlock(List<XAttribute> attList)
        {
            InitAttributes(attList);
        }

        protected virtual void InitAttributes(List<XAttribute> attList)
        {
            blockName = attList[0].Value;
            string tp = attList[1].Value.ToLower();
            type = tp.Equals("tetra") ? BlockTypes.TETRA
                 : tp.Equals("hex") ? BlockTypes.HEX
                 /*
                 : tp.Equals("octa") ? BlockTypes.OCTA
                 : tp.Equals("dode") ? BlockTypes.DODE
                 : tp.Equals("ico") ? BlockTypes.ICO
                 目前这三个东西还不准备实现
                 */
                 : throw new System.Exception("Invalid Block Type!");
        }

        /*
         *实现的要有一系列运动与缩小放大等
         */
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}