namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Mp4TrunBox : Mp4FullBox
    {
        public const uint FLAG_DATA_OFFSET_PRESENT = 1;
        public const uint FLAG_FIRST_SAMPLE_FLAGS_PRESENT = 4;
        public const uint FLAG_SAMPLE_DURATION_PRESENT = 0x100;
        public const uint FLAG_SAMPLE_SIZE_PRESENT = 0x200;
        public const uint FLAG_SAMPLE_FLAGS_PRESENT = 0x400;
        public const uint FLAG_SAMPLE_COMPOSITION_TIME_OFFSET_PRESENT = 0x800;

        public Mp4TrunBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.TRUN, 0L, stream)
        {
            uint num = stream.ReadUInt32();
            if ((base.Flags & 1) != 0)
            {
                this.DataOffset = (int) stream.ReadUInt32();
            }
            if ((base.Flags & 4) != 0)
            {
                this.FirstSampleFlags = stream.ReadUInt32();
            }
            this.Entries = new List<Mp4TrunEntry>((int) num);
            for (int i = 0; i < num; i++)
            {
                this.Entries.Add(new Mp4TrunEntry());
                if ((base.Flags & 0x100) != 0)
                {
                    this.Entries[i].SampleDuration = stream.ReadUInt32();
                }
                if ((base.Flags & 0x200) != 0)
                {
                    this.Entries[i].SampleSize = stream.ReadUInt32();
                }
                if ((base.Flags & 0x400) != 0)
                {
                    this.Entries[i].SampleFlags = stream.ReadUInt32();
                }
                if ((base.Flags & 0x800) != 0)
                {
                    this.Entries[i].SampleCompositionTimeOffset = stream.ReadUInt32();
                }
            }
        }

        public Mp4TrunBox(uint flags, int dataOffset, uint firstSampleFlags) : base(0x10, Mp4BoxType.TRUN, 0L, 0, flags)
        {
            this.DataOffset = dataOffset;
            this.FirstSampleFlags = firstSampleFlags;
            if ((base.Flags & 1) != 0)
            {
                this.Size += 4;
            }
            if ((base.Flags & 4) != 0)
            {
                this.Size += 4;
            }
            this.Entries = new List<Mp4TrunEntry>();
        }

        public override void WriteBody(Mp4Stream stream)
        {
            stream.WriteUInt32((uint) this.Entries.Count);
            if ((base.Flags & 1) != 0)
            {
                stream.WriteUInt32((uint) this.DataOffset);
            }
            if ((base.Flags & 4) != 0)
            {
                stream.WriteUInt32(this.FirstSampleFlags);
            }
            for (int i = 0; i < this.Entries.Count; i++)
            {
                if ((base.Flags & 0x100) != 0)
                {
                    stream.WriteUInt32(this.Entries[i].SampleDuration);
                }
                if ((base.Flags & 0x200) != 0)
                {
                    stream.WriteUInt32(this.Entries[i].SampleSize);
                }
                if ((base.Flags & 0x400) != 0)
                {
                    stream.WriteUInt32(this.Entries[i].SampleFlags);
                }
                if ((base.Flags & 0x800) != 0)
                {
                    stream.WriteUInt32(this.Entries[i].SampleCompositionTimeOffset);
                }
            }
        }

        public override uint Size
        {
            get
            {
                if (((base.Size != 0x10) || (this.Entries == null)) || (this.Entries.Count <= 0))
                {
                    return base.Size;
                }
                int num = 0;
                if ((base.Flags & 0x100) != 0)
                {
                    num++;
                }
                if ((base.Flags & 0x200) != 0)
                {
                    num++;
                }
                if ((base.Flags & 0x400) != 0)
                {
                    num++;
                }
                if ((base.Flags & 0x800) != 0)
                {
                    num++;
                }
                return (base.Size + ((uint) ((this.Entries.Count * num) * 4)));
            }
            set => 
                base.Size = value;
        }

        public int SampleCount =>
            this.Entries.Count;

        public int DataOffset { get; private set; }

        public uint FirstSampleFlags { get; private set; }

        public List<Mp4TrunEntry> Entries { get; private set; }
    }
}

