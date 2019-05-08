namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class Mp4SttsBox : Mp4FullBox
    {
        private LookupCache lookupCache;

        public Mp4SttsBox() : base(0x10, Mp4BoxType.STTS)
        {
            this.lookupCache = new LookupCache();
            this.Entries = new List<Mp4SttsEntry>();
        }

        public Mp4SttsBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.STTS, 0L, stream)
        {
            this.lookupCache = new LookupCache();
            uint num = stream.ReadUInt32();
            this.Entries = new List<Mp4SttsEntry>((int) num);
            for (int i = 0; i < num; i++)
            {
                uint sampleCount = stream.ReadUInt32();
                uint sampleDelta = stream.ReadUInt32();
                this.Entries.Add(new Mp4SttsEntry(sampleCount, sampleDelta));
            }
        }

        public void GetDts(int sample, out ulong dts, out uint duration)
        {
            dts = 0L;
            duration = 0;
            if (sample == 0)
            {
                throw new Exception("OUT_OF_RANGE");
            }
            int entryIndex = 0;
            uint num2 = 0;
            ulong num3 = 0L;
            if (sample >= this.lookupCache.sample)
            {
                entryIndex = this.lookupCache.entryIndex;
                num2 = this.lookupCache.sample;
                num3 = this.lookupCache.dts;
            }
            for (int i = entryIndex; i < this.Entries.Count; i++)
            {
                Mp4SttsEntry entry = this.Entries[i];
                if (sample <= (num2 + entry.SampleCount))
                {
                    dts = num3 + ((ulong) (((sample - 1) - num2) * entry.SampleDelta));
                    duration = entry.SampleDelta;
                    this.lookupCache.entryIndex = i;
                    this.lookupCache.sample = num2;
                    this.lookupCache.dts = num3;
                    return;
                }
                num2 += entry.SampleCount;
                num3 += entry.SampleCount * entry.SampleDelta;
            }
            throw new Exception("OUT_OF_RANGE");
        }

        public override void WriteBody(Mp4Stream stream)
        {
            stream.WriteUInt32(this.EntryCount);
            for (int i = 0; i < this.EntryCount; i++)
            {
                stream.WriteUInt32(this.Entries[i].SampleCount);
                stream.WriteUInt32(this.Entries[i].SampleDelta);
            }
        }

        public override uint Size
        {
            get
            {
                if (((base.Size == 0x10) && (this.Entries != null)) && (this.Entries.Count > 0))
                {
                    return (base.Size + ((uint) ((this.Entries.Count * 2) * 4)));
                }
                return base.Size;
            }
            set => 
                base.Size = value;
        }

        public uint EntryCount =>
            ((uint) this.Entries.Count);

        public List<Mp4SttsEntry> Entries { get; private set; }

        private class LookupCache
        {
            public int entryIndex;
            public uint sample;
            public ulong dts;
        }
    }
}

