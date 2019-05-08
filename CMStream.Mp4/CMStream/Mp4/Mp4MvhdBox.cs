namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4MvhdBox : Mp4FullBox
    {
        public byte[] Reserved1;
        public byte[] Reserved2;
        public uint[] Matrix;
        public byte[] Predefined;

        public Mp4MvhdBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.MVHD, 0L, stream)
        {
            this.Reserved1 = new byte[2];
            this.Reserved2 = new byte[8];
            this.Matrix = new uint[9];
            this.Predefined = new byte[0x18];
            if (base.Version == 0)
            {
                this.CreationTime = stream.ReadUInt32();
                this.ModificationTime = stream.ReadUInt32();
                this.TimeScale = stream.ReadUInt32();
                this.Duration = stream.ReadUInt32();
            }
            else
            {
                this.CreationTime = stream.ReadUInt64();
                this.ModificationTime = stream.ReadUInt64();
                this.TimeScale = stream.ReadUInt32();
                this.Duration = stream.ReadUInt64();
            }
            this.Rate = stream.ReadUInt32();
            this.Volume = stream.ReadUInt16();
            stream.Read(this.Reserved1, this.Reserved1.Length);
            stream.Read(this.Reserved2, this.Reserved2.Length);
            for (int i = 0; i < 9; i++)
            {
                this.Matrix[i] = stream.ReadUInt32();
            }
            stream.Read(this.Predefined, this.Predefined.Length);
            this.NextTrackId = stream.ReadUInt32();
        }

        public Mp4MvhdBox(DateTime creationTime, DateTime modificationTime, uint timeScale, ulong duration, uint rate, ushort volume) : base(120, Mp4BoxType.MVHD, 0L, 1, 0)
        {
            this.Reserved1 = new byte[2];
            this.Reserved2 = new byte[8];
            this.Matrix = new uint[9];
            this.Predefined = new byte[0x18];
            this.CreationTime = Mp4Util.ConvertTime(creationTime);
            this.ModificationTime = Mp4Util.ConvertTime(modificationTime);
            this.TimeScale = timeScale;
            this.Duration = duration;
            this.Rate = rate;
            this.Volume = volume;
            this.NextTrackId = uint.MaxValue;
            this.Matrix[0] = 0x10000;
            this.Matrix[1] = 0;
            this.Matrix[2] = 0;
            this.Matrix[3] = 0;
            this.Matrix[4] = 0x10000;
            this.Matrix[5] = 0;
            this.Matrix[6] = 0;
            this.Matrix[7] = 0;
            this.Matrix[8] = 0x40000000;
        }

        public override void WriteBody(Mp4Stream stream)
        {
            if (base.Version == 0)
            {
                stream.WriteUInt32((uint) this.CreationTime);
                stream.WriteUInt32((uint) this.ModificationTime);
                stream.WriteUInt32(this.TimeScale);
                stream.WriteUInt32((uint) this.Duration);
            }
            else
            {
                stream.WriteUInt64(this.CreationTime);
                stream.WriteUInt64(this.ModificationTime);
                stream.WriteUInt32(this.TimeScale);
                stream.WriteUInt64(this.Duration);
            }
            stream.WriteUInt32(this.Rate);
            stream.WriteUInt16(this.Volume);
            stream.Write(this.Reserved1, this.Reserved1.Length);
            stream.Write(this.Reserved2, this.Reserved2.Length);
            for (int i = 0; i < 9; i++)
            {
                stream.WriteUInt32(this.Matrix[i]);
            }
            stream.Write(this.Predefined, this.Predefined.Length);
            stream.WriteUInt32(this.NextTrackId);
        }

        public ulong CreationTime { get; set; }

        public ulong ModificationTime { get; set; }

        public uint TimeScale { get; set; }

        public ulong Duration { get; set; }

        public uint Rate { get; set; }

        public ushort Volume { get; set; }

        public uint NextTrackId { get; set; }
    }
}

