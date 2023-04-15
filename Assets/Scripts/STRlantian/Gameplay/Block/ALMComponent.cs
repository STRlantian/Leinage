using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace STRlantian.Gameplay.Block
{
    public abstract class ALMComponent : MonoBehaviour
    {
        public string ID { get; private set; }

        /// <summary>
        /// 构造一个组件
        /// </summary>
        /// <param name="ele">铺面文件里面的对应元素,可能是块面线</param>
        public ALMComponent(XElement ele)
        {
            ID = ele.Value;
            Init(ele);
        }
        public abstract void Init(XElement ele);
    }
}