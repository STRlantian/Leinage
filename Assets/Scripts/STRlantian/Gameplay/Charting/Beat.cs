namespace STRlantian.Gameplay.Charting
{
    public struct Signature
    {
        public static readonly Signature SIG_24 = new Signature(4, 2);
        public static readonly Signature SIG_34 = new Signature(4, 3);
        public static readonly Signature SIG_44 = new Signature(4, 4);
        public static readonly Signature SIG_68 = new Signature(8, 6);

        private byte beatsPerBar;             //每小节拍数
        public byte BeatsPerBar { get { return beatsPerBar; } }

        private byte timePerBeat;             //拍时值
        public byte TimePerBeat { get { return timePerBeat; } }

        public Signature(byte bpb, byte tpb)
        {
            beatsPerBar = bpb;
            timePerBeat = tpb;
        }
    }
    public struct ChartBeat
    {
        private Signature signature;            //拍号
        public Signature Signature { get{ return signature; } }

        private ushort bar;                     //小节数
        public ushort Bar { get { return bar; } }

        private ushort beat;                    //小节内拍数
        public ushort Beat { get { return beat; } }

        public ChartBeat(ushort bar, ushort beat, Signature sig)
        {
            this.bar = bar;
            this.beat = beat;
            signature = sig;
            if (beat > signature.BeatsPerBar)
            {
                throw new System.Exception("Beat value should be less that signature beat");
            }
        }
    }
}
