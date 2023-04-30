namespace STRlantian.Gameplay.Charting
{
    public struct Signature                                    //拍号
    {
        public static readonly Signature SIG_24 = new Signature(4, 2);
        public static readonly Signature SIG_34 = new Signature(4, 3);
        public static readonly Signature SIG_44 = new Signature(4, 4);
        public static readonly Signature SIG_68 = new Signature(8, 6);

        public byte BeatsPerBar { get; private set; }         //每小节拍数
        public byte TimePerBeat { get; private set; }         //每拍时值

        public Signature(byte beatsPerBar, byte timePerBeat)
        {
            BeatsPerBar = beatsPerBar;
            TimePerBeat = timePerBeat;
        }
    }
    public struct BeatNode                                   //小节节点
    {
        public Signature Signature { get; private set; }
        public ushort Bar { get; private set; }              //第几小节
        public ushort Beat { get; private set; }             //第几拍

        public BeatNode(ushort bar, ushort beat, Signature sig)
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
