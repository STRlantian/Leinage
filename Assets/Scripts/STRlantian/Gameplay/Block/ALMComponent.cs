using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace STRlantian.Gameplay.Block
{
    public abstract class ALMComponent : MonoBehaviour
    {
        protected string id;
        public string ID { get; }

        public ALMComponent(string id, List<XAttribute> attList)
        {
            this.id = id;
            Init(attList);
        }
        public abstract void Init(List<XAttribute> attList);
    }
}