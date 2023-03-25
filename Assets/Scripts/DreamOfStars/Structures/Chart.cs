using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamOfStars.GamePlay
{
    /// <summary>
    /// ������Ϣ����û���ü�д���ǵľ�����ô��ª
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