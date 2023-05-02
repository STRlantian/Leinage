using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace STRlantian.Util
{
    public static class STextRoller
    {
        public static int wait
        {
            get { return wait; }
            set
            {
                if (value > 0)
                {
                    wait = value;
                }
            }
        }

        public async static void RollText(this Text field, string s)
        {
            if(!(wait > 0))
            {
                wait = 2;
            }
            char[] chars = s.ToCharArray();
            foreach (char c in chars)
            {
                field.text += c;
                await Task.Delay(wait);
            }
        }
    }
}
