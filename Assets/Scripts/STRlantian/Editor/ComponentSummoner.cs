using Assets.Scripts.STRlantian.Gameplay.Note;
using STRlantian.Gameplay.Note;
using UnityEditor;
using UnityEngine;

namespace STRlantian.Editor
{
    public class ComponentSummoner : ScriptableObject
    {
        [MenuItem("Tools/ComponentSummoner/Notes")]
        private static void SummonNotes()
        {
            GameObject tap = new GameObject();
            tap.AddComponent<NoteTap>();
        }
    }
}