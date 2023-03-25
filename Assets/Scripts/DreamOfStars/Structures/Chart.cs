using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamOfStars.GamePlay
{
    /// <summary>
    /// 谱面信息，还没来得及写，是的就是这么简陋
    /// </summary>
    [System.Serializable]
    public class Chart : AStoryBoard, IDeepCloneable<Chart>
    {
        public CameraController Camera;


        public Chart DeepClone()
        {
            Chart clone = new Chart()
            {
                Camera = Camera.DeepClone(),
                StoryBoard = StoryBoard.DeepClone()
            };
            return clone;
        }
    }
}