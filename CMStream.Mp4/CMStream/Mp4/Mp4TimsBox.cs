namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4TimsBox : Mp4Box
    {
        public Mp4TimsBox(uint timescale) : base(12, Mp4BoxType.TIMS)
        {
            this.TimeScale = timescale;
        }

        public Mp4TimsBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.TIMS)
        {
            this.TimeScale = stream.ReadUInt32();
        }

        public override void WriteBody(Mp4Stream stream)
        {
            stream.WriteUInt32(this.TimeScale);
        }

        public uint TimeScale { get; private set; }
    }
}

