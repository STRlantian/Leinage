using Newtonsoft.Json.Bson;
using STRlantian.GameEffects;
using STRlantian.Gameplay.Block.Pane;
using System.Collections;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace STRlantian.Gameplay.Note
{
    public enum NoteTypes
    {
        TAP,
        HOLD,
        DRAG,
        FLICK,
        SPIN
    }
    public class Note : MonoBehaviour
    {
        public NoteTypes type;
        public Panes attachedPane;
        public float speed;

        [SerializeField]
        protected BoxCollider box;

        private readonly bool isMulti;
        private bool isOut;
        private HitEffect hit;
        private Transform block;
        private int[] rotationRange;
        private struct RotationInfo
        {
            public static int[] maxs;
            public static int[] mins;
            public static char target;
        }

        void Start()
        {
            block = GameObject.Find("Block").transform;
            InitRotationInfo();
        }

        void Update()
        {
            
        }

        public async void TriggerNote()
        {
            await new Task(hit.PlayEffect);
            Destroy(gameObject);
        }

        private void ChangeLayer()
        {
            if(rotationRange.Length == 6)
            {
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
                    RotationInfo.target = 'x';
                    break;
                case Panes.B:
                case Panes.BX:
                    RotationInfo.target = 'y';
                    break;
                case Panes.C:
                case Panes.CX:
                    RotationInfo.target = 'z';
                    break;
            }
        }
    }
}