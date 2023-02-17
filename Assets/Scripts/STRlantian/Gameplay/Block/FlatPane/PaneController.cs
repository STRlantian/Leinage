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
        [SerializeField]
        protected float initSize = 3.5f;
        void Start()
        {
            Init();
        }

        void Update()
        {
            
        }

        protected virtual void Init()
        {
            transform.localScale = new Vector3(initSize, initSize, initSize);
            try
            {
                for (int i = 0; i < paneList.Count; i++)
                {
                    paneList[i].position = initXYZ[i];
                }
            }catch(IndexOutOfRangeException)
            {
                Debug.LogError("小朋友，可不可以尝试把面数和对应的初始位置的数设置的一样呢？加油，我相信你可以的！");
            }
        }

        protected abstract void ChangePosition();
    }
}