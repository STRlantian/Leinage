using STRlantian.Util;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace STRlantian.Gameplay.Block.BlockController
{
    public abstract class ABlockController : MonoBehaviour
    {
        [SerializeField]
        protected float initSize = 3f;
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
        }

        public void Rotate(Vector3 dest)
        {
            STransformer.SmoothRotate(transform, new Quaternion(dest.x, dest.y, dest.z, transform.rotation.w));
        }

    }
}