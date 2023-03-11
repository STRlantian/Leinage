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
        }
    }
}