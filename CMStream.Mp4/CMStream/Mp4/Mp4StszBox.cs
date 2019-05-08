namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Mp4StszBox : Mp4FullBox
    {
        public Mp4StszBox() : base(20, Mp4BoxType.STSZ)
        {
            this.Entries = new List<uint>();
        }

        public Mp4StszBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.STSZ, 0L, stream)
        {
            this.SampleSize = stream.ReadUInt32();
            uint num = stream.ReadUInt32();
            if (this.SampleSize == 0)
            {
                this.Entries = new List<uint>((int) num);
                byte[] buffer = new byte[num * 4];
                stream.Read(buffer, (int) (num * 4));
                for (uint i = 0; i < num; i++)
                {
                    this.Entries.Add(Mp4Util.BytesToUInt32BE(buffer, (int) (i * 4)));
                }
            }
            else
            {
                this.Entries = new List<uint>();
            }
        }

        public uint GetSampleSize(uint sample)
        {
            uint num = 0;
            if ((sample > this.SampleCount) || (sample == 0))
            {
                num = 0;
                throw new Exception("ERROR_OUT_OF_RANGE");
            }
            if (this.SampleSize != 0)
            {
                return this.SampleSize;
            }
            return this.Entries[((int) sample) - 1];
        }

        public override void WriteBody(Mp4Stream stream)
        {
            stream.WriteUInt32(this.SampleSize);
            stream.WriteUInt32(this.SampleCount);
            if (this.SampleSize == 0)
            {
                for (int i = 0; i < this.SampleCount; i++)
                {
                    stream.WriteUInt32(this.Entries[i]);
                }
            }
        }

        public override uint Size
        {
            get
            {
                if (((base.Size == 20) && (this.Entries != null)) && (this.Entries.Count > 0))
                {
                    return (base.Size + ((uint) (this.Entries.Count * 4)));
                }
                return base.Size;
            }
            set => 
                base.Size = value;
        }

        public uint SampleSize { get; private set; }

        public uint SampleCount =>
            ((uint) this.Entries.Count);

        public List<uint> Entries { get; private set; }
    }
}

