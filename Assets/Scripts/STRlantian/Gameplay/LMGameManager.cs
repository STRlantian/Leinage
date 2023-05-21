using STRlantian.Gameplay.Block;
using STRlantian.Gameplay.Charting;
using STRlantian.Gameplay.Note;
using System.Collections.Generic;
using UnityEngine;

namespace STRlantian.Gameplay
{

    public class LMGameManager : MonoBehaviour
    {
        public int Bpm { get; set; }
        public BeatRail CurrentBeat { get; private set; }

        private float beatPerMill;

        void Start()
        {
            Init();
        }

        void Update()
        {
            CurrentBeat += beatPerMill;
        }

        public void Init()
        {
            beatPerMill = Bpm / 60 / 1000;
        }
    }
}
