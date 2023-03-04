using System.Threading.Tasks;
using UnityEngine;

namespace STRlantian.GameEffects
{
    /// <summary>
    ///     BlurApplier: 对GameObject进行模糊化处理
    ///     关联的shader: ImageBlur.shader
    /// </summary>
    public class BlurApplier : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer sprRen;                      //如果是Image的话。。。到时候再说吧(逃
        [SerializeField]
        private float speed = 0.5f;                         //模糊变化速度(每次模糊度变化值)

        private Material blur;                              //对应着色器材质
        private bool? isEnable = false;                     //是否启用模糊器
        private int itensID;                                //shader中着色器强度变量对应的id

        public bool CurrentState                            //虽然但是, 仍然不习惯用属性, 因为java没属性
        {
            set { isEnable = value; }
        }

        private void Start()            
        {
            blur = sprRen.material;
            itensID = Shader.PropertyToID("_Intensity");
        }    

        private void Update()
        {
            if (isEnable == true)
            {
                ApplyBlur();
            }
            else if (isEnable == false)
            {
                DisableBlur();
            }
        }

        /// <summary>
        /// ApplyBlur(): 启用着色器, 在结束后将是否启用设置为null
        /// </summary>
        private async void ApplyBlur()
        {
            if(blur.GetFloat(itensID) < 20f)                //20应该不会随便改所以我就没有再分出来个变量(?
            {
                blur.SetFloat(itensID, blur.GetFloat(itensID) + speed);
                await Task.Delay(1);
            }
            else
            {
                isEnable = null;
            }
        }

        /// <summary>
        /// DisableBlur(): 同上, 与上相反
        /// </summary>
        private async void DisableBlur()
        {
            if(blur.GetFloat(itensID) > 0f)
            {
                blur.SetFloat(itensID, blur.GetFloat(itensID) - speed);
                await Task.Delay(1);
            }
            else
            {
                isEnable = null;
            }
        }
    }
}