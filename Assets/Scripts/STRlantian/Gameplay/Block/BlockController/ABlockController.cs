using STRlantian.Util; 
using System;
using System.Collections.Generic;
using UnityEngine;

namespace STRlantian.Gameplay.Block.BlockController
{
    /// <summary>
    ///     ABlockController: 几何体总控制器 抽象类
    /// </summary>
    public abstract class ABlockController : MonoBehaviour
    {
        /* 对于这个类我没有太下功夫
         * 因为想着先做好Note那边的事
         * 这个类的设想要实现一系列运动和缩小放大
         * 都挺基础的东西 甚至可以写个扩展方法类来替换这玩意
         * 要不要只是写个拓展方法类还在考虑
        */ 
        [SerializeField]
        protected float initSize = 3f;
        void Start()
        {
            Init();
        }

        void Update()
        {
        }

        /// <summary>
        /// Init(): 这个 额 我也不知道为什么总是喜欢写个Init类而不是直接重写Start()
        /// 或许该改一改这个毛病(? 也可能因为Init这个名字好听
        /// </summary>
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