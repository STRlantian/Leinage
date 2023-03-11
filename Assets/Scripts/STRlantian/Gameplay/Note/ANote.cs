using STRlantian.GameEffects;
using STRlantian.Gameplay.Block.Pane;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace STRlantian.Gameplay.Note
{
    public enum NoteTypes
    {
        TAP,
        FLICK,
        DRAG,
        HOLD,
        SPIN
    }

    public abstract partial class ANote : MonoBehaviour
    {
        public NoteTypes type;
        public Panes attachedPane;
        public float speed;
        public int[] beat;

        [SerializeField]
        protected BoxCollider box;

        private readonly bool isMulti;
        private bool isOut;
        private HitEffect hit;
        private Transform block;
        private struct RotationInfo
        {
            public static int[] maxs;
            public static int[] mins;
            public static byte target;
        }

        protected ANote(List<XAttribute> attList)
        {
            switch (attList[0].Value.ToLower())
            {
                case "tap":
                    type = NoteTypes.TAP;
                    break;
                case "flick":
                    type = NoteTypes.FLICK;
                    break;
                case "drag":
                    type = NoteTypes.DRAG;
                    break;
                case "hold":
                    type = NoteTypes.HOLD;
                    break;
            }
            beat = Array.ConvertAll<string, int>(attList[1].Value.Split(':'), int.Parse);
        }

        void Start()
        {
            block = GameObject.Find("Block").transform;
            InitRotationInfo();
            
        }

        void Update()
        {
            ChangeLayer();
        }

        public virtual async void TriggerNote()
        {
            await new Task(hit.PlayEffect);
            Destroy(gameObject);
        }

        private void ChangeLayer()
        {
            float tar;
            for(int i = 0; i < RotationInfo.maxs.Length; i++)
            {
                tar = RotationInfo.target == 'x' ? block.rotation.x :
                    RotationInfo.target == 'y' ? block.rotation.y : block.rotation.z;
                GetComponent<SpriteRenderer>().sortingOrder = 
                    tar < RotationInfo.mins[i] || tar > RotationInfo.maxs[i] ? -1 : 1;
            }
        }

        private void InitRotationInfo()
        {
            //这段我找不到更好的写法（
            RotationInfo.maxs = (attachedPane == Panes.A
                || attachedPane == Panes.B
                || attachedPane == Panes.C) ? new int[3] { 90, 360, -270 } : new int[2] { -90, 270 };
            RotationInfo.mins = (attachedPane == Panes.A
                || attachedPane == Panes.B
                || attachedPane == Panes.C) ? new int[3] { -90, 270, -360 } : new int[2] { -270, 90 };
            switch (attachedPane)
            {
                case Panes.A:
                case Panes.AX:
                    RotationInfo.target = 0;
                    break;
                case Panes.B:
                case Panes.BX:
                    RotationInfo.target = 1;
                    break;
                case Panes.C:
                case Panes.CX:
                    RotationInfo.target = 2;
                    break;
            }
        }
    }
}