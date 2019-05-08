namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4VmhdBox : Mp4FullBox
    {
        public ushort[] OpColor;

        public Mp4VmhdBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.VMHD, 0L, stream)
        {
            this.OpColor = new ushort[3];
            this.GraphicsMode = stream.ReadUInt16();
            this.OpColor[0] = stream.ReadUInt16();
            this.OpColor[1] = stream.ReadUInt16();
            this.OpColor[2] = stream.ReadUInt16();
        }

        public Mp4VmhdBox(ushort graphicsMode, ushort r, ushort g, ushort b) : base(20, Mp4BoxType.VMHD, 0L, 0, 1)
        {
            this.OpColor = new ushort[3];
            this.GraphicsMode = graphicsMode;
            this.OpColor[0] = r;
            this.OpColor[1] = g;
            this.OpColor[2] = b;
        }

        public override void WriteBody(Mp4Stream stream)
        {
            stream.WriteUInt16(this.GraphicsMode);
            stream.WriteUInt16(this.OpColor[0]);
            stream.WriteUInt16(this.OpColor[1]);
            stream.WriteUInt16(this.OpColor[2]);
        }

        public ushort GraphicsMode { get; set; }
    }
}

