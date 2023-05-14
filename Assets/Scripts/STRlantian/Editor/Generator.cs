using STRlantian.Gameplay.Note;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace STRlantian.Editor
{
    public class Generator : ScriptableObject
    {
        [MenuItem("Tools/LMGenerator/notes")]
        private static void GenNotes()
        {
            const string TAP = "NoteTap"
                , FLICK = "NoteFlick"
                , DRAG = "NoteDrag"
                , HOLD = "NoteHold";
            Type[] tps = new Type[] { typeof(SpriteRenderer), typeof(BoxCollider2D) };
            GameObject inst = GameObject.Find("Instance");

            GameObject tap = new GameObject(TAP, tps);
            NoteTap tp = tap.AddComponent<NoteTap>();
            tp.box = tap.GetComponent<BoxCollider2D>();
            tp.renderer = tap.GetComponent<SpriteRenderer>();

            GameObject flk = new GameObject(FLICK, tps);
            NoteFlick fk = flk.AddComponent<NoteFlick>();
            fk.box = flk.GetComponent<BoxCollider2D>();
            fk.renderer = flk.GetComponent<SpriteRenderer>();
            flk.transform.parent = inst.transform;

            GameObject drg = new GameObject(DRAG, tps);
            NoteDrag dg = drg.AddComponent<NoteDrag>();
            dg.box = flk.GetComponent<BoxCollider2D>();
            dg.renderer = flk.GetComponent<SpriteRenderer>();
            drg.transform.parent = inst.transform;

            GameObject hld = new GameObject(HOLD, tps);
            NoteHold hd = hld.AddComponent<NoteHold>();
            hd.box = flk.GetComponent<BoxCollider2D>();
            hd.renderer = flk.GetComponent<SpriteRenderer>();
            hld.transform.parent = inst.transform;
        }

        [MenuItem("Tools/LMGenerator/Chart")]
        private static void GenChart()
        {
            
        }
    }
}