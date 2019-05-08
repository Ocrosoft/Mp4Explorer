namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4SmhdBox : Mp4FullBox
    {
        public Mp4SmhdBox(short balance) : base(0x10, Mp4BoxType.SMHD)
        {
            this.Balance = balance;
            this.Reserved = 0;
        }

        public Mp4SmhdBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.SMHD, 0L, stream)
        {
            this.Balance = stream.ReadInt16();
            this.Reserved = stream.ReadUInt16();
        }

        public override void WriteBody(Mp4Stream stream)
        {
            stream.WriteInt16(this.Balance);
            stream.WriteUInt16(this.Reserved);
        }

        public short Balance { get; set; }

        public ushort Reserved { get; set; }
    }
}

