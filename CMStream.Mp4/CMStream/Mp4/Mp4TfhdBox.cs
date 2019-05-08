namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4TfhdBox : Mp4FullBox
    {
        public const uint FLAG_BASE_DATA_OFFSET_PRESENT = 1;
        public const uint FLAG_SAMPLE_DESCRIPTION_INDEX_PRESENT = 2;
        public const uint FLAG_DEFAULT_SAMPLE_DURATION_PRESENT = 8;
        public const uint FLAG_DEFAULT_SAMPLE_SIZE_PRESENT = 0x10;
        public const uint FLAG_DEFAULT_SAMPLE_FLAGS_PRESENT = 0x20;
        public const uint FLAG_DURATION_IS_EMPTY = 0x10000;

        public Mp4TfhdBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.TFHD, 0L, stream)
        {
            this.TrackId = stream.ReadUInt32();
            if ((base.Flags & 1) != 0)
            {
                this.BaseDataOffset = stream.ReadUInt64();
            }
            if ((base.Flags & 2) != 0)
            {
                this.SampleDescriptionIndex = stream.ReadUInt32();
            }
            if ((base.Flags & 8) != 0)
            {
                this.DefaultSampleDuration = stream.ReadUInt32();
            }
            if ((base.Flags & 0x10) != 0)
            {
                this.DefaultSampleSize = stream.ReadUInt32();
            }
            if ((base.Flags & 0x20) != 0)
            {
                this.DefaultSampleFlags = stream.ReadUInt32();
            }
        }

        public Mp4TfhdBox(uint flags, uint trackId, ulong baseDataOffset, uint sampleDescriptionIndex, uint defaultSampleDuration, uint defaultSampleSize, uint defaultSampleFlags) : base(0x10, Mp4BoxType.TFHD, 0L, 0, flags)
        {
            this.TrackId = trackId;
            this.BaseDataOffset = baseDataOffset;
            this.SampleDescriptionIndex = sampleDescriptionIndex;
            this.DefaultSampleDuration = defaultSampleDuration;
            this.DefaultSampleSize = defaultSampleSize;
            this.DefaultSampleFlags = defaultSampleFlags;
            if ((base.Flags & 1) != 0)
            {
                this.Size += 8;
            }
            if ((base.Flags & 2) != 0)
            {
                this.Size += 4;
            }
            if ((base.Flags & 8) != 0)
            {
                this.Size += 4;
            }
            if ((base.Flags & 0x10) != 0)
            {
                this.Size += 4;
            }
            if ((base.Flags & 0x20) != 0)
            {
                this.Size += 4;
            }
        }

        public override void WriteBody(Mp4Stream stream)
        {
            stream.WriteUInt32(this.TrackId);
            if ((base.Flags & 1) != 0)
            {
                stream.WriteUInt64(this.BaseDataOffset);
            }
            if ((base.Flags & 2) != 0)
            {
                stream.WriteUInt32(this.SampleDescriptionIndex);
            }
            if ((base.Flags & 8) != 0)
            {
                stream.WriteUInt32(this.DefaultSampleDuration);
            }
            if ((base.Flags & 0x10) != 0)
            {
                stream.WriteUInt32(this.DefaultSampleSize);
            }
            if ((base.Flags & 0x20) != 0)
            {
                stream.WriteUInt32(this.DefaultSampleFlags);
            }
        }

        public uint TrackId { get; private set; }

        public ulong BaseDataOffset { get; private set; }

        public uint SampleDescriptionIndex { get; private set; }

        public uint DefaultSampleDuration { get; private set; }

        public uint DefaultSampleSize { get; private set; }

        public uint DefaultSampleFlags { get; private set; }
    }
}

