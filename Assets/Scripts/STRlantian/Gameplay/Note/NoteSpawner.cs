using STRlantian.Gameplay.Block.Pane;
using STRlantian.Gameplay.Note;
using STRlantian.Util;
using System;
using System.Collections;
using UnityEngine;

namespace STRlantian.Gameplay.Note
{
    public class NoteSpawner : MonoBehaviour
    {
        [SerializeField]
        private NoteReceiver slot;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SpawnNote(NoteTypes tp, Panes attPane)
        {
            slot.curHasNote = true;
        }
    }
}