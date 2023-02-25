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
            public static float target;
        }

        void Start()
        {
            block = GameObject.Find("Block").transform;
            if (attachedPane == Panes.A
                || attachedPane == Panes.B
                || attachedPane == Panes.C)
            {
                RotationInfo.maxs = new int[3] { 90, 360, -270 };
                RotationInfo.mins = new int[3] { -90, 270, -360 };
                switch(attachedPane)
                {
                    case Panes.A:
                        RotationInfo.target = transform.rotation.x;
                        break;
                    case Panes.B:
                        RotationInfo.target = transform.rotation.y;
                        break;
                    case Panes.C:
                        RotationInfo.target = transform.rotation.z;
                        break;
                }
            }
            else
            {
                RotationInfo.maxs = new int[2] { -90, 270 };
                RotationInfo.mins = new int[2] { -270 , 90 };
            }
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
            void CheckLayer(float v, int[] ranges)
            {

            }
            if(rotationRange.Length == 6)
            {
            }
        }
    }
}