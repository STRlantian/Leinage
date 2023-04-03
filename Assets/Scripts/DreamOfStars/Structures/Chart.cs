using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamOfStars.GamePlay
{
    /// <summary>
    /// Æ×ÃæÐÅÏ¢
    /// </summary>
    [System.Serializable]
    public class Chart : AStoryBoard, IDeepCloneable<Chart>
    {
        public CameraController Camera;

        public List<Pane> PaneList = new List<Pane>();
        public List<PaneGroup> PaneGroups = new List<PaneGroup>();

        public Chart DeepClone()
        {
            Chart clone = new Chart()
            {
                Camera = Camera.DeepClone(),
                StoryBoard = StoryBoard.DeepClone()
            };
            foreach(Pane p in PaneList) clone.PaneList.Add(p.DeepClone());
            foreach(PaneGroup p in PaneGroups) clone.PaneGroups.Add(p.DeepClone());
            return clone;
        }
    }
}