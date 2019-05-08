namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4HmhdBox : Mp4FullBox
    {
        public Mp4HmhdBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.HMHD, 0L, stream)
        {
            this.MaxPduSize = stream.ReadUInt16();
            this.AvgPduSize = stream.ReadUInt16();
            this.MaxBitrate = stream.ReadUInt32();
            this.AvgBitrate = stream.ReadUInt32();
            this.Reserved = stream.ReadUInt32();
        }

        public override void WriteBody(Mp4Stream stream)
        {
            throw new NotImplementedException();
        }

        public ushort MaxPduSize { get; set; }

        public ushort AvgPduSize { get; set; }

        public uint MaxBitrate { get; set; }

        public uint AvgBitrate { get; set; }

        public uint Reserved { get; set; }
    }
}

