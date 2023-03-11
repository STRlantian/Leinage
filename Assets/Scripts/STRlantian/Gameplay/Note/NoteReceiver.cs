using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STRlantian.Gameplay.Note
{
    public partial class NoteReceiver : MonoBehaviour
    {
        public bool curHasNote = false;
        public bool isAuto;
        public List<ANote> noteList = new(50);

        private BoxCollider2D box;
        private int received = 0;
        private NoteTypes curType;
        private float curSpeed;
        private Touch touch;
        

        void Start()
        {
            box = GetComponent<BoxCollider2D>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateCurrentAll()
        {
            curType = noteList[received].type;
        }
        private void JudgeNote()
        {
            if(isAuto)
            {
                if(noteList[received].transform.position == transform.position)
                {
                  
                }
            }
            if(curHasNote)
            {
                touch = Input.GetTouch(Input.touchCount - 1);
                
                if(box.bounds.Contains(touch.position))
                {
                    switch (curType)
                    {
                        case NoteTypes.TAP:

                            break;
                    }
                }
            }
        }
    }
}