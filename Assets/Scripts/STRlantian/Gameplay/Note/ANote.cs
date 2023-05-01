using STRlantian.GameEffects;
using STRlantian.Gameplay.Charting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;

namespace STRlantian.Gameplay.Note
{
    public enum NoteType
    {
        TAP,
        FLICK,
        DRAG,
        HOLD
    }

    public abstract partial class ANote : MonoBehaviour
    {
        public NoteType Type { get; private set; };
        public float Speed { get; set; };
        public BeatNode Beat { get; private set; };

        [SerializeField]
        protected BoxCollider box;

        private bool isMulti;
        private bool isOut;
        private HitEffect hit;

        protected ANote(XElement note)
        {
            Init(note);
        }

        void Start()
        {
        }

        void Update()
        {
        }

        protected virtual void Init(XElement note)
        {
            //这里的beat被分为四部分 第一部分是小节 第二部分是拍 第三部分是拍号(2参数) 都用冒号分隔
            //详情可见xml文档
            //NoteHold会重写这个方法 主要是为了去添加其结束拍节点
            uint[] beat = Array.ConvertAll(note.Attribute("beat").Value.Split(":"), uint.Parse);
            Beat = new BeatNode(beat[0], beat[1], new Signature(beat[2], beat[3]));
            Speed = float.Parse(note.Attribute("speed").Value);
            isOut = bool.Parse(note.Attribute("out").Value);
            isMulti = bool.Parse(note.Attribute("multi").Value);
        }

        public virtual async void TriggerNote()
        {
            await new Task(hit.PlayEffect);
            Destroy(gameObject);
        }

        //protected abstract void JudgeNote();
    }
} 