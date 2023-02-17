using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STRlantian.Gameplay.Block.FlatPane
{
    public abstract class PaneController : MonoBehaviour
    {
        [SerializeField]
        protected List<Vector3> initXYZ = new(3);
        [SerializeField]
        protected List<Transform> paneList = new(3);

        void Start()
        {
            Init();
        }

        void Update()
        {
            
        }

        protected virtual void Init()
        {
            try
            {
                for (int i = 0; i < paneList.Count; i++)
                {
                    paneList[i].position += new Vector3(0, 0, 0);
                }
            }catch(IndexOutOfRangeException)
            {
                Debug.LogError("你能不能把面和对应的初始位置设置的数量一样");
            }
        }

        protected abstract void ChangePosition();
    }
}