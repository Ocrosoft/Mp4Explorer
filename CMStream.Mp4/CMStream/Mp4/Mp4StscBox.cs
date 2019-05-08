namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class Mp4StscBox : Mp4FullBox
    {
        private uint cachedChunkGroup;

        public Mp4StscBox() : base(0x10, Mp4BoxType.STSC)
        {
            this.Entries = new List<Mp4StscEntry>();
        }

        public Mp4StscBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.STSC, 0L, stream)
        {
            uint num = 1;
            uint num2 = stream.ReadUInt32();
            this.Entries = new List<Mp4StscEntry>((int) num2);
            byte[] buffer = new byte[num2 * 12];
            stream.Read(buffer, (int) (num2 * 12));
            for (int i = 0; i < num2; i++)
            {
                uint num4 = Mp4Util.BytesToUInt32BE(buffer, i * 12);
                uint num5 = Mp4Util.BytesToUInt32BE(buffer, (i * 12) + 4);
                uint num6 = Mp4Util.BytesToUInt32BE(buffer, (i * 12) + 8);
                if (i > 0)
                {
                    int num7 = i - 1;
                    this.Entries[num7].ChunkCount = num4 - this.Entries[num7].FirstChunk;
                    num += this.Entries[num7].ChunkCount * this.Entries[num7].SamplesPerChunk;
                }
                this.Entries.Add(new Mp4StscEntry());
                this.Entries[i].ChunkCount = 0;
                this.Entries[i].FirstChunk = num4;
                this.Entries[i].FirstSample = num;
                this.Entries[i].SamplesPerChunk = num5;
                this.Entries[i].SampleDescriptionIndex = num6;
            }
        }

        public void GetChunkForSample(uint sample, out uint chunk, out uint skip, out uint sampleDescriptionIndex)
        {
            int cachedChunkGroup;
            if (sample <= 0)
            {
                throw new Exception("sample > 0");
            }
            if ((this.cachedChunkGroup < this.Entries.Count) && (this.Entries[(int) this.cachedChunkGroup].FirstSample <= sample))
            {
                cachedChunkGroup = (int) this.cachedChunkGroup;
            }
            else
            {
                cachedChunkGroup = 0;
            }
            while (cachedChunkGroup < this.Entries.Count)
            {
                uint num2 = this.Entries[cachedChunkGroup].ChunkCount * this.Entries[cachedChunkGroup].SamplesPerChunk;
                if (num2 == 0)
                {
                    if (this.Entries[cachedChunkGroup].FirstSample > sample)
                    {
                        throw new Exception("INVALID_FORMAT");
                    }
                }
                else if ((this.Entries[cachedChunkGroup].FirstSample + num2) <= sample)
                {
                    cachedChunkGroup++;
                    continue;
                }
                if (this.Entries[cachedChunkGroup].SamplesPerChunk == 0)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                uint num3 = (sample - this.Entries[cachedChunkGroup].FirstSample) / this.Entries[cachedChunkGroup].SamplesPerChunk;
                chunk = this.Entries[cachedChunkGroup].FirstChunk + num3;
                skip = sample - (this.Entries[cachedChunkGroup].FirstSample + (this.Entries[cachedChunkGroup].SamplesPerChunk * num3));
                sampleDescriptionIndex = this.Entries[cachedChunkGroup].SampleDescriptionIndex;
                this.cachedChunkGroup = (uint) cachedChunkGroup;
                return;
            }
            chunk = 0;
            skip = 0;
            sampleDescriptionIndex = 0;
            throw new Exception("OUT_OF_RANGE");
        }

        public override void WriteBody(Mp4Stream stream)
        {
            stream.WriteUInt32(this.EntryCount);
            for (int i = 0; i < this.EntryCount; i++)
            {
                stream.WriteUInt32(this.Entries[i].FirstChunk);
                stream.WriteUInt32(this.Entries[i].SamplesPerChunk);
                stream.WriteUInt32(this.Entries[i].SampleDescriptionIndex);
            }
        }

        public override uint Size
        {
            get
            {
                if (((base.Size == 0x10) && (this.Entries != null)) && (this.Entries.Count > 0))
                {
                    return (base.Size + ((uint) ((this.Entries.Count * 3) * 4)));
                }
                return base.Size;
            }
            set => 
                base.Size = value;
        }

        public uint EntryCount =>
            ((uint) this.Entries.Count);

        public List<Mp4StscEntry> Entries { get; private set; }
    }
}

