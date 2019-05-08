namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Mp4Co64Box : Mp4FullBox
    {
        public Mp4Co64Box(ulong[] entries) : base((uint) (0x10 + (entries.Length * 8)), Mp4BoxType.CO64)
        {
            this.Entries = new List<ulong>(entries);
        }

        public Mp4Co64Box(uint size, Mp4Stream stream) : base(size, Mp4BoxType.CO64, 0L, stream)
        {
            uint num = stream.ReadUInt32();
            if (num > (((size - 12) - 4) / 8))
            {
                num = ((size - 12) - 4) / 8;
            }
            this.Entries = new List<ulong>((int) num);
            for (int i = 0; i < num; i++)
            {
                this.Entries.Add(stream.ReadUInt64());
            }
        }

        public ulong GetChunkOffset(int chunk) => 
            this.Entries[chunk - 1];

        public override void WriteBody(Mp4Stream stream)
        {
            stream.WriteUInt32(this.EntryCount);
            for (int i = 0; i < this.EntryCount; i++)
            {
                stream.WriteUInt64(this.Entries[i]);
            }
        }

        public uint EntryCount =>
            ((uint) this.Entries.Count);

        public List<ulong> Entries { get; set; }
    }
}

