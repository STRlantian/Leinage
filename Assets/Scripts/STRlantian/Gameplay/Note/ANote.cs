using STRlantian.GameEffects;
using STRlantian.Gameplay.Charting;
using STRlantian.Util;
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
        public NoteType Type { get; private set; }
        public float Speed { get; set; }
        public BeatNode Beat { get; private set; }

        [SerializeField]
        public BoxCollider2D box;
        [SerializeField]
        public new SpriteRenderer renderer;

        protected bool isMulti;
        protected bool isOut;
        protected HitEffect hit;
        protected bool isActive = false;
        public float posX, posY;

        protected ANote(XElement note, float posY)
        {
            this.posY = posY;
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
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0);
            //这里的beat被分为四部分 第一部分是小节 第二部分是拍 第三部分是拍号(2参数) 都用冒号分隔
            //详情可见xml文档
            //NoteHold会重写这个方法 主要是为了去添加其结束拍节点
            string[] beatStart = note.Attribute("beat").Value.Split('_');
            uint[] beat = Array.ConvertAll(beatStart[0].Split(":"), uint.Parse);
            Beat = new BeatNode(beat[0], beat[1], new Signature(beat[2], beat[3]));
            Speed = float.Parse(note.Attribute("speed").Value);
            isOut = bool.Parse(note.Attribute("out").Value);
            isMulti = bool.Parse(note.Attribute("multi").Value);
            posX = float.Parse(note.Attribute("x").Value);
        }

        public virtual async void TriggerNote()
        {
            await new Task(hit.PlayEffect);
            Destroy(gameObject);
        }

        public void ActiveNote()
        {
            if(!isActive)
            {
                renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 255);
                transform.UniformTranslate(new Vector2(transform.position.x, posY), 50);
                isActive = true;
            }
        }
        //protected abstract void JudgeNote(Touch touch);
    }
} 