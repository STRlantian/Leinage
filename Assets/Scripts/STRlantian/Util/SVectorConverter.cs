using UnityEngine;

namespace STRlantian.Util
{
    public static class SVectorConverter
    {
        public static Vector2 ConvertV3ToV2(Vector3 v3)
        {
            return new Vector2(v3.x, v3.y);
        }

        public static bool CompareV3WithV2(Vector3 v3, Vector2 v2)
        {
            return v3.x == v2.x && v3.y == v2.y;
        }

        public static Vector2 VectorPlus(Vector3 v3, Vector2 v2)
        {
            return ConvertV3ToV2(v3) + v2;
        }

        public static Vector2 VectorMinus(Vector3 v3, Vector2 v2)
        {
            return ConvertV3ToV2(v3) - v2;
        }

        public static Vector2 VectorMinus(Vector2 v2, Vector3 v3)
        {
            return v2 - ConvertV3ToV2(v3);
        }
    }
}
