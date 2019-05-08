namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Mp4StsdBox : Mp4FullBox
    {
        public Mp4StsdBox(Mp4SampleTable sampleTable) : base(12, Mp4BoxType.STSD)
        {
            this.Entries = new List<Mp4Box>();
            this.Size += 4;
            int sampleDescriptionCount = sampleTable.SampleDescriptionCount;
            for (int i = 0; i < sampleDescriptionCount; i++)
            {
                Mp4SampleEntry sampleDescription = sampleTable.GetSampleDescription(i);
                this.Entries.Add(sampleDescription);
                this.Size += sampleDescription.Size;
            }
        }

        public Mp4StsdBox(uint size, Mp4Stream stream, Mp4BoxFactory factory) : base(size, Mp4BoxType.STSD, 0L, stream)
        {
            uint num = stream.ReadUInt32();
            factory.PushContext(base.Type);
            long bytesAvailable = (size - 12) - 4;
            this.Entries = new List<Mp4Box>((int) num);
            for (int i = 0; i < num; i++)
            {
                Mp4Box item = factory.Read(stream, ref bytesAvailable);
                if (item != null)
                {
                    this.Entries.Add(item);
                }
            }
            factory.PopContext();
        }

        public Mp4SampleEntry GetSampleEntry(int index)
        {
            if (index >= this.Entries.Count)
            {
                return null;
            }
            return (this.Entries[index] as Mp4SampleEntry);
        }

        public override void WriteBody(Mp4Stream stream)
        {
            stream.WriteUInt32((uint) this.Entries.Count);
            foreach (Mp4Box box in this.Entries)
            {
                long position = stream.Position;
                box.Write(stream);
                long num3 = stream.Position - position;
                if (num3 > box.Size)
                {
                    throw new Exception("Internal error");
                }
            }
        }

        public override int HeaderSize =>
            (12 + ((this.Size == 1) ? 8 : 0));

        public List<Mp4Box> Entries { get; private set; }
    }
}

