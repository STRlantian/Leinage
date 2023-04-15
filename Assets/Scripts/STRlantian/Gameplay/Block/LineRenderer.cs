using STRlantian.Gameplay.Block;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace STRlantian.Gameplay.Block
{
    public class LineRenderer : ALMComponent
    {
        private float length;                   //就是note里面那个x的取值范围 输入的数字是指正负都有 
        public float Length { get; }
        private float height;
        public float Height { get; }
        public bool pointMode = false;

        public LineRenderer(XElement ele) : base(ele) { }

        public void SummonNote(float x)
        {

        }

        public override void Init(XElement ele)
        {
        }
    }
}