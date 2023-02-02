using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace STRlantian.GameEffects
{
    public class BlurApplier : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer sprRen;
        [SerializeField]
        private float speed = 0.5f;

        private Material blur;
        private int isEnable = 2;           //0 for yes, 1 for no, 2 for not active
        private int itensID;

        private void Start()
        {
            blur = sprRen.material;
            itensID = Shader.PropertyToID("_Intensity");
        }    

        private void Update()
        {
            if(isEnable == 0)
            {
                ApplyBlur();
            }
            else if(isEnable == 1)
            {
                DisableBlur();
            }
        }

        private async void ApplyBlur()
        {
            if(blur.GetFloat(itensID) < 20f)
            {
                blur.SetFloat(itensID, blur.GetFloat(itensID) + speed);     //Range: 0 - 20
                await Task.Delay(1);
            }
            else
            {
                isEnable = 2;                                           //Idle state
            }
        }

        private async void DisableBlur()
        {
            if(blur.GetFloat(itensID) > 0f)
            {
                blur.SetFloat(itensID, blur.GetFloat(itensID) - speed);
                await Task.Delay(1);
            }
            else
            {
                isEnable = 2;
            }
        }

        public void SetBlur(bool v)
        {
            if(v)
            {
                isEnable = 0;
            }
            else
            {
                isEnable = 1;
            }
        }
    }
}