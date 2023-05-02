using System.Threading.Tasks;
using UnityEngine;

namespace STRlantian.Util
{
    public static class STransformer
    {
        public static async void UniformTranslate(this Transform trans, Vector2 des, float time)
        {
            Vector2 step = Vector2.zero;
            for(int i = 0; i < time; i++)
            {
                step = SVectorConverter.VectorMinus(des, trans.position);
                trans.Translate(step / time);
                await Task.Delay(1);
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