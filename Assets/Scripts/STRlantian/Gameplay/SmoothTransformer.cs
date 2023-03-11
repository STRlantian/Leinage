using STRlantian.Util;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.STRlantian.Scripts.Gameplay
{
    /// <summary>
    /// SmoothTransformer: 平滑移动
    /// </summary>
    /// 要改
    public partial class SmoothTransformer : MonoBehaviour
    {
        public Vector2 destination;
        public int time;

        private bool isEnabled = false;

        void Update()
        {
            if(isEnabled)
            {
                TranslateStep();
                if(Mathf.Abs(SVectorConverter.VectorMinus(destination, transform.position).x) <= 0.01
                    || Mathf.Abs(SVectorConverter.VectorMinus(destination, transform.position).y) <= 0.01) 
                {
                    isEnabled = false;
                }
            }        
        }

        private async void TranslateStep()
        {
            transform.Translate(SVectorConverter.VectorMinus(destination, transform.position) / time);
            await Task.Delay(1);
        }

        public void SetEnable(Vector2 dest, int time, bool v)
        {
            destination = dest;
            this.time = time;
            isEnabled = v;
        }

        public void SetEnable(bool v)
        {
            isEnabled = v;
        }
    }
}