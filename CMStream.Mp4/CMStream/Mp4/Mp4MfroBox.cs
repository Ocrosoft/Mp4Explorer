namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4MfroBox : Mp4FullBox
    {
        public Mp4MfroBox(uint mfraSize) : base(0x10, Mp4BoxType.MFRO, 0L, 0, 1)
        {
            this.MfraSize = mfraSize;
        }

        public Mp4MfroBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.MFRO, 0L, stream)
        {
            this.MfraSize = stream.ReadUInt32();
        }

        public override void WriteBody(Mp4Stream stream)
        {
            stream.WriteUInt32(this.MfraSize);
        }

        public uint MfraSize { get; private set; }
    }
}

