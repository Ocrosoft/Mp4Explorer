namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Mp4ElstBox : Mp4FullBox
    {
        public Mp4ElstBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.ELST, 0L, stream)
        {
            uint num = stream.ReadUInt32();
            this.Entries = new List<Mp4ElstEntry>();
            for (uint i = 0; i < num; i++)
            {
                short num3;
                if (base.Version == 0)
                {
                    uint num4 = stream.ReadUInt32();
                    int num5 = stream.ReadInt32();
                    num3 = stream.ReadInt16();
                    stream.ReadInt16();
                    this.Entries.Add(new Mp4ElstEntry((ulong) num4, (long) num5, num3));
                }
                else
                {
                    ulong segmentDuration = stream.ReadUInt64();
                    long mediaTime = stream.ReadInt64();
                    num3 = stream.ReadInt16();
                    stream.ReadInt16();
                    this.Entries.Add(new Mp4ElstEntry(segmentDuration, mediaTime, num3));
                }
            }
        }

        public override void WriteBody(Mp4Stream stream)
        {
            throw new NotImplementedException();
        }

        public List<Mp4ElstEntry> Entries { get; private set; }

        public int EntryCount =>
            this.Entries.Count;
    }
}

