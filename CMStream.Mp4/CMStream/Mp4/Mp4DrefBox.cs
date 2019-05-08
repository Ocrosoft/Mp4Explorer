namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Mp4DrefBox : Mp4FullBox
    {
        public Mp4DrefBox(Mp4Box[] refs) : base(12, Mp4BoxType.DREF)
        {
            this.Size += 4;
            this.Entries = new List<Mp4Box>(refs.Length);
            for (uint i = 0; i < refs.Length; i++)
            {
                this.Entries.Add(refs[i]);
                this.Size += refs[i].Size;
            }
        }

        public Mp4DrefBox(uint size, Mp4Stream stream, Mp4BoxFactory factory) : base(size, Mp4BoxType.DREF, 0L, stream)
        {
            uint num = stream.ReadUInt32();
            this.Entries = new List<Mp4Box>((int) num);
            uint num2 = num;
            long bytesAvailable = (size - 12) - 4;
            while (num2-- > 0)
            {
                Mp4Box item = null;
                while ((item = factory.Read(stream, ref bytesAvailable)) != null)
                {
                    this.Entries.Add(item);
                }
            }
        }

        public override void WriteBody(Mp4Stream stream)
        {
            stream.WriteUInt32((uint) this.Entries.Count);
            foreach (Mp4Box box in this.Entries)
            {
                box.Write(stream);
            }
        }

        public List<Mp4Box> Entries { get; private set; }
    }
}

