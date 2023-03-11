using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace STRlantian.GameEffects
{
    /// <summary>
    ///     HitEffect: 打击特效类, 在ANote中有用, 也许以后会用在ui
    /// </summary>
    /// ====================
    /// 这个类要改 因为每次都实例化一个GameObject的话很费内存
    /// 会改成从一个现有的GameObject赋值
    /// ====================
    public partial class HitEffect : MonoBehaviour
    {
        private static int showTime;            
        private Animator anim;
        
        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        public void PlayEffect()
        {
            anim.Play("HitEffect");
        }

        public static void CreateHitEffect(Vector2 loc)
        {
            GameObject hit = new GameObject($"HitEffect{showTime}");
            showTime++;
            hit.AddComponent<HitEffect>().PlayEffect();
            Destroy(hit);
        }
    }
}