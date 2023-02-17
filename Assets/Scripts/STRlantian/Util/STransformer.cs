﻿using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UnityEngine;

namespace STRlantian.Util
{
    public static class STransformer
    {
        public static async void SmoothTranslate(Transform trans, Vector2 des)
        {
            for(int i = 0; i < 50; i++)
            {
                trans.Translate(SVectorConverter.VectorMinus(des, trans.position) / 25);
                await Task.Delay(2);
            }
        }

        public static async void SmoothRotate(Transform trans, Quaternion des)
        {
            float GetTo(float dest, float cur)
            {
                return cur + (dest - cur) / 100;
            }
            for(int i = 0; i < 100; i++) 
            {
                trans.rotation = new Quaternion(GetTo(des.x, trans.rotation.x),
                                                GetTo(des.y, trans.rotation.y),
                                                GetTo(des.z, trans.rotation.z),
                                                trans.rotation.w);
                await Task.Delay(1);
            }
        }
    }
}