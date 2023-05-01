namespace STRlantian.Gameplay.Charting
{
    /// <summary>
    /// 拍号结构体
    /// </summary>
    public struct Signature                                    //拍号
    {
        public static readonly Signature SIG_24 = new Signature(4, 2);
        public static readonly Signature SIG_34 = new Signature(4, 3);
        public static readonly Signature SIG_44 = new Signature(4, 4);
        public static readonly Signature SIG_68 = new Signature(8, 6);

        public uint BeatsPerBar { get; private set; }         //每小节拍数
        public uint TimePerBeat { get; private set; }         //每拍时值

        /// <summary>
        /// 自己构造个拍号 虽然常用的已经列出来了
        /// </summary>
        /// <param name="beatsPerBar">每小节的拍子数 例如一小节4拍</param>
        /// <param name="timePerBeat">每拍的时值 例如以四分音符为一拍</param>
        public Signature(uint beatsPerBar, uint timePerBeat)
        {
            BeatsPerBar = beatsPerBar;
            TimePerBeat = timePerBeat;
        }
    }

    /// <summary>
    /// 小节 拍子和拍号的一个节点 应该也可以用来表示整首歌的(?)
    /// </summary>
    public struct BeatNode                                   //小节节点
    {
        public Signature Signature { get; private set; }
        public uint Bar { get; private set; }              //第几小节
        public uint Beat { get; private set; }             //第几拍

        /// <summary>
        /// 构造一个Beat节点
        /// </summary>
        /// <param name="bar">小节(数)</param>
        /// <param name="beat">小节中的哪一拍()</param>
        /// <param name="sig">拍号 如上</param>
        /// <exception cref="System.Exception">小节内拍数的数字应该小于拍号的</exception>
        public BeatNode(uint bar, uint beat, Signature sig)
        {
            if (beat > sig.BeatsPerBar)
            {
                throw new System.Exception("Beat value should be less than signature beat");
            }
            Bar = bar;
            Beat = beat;
            Signature = sig;
        }
    }
}
