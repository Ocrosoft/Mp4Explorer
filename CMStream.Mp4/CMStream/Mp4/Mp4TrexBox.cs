namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4TrexBox : Mp4FullBox
    {
        public Mp4TrexBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.TREX, 0L, stream)
        {
            this.TrackId = stream.ReadUInt32();
            this.DefaultSampleDescriptionIndex = stream.ReadUInt32();
            this.DefaultSampleDuration = stream.ReadUInt32();
            this.DefaultSampleSize = stream.ReadUInt32();
            this.DefaultSampleFlags = stream.ReadUInt32();
        }

        public Mp4TrexBox(uint trackId, uint defaultSampleDescriptionIndex, uint defaultSampleDuration, uint defaultSampleSize, uint defaultSampleFlags) : base(0x20, Mp4BoxType.TREX)
        {
            this.TrackId = trackId;
            this.DefaultSampleDescriptionIndex = defaultSampleDescriptionIndex;
            this.DefaultSampleDuration = defaultSampleDuration;
            this.DefaultSampleSize = defaultSampleSize;
            this.DefaultSampleFlags = defaultSampleFlags;
        }

        public override void WriteBody(Mp4Stream stream)
        {
            stream.WriteUInt32(this.TrackId);
            stream.WriteUInt32(this.DefaultSampleDescriptionIndex);
            stream.WriteUInt32(this.DefaultSampleDuration);
            stream.WriteUInt32(this.DefaultSampleSize);
            stream.WriteUInt32(this.DefaultSampleFlags);
        }

        public uint TrackId { get; private set; }

        public uint DefaultSampleDescriptionIndex { get; private set; }

        public uint DefaultSampleDuration { get; private set; }

        public uint DefaultSampleSize { get; private set; }

        public uint DefaultSampleFlags { get; private set; }
    }
}

