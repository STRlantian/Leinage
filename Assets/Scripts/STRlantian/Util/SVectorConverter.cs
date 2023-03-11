using UnityEngine;

namespace STRlantian.Util
{
    /// <summary>
    /// SVectorConverter: 用于Vector3和2之间的互相转化 我被逼急了写的类
    /// 主要是z的问题 其实可以考虑整成扩展方法
    /// </summary>
    public static partial class SVectorConverter
    {
        /// <summary>
        /// ConvertV3ToV2: 快速转化V3到V2 不损失z
        /// </summary>
        /// <param name="v3">被转化对象</param>
        /// <returns>转化结果</returns>
        public static Vector2 ConvertV3ToV2(this Vector3 v3)
        {
            return new Vector2(v3.x, v3.y);
        }

        /// <summary>
        /// CompareV3ToV2: 快速比较V3 V2
        /// </summary>
        /// <param name="v3">被比较对象</param>
        /// <param name="v2">被比较对象</param>
        /// <returns>一样不一样</returns>
        public static bool CompareV3WithV2(this Vector3 v3, Vector2 v2)
        {
            return v3.x == v2.x && v3.y == v2.y;
        }

        /// <summary>
        /// CompareV3ToV2: 快速比较V3 V2
        /// </summary>
        /// <param name="v2">被比较对象</param>
        /// <param name="v3">被比较对象</param>
        /// <returns>一样不一样</returns>
        public static bool CompareV3WithV2(this Vector2 v2, Vector3 v3)
        {
            return v3.x == v2.x && v3.y == v2.y;
        }

        /// <summary>
        /// VectorPlus: 快速相加V3 V2
        /// </summary>
        /// <param name="v3">加数</param>
        /// <param name="v2">加数</param>
        /// <returns>和</returns>
        public static Vector2 VectorPlus(this Vector3 v3, Vector2 v2)
        {
            return ConvertV3ToV2(v3) + v2;
        }

        /// <summary>
        /// VectorPlus: 快速相加V3 V2
        /// </summary>
        /// <param name="v2">加数</param>
        /// <param name="v3">加数</param>
        /// <returns>和</returns>
        public static Vector2 VectorPlus(this Vector2 v2, Vector3 v3)
        {
            return ConvertV3ToV2(v3) + v2;
        }

        /// <summary>
        /// VectorMinus: 快速相减V3 V2
        /// </summary>
        /// <param name="v3">被减数</param>
        /// <param name="v2">减数</param>
        /// <returns>差</returns>
        public static Vector2 VectorMinus(this Vector3 v3, Vector2 v2)
        {
            return ConvertV3ToV2(v3) - v2;
        }

        /// <summary>
        /// VectorMinus: 快速相减V3 V2
        /// </summary>
        /// <param name="v2">被减数</param>
        /// <param name="v3">减数</param>
        /// <returns>差</returns>
        public static Vector2 VectorMinus(this Vector2 v2, Vector3 v3)
        {
            return v2 - ConvertV3ToV2(v3);
        }
    }
}
