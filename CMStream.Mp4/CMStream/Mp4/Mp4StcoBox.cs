namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Mp4StcoBox : Mp4FullBox
    {
        public Mp4StcoBox(uint[] entries) : base((uint) (0x10 + (entries.Length * 4)), Mp4BoxType.STCO)
        {
            this.Entries = new List<uint>(entries);
        }

        public Mp4StcoBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.STCO, 0L, stream)
        {
            uint num = stream.ReadUInt32();
            if (num > (((size - 12) - 4) / 4))
            {
                num = ((size - 12) - 4) / 4;
            }
            this.Entries = new List<uint>((int) num);
            byte[] buffer = new byte[num * 4];
            stream.Read(buffer, (int) (num * 4));
            for (int i = 0; i < num; i++)
            {
                this.Entries.Add(Mp4Util.BytesToUInt32BE(buffer, i * 4));
            }
        }

        public uint GetChunkOffset(uint chunk)
        {
            if ((chunk > this.Entries.Count) || (chunk == 0))
            {
                throw new Exception("ERROR_OUT_OF_RANGE");
            }
            return this.Entries[((int) chunk) - 1];
        }

        public override void WriteBody(Mp4Stream stream)
        {
            stream.WriteUInt32((uint) this.Entries.Count);
            for (int i = 0; i < this.Entries.Count; i++)
            {
                stream.WriteUInt32(this.Entries[i]);
            }
        }

        public List<uint> Entries { get; set; }
    }
}

