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
        public NoteType type;
        public Pane attachedPane;
        public float speed;
        public ChartBeat beat;

        [SerializeField]
        protected BoxCollider box;

        private readonly bool isMulti;
        private bool isOut;
        private HitEffect hit;
        private struct RotationInfo
        {
            public static int[] maxs;
            public static int[] mins;
            public static byte target;
        }

        protected ANote(List<XAttribute> attList)
        {
            InitAttributes(attList);

        }

        void Start()
        {
            InitRotationInfo();
        }

        void Update()
        {
            ChangeLayer();
        }

        protected virtual void InitAttributes(List<XAttribute> attList)
        {
            //beat = Array.ConvertAll(attList[1].Value.Split(':'), ushort.Parse);
            /*
            attachedPane = new APane();
            attachedPane = pn.Equals("A") ? PaneType.A
                         : pn.Equals("AX") ? PaneType.AX
                         : pn.Equals("B") ? PaneType.B
                         : pn.Equals("BX") ? PaneType.BX
                         : pn.Equals("C") ? PaneType.C
                         : pn.Equals("CX") ? PaneType.CX
                         : throw new Exception("Invalid Pane");
            //line
            */
        }

        public virtual async void TriggerNote()
        {
            await new Task(hit.PlayEffect);
            Destroy(gameObject);
        }

        private void ChangeLayer()
        {
            /*
            float tar;
            for(int i = 0; i < RotationInfo.maxs.Length; i++)
            {
                tar = RotationInfo.target == 'x' ? block.rotation.x :
                    RotationInfo.target == 'y' ? block.rotation.y : block.rotation.z;
                GetComponent<SpriteRenderer>().sortingOrder = 
                    tar < RotationInfo.mins[i] || tar > RotationInfo.maxs[i] ? -1 : 1;
            }
            */
        }

        private void InitRotationInfo()
        {
            /*
            //这段我找不到更好的写法（
            RotationInfo.maxs = (attachedPane == PaneType.A
                || attachedPane == PaneType.B
                || attachedPane == PaneType.C) ? new int[3] { 90, 360, -270 } : new int[2] { -90, 270 };
            RotationInfo.mins = (attachedPane == PaneType.A
                || attachedPane == PaneType.B
                || attachedPane == PaneType.C) ? new int[3] { -90, 270, -360 } : new int[2] { -270, 90 };
            switch (attachedPane)
            {
                case PaneType.A:
                case PaneType.AX:
                    RotationInfo.target = 0;
                    break;
                case PaneType.B:
                case PaneType.BX:
                    RotationInfo.target = 1;
                    break;
                case PaneType.C:
                case PaneType.CX:
                    RotationInfo.target = 2;
                    break;
            }
            */
        }

        //protected abstract void JudgeNote();
    }
}