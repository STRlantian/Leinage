using STRlantian.Util;
using System.Collections.Generic;
using UnityEngine;

namespace STRlantian.Gameplay.Block.ThreeDimens
{
    public class APolyhedron : MonoBehaviour
    {
        public readonly int paneCount;


        protected APolyhedron(int panes)
        {
            paneCount = panes;
        }
        private void Start()
        {
            
        }

        protected virtual void Initialise()
        {

        }
        protected virtual void Update()
        {
        }

        protected void RotateTo(Quaternion now)
        {
            STransformer.SmoothRotate(transform, now);
        }
    }
}