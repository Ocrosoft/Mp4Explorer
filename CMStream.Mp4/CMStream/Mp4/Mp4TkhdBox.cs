namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4TkhdBox : Mp4FullBox
    {
        public const int TKHD_FLAG_TRACK_ENABLED = 1;
        public const int TKHD_FLAG_TRACK_IN_MOVIE = 2;
        public const int TKHD_FLAG_TRACK_IN_PREVIEW = 4;
        public const int TKHD_FLAG_DEFAULTS = 7;
        public byte[] Reserved2;
        public uint[] Matrix;

        public Mp4TkhdBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.TKHD, 0L, stream)
        {
            this.Reserved2 = new byte[8];
            this.Matrix = new uint[9];
            if (base.Version == 0)
            {
                this.CreationTime = stream.ReadUInt32();
                this.ModificationTime = stream.ReadUInt32();
                this.TrackId = stream.ReadUInt32();
                this.Reserved1 = stream.ReadUInt32();
                this.Duration = stream.ReadUInt32();
            }
            else
            {
                this.CreationTime = stream.ReadUInt64();
                this.ModificationTime = stream.ReadUInt64();
                this.TrackId = stream.ReadUInt32();
                this.Reserved1 = stream.ReadUInt32();
                this.Duration = stream.ReadUInt64();
            }
            stream.Read(this.Reserved2, 8);
            this.Layer = stream.ReadUInt16();
            this.AlternateGroup = stream.ReadUInt16();
            this.Volume = stream.ReadUInt16();
            this.Reserved3 = stream.ReadUInt16();
            for (int i = 0; i < 9; i++)
            {
                this.Matrix[i] = stream.ReadUInt32();
            }
            this.Width = stream.ReadUInt32();
            this.Height = stream.ReadUInt32();
        }

        public Mp4TkhdBox(DateTime creationTime, DateTime modificationTime, uint trackId, ulong duration, ushort volume, uint width, uint height) : base(0x68, Mp4BoxType.TKHD, 0L, 1, 7)
        {
            this.Reserved2 = new byte[8];
            this.Matrix = new uint[9];
            this.CreationTime = Mp4Util.ConvertTime(creationTime);
            this.ModificationTime = Mp4Util.ConvertTime(modificationTime);
            this.TrackId = trackId;
            this.Reserved1 = 0;
            this.Duration = duration;
            this.Layer = 0;
            this.AlternateGroup = 0;
            this.Volume = volume;
            this.Reserved3 = 0;
            this.Width = width;
            this.Height = height;
            this.Matrix[0] = 0x10000;
            this.Matrix[1] = 0;
            this.Matrix[2] = 0;
            this.Matrix[3] = 0;
            this.Matrix[4] = 0x10000;
            this.Matrix[5] = 0;
            this.Matrix[6] = 0;
            this.Matrix[7] = 0;
            this.Matrix[8] = 0x40000000;
            this.Reserved2[0] = 0;
            this.Reserved2[1] = 0;
        }

        public override void WriteBody(Mp4Stream stream)
        {
            if (base.Version == 0)
            {
                stream.WriteUInt32((uint) this.CreationTime);
                stream.WriteUInt32((uint) this.ModificationTime);
                stream.WriteUInt32(this.TrackId);
                stream.WriteUInt32(this.Reserved1);
                stream.WriteUInt32((uint) this.Duration);
            }
            else
            {
                stream.WriteUInt64(this.CreationTime);
                stream.WriteUInt64(this.ModificationTime);
                stream.WriteUInt32(this.TrackId);
                stream.WriteUInt32(this.Reserved1);
                stream.WriteUInt64(this.Duration);
            }
            stream.Write(this.Reserved2, this.Reserved2.Length);
            stream.WriteUInt16(this.Layer);
            stream.WriteUInt16(this.AlternateGroup);
            stream.WriteUInt16(this.Volume);
            stream.WriteUInt16(this.Reserved3);
            for (int i = 0; i < 9; i++)
            {
                stream.WriteUInt32(this.Matrix[i]);
            }
            stream.WriteUInt32(this.Width);
            stream.WriteUInt32(this.Height);
        }

        public ulong CreationTime { get; set; }

        public ulong ModificationTime { get; set; }

        public uint TrackId { get; set; }

        public uint Reserved1 { get; set; }

        public ulong Duration { get; set; }

        public ushort Layer { get; set; }

        public ushort AlternateGroup { get; set; }

        public ushort Volume { get; set; }

        public ushort Reserved3 { get; set; }

        public uint Width { get; set; }

        public uint Height { get; set; }
    }
}

