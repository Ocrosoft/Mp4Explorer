namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4MfhdBox : Mp4FullBox
    {
        public Mp4MfhdBox(uint sequenceNumber) : base(0x10, Mp4BoxType.MFHD)
        {
            this.SequenceNumber = sequenceNumber;
        }

        public Mp4MfhdBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.MFHD, 0L, stream)
        {
            this.SequenceNumber = stream.ReadUInt32();
        }

        public override void WriteBody(Mp4Stream stream)
        {
            stream.WriteUInt32(this.SequenceNumber);
        }

        public uint SequenceNumber { get; private set; }
    }
}

