namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Mp4CttsBox : Mp4FullBox
    {
        private LookupCache lookupCache;

        public Mp4CttsBox() : base(0x10, Mp4BoxType.CTTS)
        {
            this.lookupCache = new LookupCache();
            this.Entries = new List<Mp4CttsEntry>();
        }

        public Mp4CttsBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.CTTS, 0L, stream)
        {
            this.lookupCache = new LookupCache();
            uint num = stream.ReadUInt32();
            this.Entries = new List<Mp4CttsEntry>((int) num);
            byte[] buffer = new byte[num * 8];
            stream.Read(buffer, (int) (num * 8));
            bool flag = false;
            int num2 = 0;
            for (int i = 0; i < num; i++)
            {
                this.Entries.Add(new Mp4CttsEntry());
                this.Entries[i].SampleCount = Mp4Util.BytesToUInt32BE(buffer, i * 8);
                uint num4 = Mp4Util.BytesToUInt32BE(buffer, (i * 8) + 4);
                if ((num4 & 0x80000000) != 0)
                {
                    flag = true;
                    int num5 = (int) num4;
                    if (num5 < num2)
                    {
                        num2 = num5;
                    }
                }
                this.Entries[i].SampleOffset = num4;
            }
            if (flag)
            {
                for (int j = 0; j < num; j++)
                {
                    Mp4CttsEntry local1 = this.Entries[j];
                    local1.SampleOffset -= (uint) num2;
                }
            }
        }

        public uint GetCtsOffset(uint sample)
        {
            uint sampleOffset = 0;
            if (sample == 0)
            {
                throw new Exception("OUT_OF_RANGE");
            }
            uint entryIndex = 0;
            uint num3 = 0;
            if (sample >= this.lookupCache.sample)
            {
                entryIndex = this.lookupCache.entryIndex;
                num3 = this.lookupCache.sample;
            }
            for (uint i = entryIndex; i < this.Entries.Count; i++)
            {
                Mp4CttsEntry entry = this.Entries[(int) i];
                if (sample <= (num3 + entry.SampleCount))
                {
                    sampleOffset = entry.SampleOffset;
                    this.lookupCache.entryIndex = i;
                    this.lookupCache.sample = num3;
                    return sampleOffset;
                }
                num3 += entry.SampleCount;
            }
            throw new Exception("OUT_OF_RANGE");
        }

        public override void WriteBody(Mp4Stream stream)
        {
            throw new NotImplementedException();
        }

        public List<Mp4CttsEntry> Entries { get; set; }

        private class LookupCache
        {
            public uint sample;
            public uint entryIndex;
        }
    }
}

