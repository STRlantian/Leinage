using STRlantian.Gameplay.Note;
using UnityEditor;
using UnityEngine;

namespace STRlantian.Editor
{
    public class Generator : ScriptableObject
    {
        [MenuItem("Tools/Generator/Notes")]
        private static void GenNotes()
        {
            GameObject tap = new GameObject("TapInst");
            tap.AddComponent<NoteTap>();
            GameObject flk = new GameObject("FlickInst");
            flk.AddComponent<NoteFlick>();
            GameObject drg = new GameObject("DragInst");
            drg.AddComponent<NoteDrag>();
            GameObject hld = new GameObject("HoldInst");
            hld.AddComponent<NoteHold>();
        }
    }
}