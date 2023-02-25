using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace STRlantian.GameEffects
{
    public class HitEffect : MonoBehaviour
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